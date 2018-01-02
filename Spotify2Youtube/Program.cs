using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Spotify2Youtube.Configs;
using Xabe.FFmpeg;

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
			var docDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			var downloadDir = $@"{docDir}\Spotify2Youtube\Downloads";
			var convertedDir = $@"{docDir}\Spotify2Youtube\Converted";

			// Check if the Download Directory exists
			if (!Directory.Exists(downloadDir))
				Directory.CreateDirectory(downloadDir);

			// Check if the Converted Directory exists
			if (!Directory.Exists(convertedDir))
				Directory.CreateDirectory(convertedDir);

			MainConfig.DownloadDir = downloadDir;
			MainConfig.ConvertedDir = convertedDir;


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new WebControl());

		}
	}
}
