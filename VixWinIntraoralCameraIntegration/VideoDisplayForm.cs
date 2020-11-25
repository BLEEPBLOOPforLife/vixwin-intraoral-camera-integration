using System;
using System.Drawing;
using System.Windows.Forms;

using Accord.Video;

namespace VixWinIntraoralCameraIntegration
{
	/// <summary>
	/// The user-created VideoDisplayForm partial class.
	/// </summary>
	public partial class VideoDisplayForm : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VideoDisplayForm"/> class.
		/// </summary>
		public VideoDisplayForm( )
		{
			InitializeComponent( );
		}

		/// <summary>
		/// Sets the video stream for the window.
		/// </summary>
		/// <param name="videoSource">The video source.</param>
		public void SetVideoStream( IVideoSource videoSource )
		{
			StopVideoStream( );
			videoSourcePlayer1.VideoSource = new AsyncVideoSource( videoSource );
			videoSourcePlayer1.Start( );
		}

		/// <summary>
		/// Stops the video stream.
		/// </summary>
		public void StopVideoStream( )
		{
			videoSourcePlayer1.SignalToStop( );
			videoSourcePlayer1.WaitForStop( );
		}

		/// <summary>
		/// Initiializes the video display form.
		/// </summary>
		private void VideoDisplayForm_Load( object sender, EventArgs e )
		{
			Text = "Camera Display Window";
			Icon = new Icon( AppDomain.CurrentDomain.BaseDirectory + "tray_icon.ico" );
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
		}

		/// <summary>
		/// Called when the manual capture button is pressed.
		/// </summary>
		private void Capture_Click( object sender, EventArgs e )
		{
			WebcamManager.VideoSource?.SimulateTrigger( );
		}
	}
}
