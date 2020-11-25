using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

using Accord.Video;
using Accord.Video.DirectShow;

namespace VixWinIntraoralCameraIntegration
{
	/// <summary>
	/// The class that manages the active webcam.
	/// </summary>
	public static class WebcamManager
	{
		/// <summary>
		/// Gets the currently selected video source.
		/// </summary>
		internal static VideoCaptureDevice VideoSource { get; private set; }
		private static DateTime firstFrameDateTime;

		/// <summary>
		/// Gets the last frame captured.
		/// </summary>
		public static Bitmap LastFrame { get; private set; }
		private readonly static Dictionary<string, string> webcamDictionary;

		/// <summary>
		/// Static constructor for the <see cref="WebcamManager"/> class.
		/// </summary>
		static WebcamManager( )
		{
			webcamDictionary = new Dictionary<string, string>( );
		}

		/// <summary>
		/// Gets a read-only dictionary of webcams.
		/// </summary>
		/// <returns>A ReadOnlyDictionary with the key being the camera's moniker and the value being the camera's pretty name.</returns>
		public static ReadOnlyDictionary<string, string> GetWebcamList( )
		{
			RefreshWebcamList( );

			return new ReadOnlyDictionary<string, string>( webcamDictionary );
		}

		/// <summary>
		/// Switches the active webcam.
		/// </summary>
		/// <param name="monikerString">The moniker string of the new webcam.</param>
		public static void SelectWebcam( string monikerString )
		{
			if ( monikerString != null && webcamDictionary.TryGetValue( monikerString, out string webcamPrettyName ) )
			{
				App.CurrentVideoDisplayForm.StopVideoStream( );
				firstFrameDateTime = default;
				VideoSource = new VideoCaptureDevice( monikerString );
				VideoSource.NewFrame += new NewFrameEventHandler( NewVideoFrame );
				VideoSource.ProvideSnapshots = true;
				VideoSource.SnapshotFrame += new NewFrameEventHandler( NewSnapshotFrame );
				App.CurrentVideoDisplayForm.SetVideoStream( VideoSource );
				TrayIcon.Current.SelectWebcamForDisplay( webcamPrettyName );
				File.WriteAllText( AppDomain.CurrentDomain.BaseDirectory + "default_webcam.txt", monikerString );
			} else
			{
				App.CurrentVideoDisplayForm.StopVideoStream( );
				VideoSource = null;
				TrayIcon.Current.SelectWebcamForDisplay( "None" );
			}
		}

		/// <summary>
		/// Refreshes the webcam dictionary.
		/// </summary>
		internal static void RefreshWebcamList( )
		{
			webcamDictionary.Clear( );
			FilterInfoCollection videoDevices = new FilterInfoCollection( FilterCategory.VideoInputDevice );

			foreach ( FilterInfo videoDevice in videoDevices )
			{
				webcamDictionary.Add( videoDevice.MonikerString, videoDevice.Name );
			}
		}

		/// <summary>
		/// Called when a new video frame is sent to us.
		/// </summary>
		private static void NewVideoFrame( object sender, NewFrameEventArgs eventArgs )
		{
			if ( firstFrameDateTime.Equals( default ) )
			{
				firstFrameDateTime = DateTime.Now;
			}

			Bitmap frame = eventArgs.Frame;
			LastFrame?.Dispose( );
			LastFrame = new Bitmap( frame );
		}

		/// <summary>
		/// Called when a new snapshot frame is sent to us.
		/// </summary>
		private static void NewSnapshotFrame( object sender, NewFrameEventArgs eventArgs )
		{
			if ( firstFrameDateTime.Equals( default ) )
			{
				return;
			}

			TimeSpan timeSinceFirstFrame = DateTime.Now - firstFrameDateTime;

			if ( timeSinceFirstFrame.TotalSeconds < 0.5 ) // Add this delay because the camera takes a snapshot at the beginning for some reason.
			{
				return;
			}

			Bitmap frame = eventArgs.Frame;
			string directoryPath = @"C:\VixTemp\FileIn\";
			string[ ] files = Directory.GetFiles( directoryPath );
			int indexOfLastFile = -1;

			if ( files.Any( ) )
			{
				foreach ( string file in files )
				{
					string indexOfFile = file.Replace( directoryPath + "plug", "" ).Replace( ".jpg", "" );
					bool successfullyConvertedToInt = int.TryParse( indexOfFile, out int indexOfFileAsInt );

					if ( successfullyConvertedToInt && indexOfFileAsInt > indexOfLastFile )
					{
						indexOfLastFile = indexOfFileAsInt;
					}
				}
			}

			frame.Save( directoryPath + "plug" + ( indexOfLastFile + 1 ) + ".jpg", ImageFormat.Jpeg );
		}
	}
}
