using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VixWinIntraoralCameraIntegration
{
	/// <summary>
	/// The tray icon class.
	/// </summary>
	public class TrayIcon
	{
		/// <summary>
		/// Gets the current instance of the tray icon.
		/// </summary>
		public static TrayIcon Current { get; private set; }
		private readonly NotifyIcon trayIconMenu;

		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public TrayIcon( )
		{
			Current = this;
			trayIconMenu = new NotifyIcon( );
			trayIconMenu.Icon = new Icon( AppDomain.CurrentDomain.BaseDirectory + "tray_icon.ico" );
			trayIconMenu.Text = "VixWin Intraoral Camera Integration";
			trayIconMenu.ContextMenuStrip = new ContextMenuStrip( );
			trayIconMenu.Visible = true;
			PopulateTrayIconMenu( );
		}

		/// <summary>
		/// Selects a webcam for displaying in the tray menu.
		/// </summary>
		/// <param name="cameraName">The webcam's pretty name.</param>
		internal void SelectWebcamForDisplay( string webcamName )
		{
			trayIconMenu.ContextMenuStrip.Items[ 2 ].Text = "Camera: " + webcamName;
		}

		/// <summary>
		/// Updates the webcam list in the sub-context menu for webcam selection.
		/// </summary>
		/// <param name="selectWebcamMenu">The webcam sub-context menu to update.</param>
		private void UpdateWebcamList( ToolStripMenuItem selectWebcamMenu )
		{
			ToolStripItemCollection dropDownItems = selectWebcamMenu.DropDownItems;
			dropDownItems.Clear( );

			foreach ( KeyValuePair<string, string> webcam in WebcamManager.GetWebcamList( ) )
			{
				dropDownItems.Add( webcam.Value, null, ( object sender, EventArgs handler ) => WebcamManager.SelectWebcam( webcam.Key ) );
			}
		}

		/// <summary>
		/// Populates the tray icon menu initially.
		/// </summary>
		private void PopulateTrayIconMenu( )
		{
			ToolStripItemCollection trayItems = trayIconMenu.ContextMenuStrip.Items;
			trayItems.Add( new ToolStripLabel( "VixWin Intraoral Camera Integration" ) );
			trayItems.Add( new ToolStripSeparator( ) );
			trayItems.Add( new ToolStripLabel( "" ) );
			trayItems.Add( new ToolStripSeparator( ) );
			ToolStripMenuItem selectWebcamMenu = new ToolStripMenuItem( "Select Webcam" );

			trayIconMenu.MouseClick += ( object sender, MouseEventArgs eventArgs ) =>
			{
				if ( eventArgs.Button == MouseButtons.Right )
				{
					UpdateWebcamList( selectWebcamMenu );
				}
			};

			trayItems.Add( selectWebcamMenu );
			trayItems.Add( new ToolStripSeparator( ) );
			trayItems.Add( "Exit", null, App.Current.Exit );
		}
	}
}
