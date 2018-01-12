using System;
using System.Windows.Forms;
using Spotify2Youtube.Helpers;

namespace Spotify2Youtube
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		[STAThread]
		private static void Main()
		{
			InitialSetup.Run(); //Finds and sets paths and settings used in the app

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new WebControl());
		}
	}
}