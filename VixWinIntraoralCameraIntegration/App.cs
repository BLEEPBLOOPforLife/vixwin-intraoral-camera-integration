using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VixWinIntraoralCameraIntegration
{
	/// <summary>
	/// The application class.
	/// </summary>
	public class App : ApplicationContext
	{
		/// <summary>
		/// Gets the current App instance.
		/// </summary>
		public static App Current { get; private set; }

		/// <summary>
		/// Gets the current video display form.
		/// </summary>
		internal static VideoDisplayForm CurrentVideoDisplayForm { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App( )
		{
			Current = this;
			new TrayIcon( );
			CreateVideoDisplayForm( );
			SelectDefaultWebcam( );
		}

		/// <summary>
		/// Exits the program with a confirmation dialog.
		/// </summary>
		public void Exit( object sender, EventArgs e )
		{
			Application.Exit( );
		}

		/// <summary>
		/// Creates the video display form.
		/// </summary>
		private void CreateVideoDisplayForm( )
		{
			CurrentVideoDisplayForm = new VideoDisplayForm( );
			CurrentVideoDisplayForm.StartPosition = FormStartPosition.CenterScreen;
			CurrentVideoDisplayForm.Visible = true;
			CurrentVideoDisplayForm.FormClosing += ( object sender, FormClosingEventArgs e ) => CurrentVideoDisplayForm.StopVideoStream( );
			CurrentVideoDisplayForm.FormClosed += ( object sender, FormClosedEventArgs e ) => Exit( null, null );
		}

		/// <summary>
		/// Selects the default or first webcam as the active webcam.
		/// </summary>
		private void SelectDefaultWebcam( )
		{
			string path = AppDomain.CurrentDomain.BaseDirectory + "default_webcam.txt";
			string webcamMoniker;

			if ( File.Exists( path ) )
			{
				webcamMoniker = File.ReadAllText( AppDomain.CurrentDomain.BaseDirectory + "default_webcam.txt" );
			} else
			{
				ReadOnlyDictionary<string, string> webcamDictionary = WebcamManager.GetWebcamList( );
				webcamMoniker = webcamDictionary.Any( ) ? WebcamManager.GetWebcamList( ).First( ).Key : null;
			}

			WebcamManager.RefreshWebcamList( );
			WebcamManager.SelectWebcam( webcamMoniker );
		}
	}
}
