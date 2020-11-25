namespace VixWinIntraoralCameraIntegration
{
	/// <summary>
	/// The designer generated VideoDisplayForm partial class.
	/// </summary>
	partial class VideoDisplayForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose( );
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( )
		{
			this.videoSourcePlayer1 = new Accord.Controls.VideoSourcePlayer();
			this.capture = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			// videoSourcePlayer1
			//
			this.videoSourcePlayer1.Location = new System.Drawing.Point(12, 53);
			this.videoSourcePlayer1.Name = "videoSourcePlayer1";
			this.videoSourcePlayer1.Size = new System.Drawing.Size(1026, 617);
			this.videoSourcePlayer1.TabIndex = 0;
			this.videoSourcePlayer1.Text = "videoSourcePlayer1";
			this.videoSourcePlayer1.VideoSource = null;
			//
			// capture
			//
			this.capture.Location = new System.Drawing.Point(459, 12);
			this.capture.Name = "capture";
			this.capture.Size = new System.Drawing.Size(132, 35);
			this.capture.TabIndex = 1;
			this.capture.Text = "Capture";
			this.capture.UseVisualStyleBackColor = true;
			this.capture.Click += new System.EventHandler(this.Capture_Click);
			//
			// VideoDisplayForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1050, 682);
			this.Controls.Add(this.capture);
			this.Controls.Add(this.videoSourcePlayer1);
			this.Name = "VideoDisplayForm";
			this.Text = "VideoDisplayForm";
			this.Load += new System.EventHandler(this.VideoDisplayForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Accord.Controls.VideoSourcePlayer videoSourcePlayer1;
		private System.Windows.Forms.Button capture;
	}
}
