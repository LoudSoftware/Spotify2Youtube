using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Spotify2Youtube.Properties;

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


			Settings.Default.DownloadPath = downloadDir;
			Settings.Default.ConvertedPath = convertedDir;

			Debug.WriteLine(Settings.Default.DownloadPath);
			Debug.WriteLine(Settings.Default.ConvertedPath);

			Settings.Default.Save();


			var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
			Console.WriteLine(config.AppSettings.Settings["youtubeApi"].Value);
			config.AppSettings.Settings["youtubeApi"].Value = "Hello world";
			Console.WriteLine(config.AppSettings.Settings["youtubeApi"].Value);
			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new WebControl());
		}
	}
}