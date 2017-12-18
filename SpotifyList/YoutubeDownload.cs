using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaToolkit;
using MediaToolkit.Model;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;


namespace SpotifyList
{
	internal class YoutubeDownload
	{
		private readonly string _id;
		private readonly string _title;
		private readonly string _artist;
		private readonly ProgressBar _progressBar;

		public YoutubeDownload(ProgressBar progressBar, string id, string title, string artist)
		{
			_id = id;
			_title = title;
			_artist = artist;
			_progressBar = progressBar;

			// Check if the Download Directory exists
			if (!Directory.Exists(@".\Downloads"))
				Directory.CreateDirectory(@".\Downloads");

			// Check if the Converted Directory exists
			if (!Directory.Exists(@".\Converted"))
				Directory.CreateDirectory(@".\Converted");
		}

		public async Task Download()
		{
			var client = new YoutubeClient();

			var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(_id);

			var streamInfo = streamInfoSet.Audio.WithHighestBitrate();
			var ext = streamInfo.Container.GetFileExtension();

			

			var progress = new Progress<double>(p =>
			{
				
				if (_progressBar.Value != (int) Math.Round(p*100) )
				{
					Debug.WriteLine((int) Math.Round(p*100));
				}
				_progressBar.Value = (int)Math.Round(p * 100);
				
			});

			var filename = $"{_title} - {_artist}";


			await client.DownloadMediaStreamAsync(streamInfo, $@".\Downloads\{filename}.{ext}", progress);

			Debug.WriteLine("Download complete!");
			Debug.WriteLine("Now converting the file");
			ConvertToMp3(filename, ext);


		}

		public void ConvertToMp3(string filename, string ext)
		{
			

			var inputFile = new MediaFile { Filename = $@".\Downloads\{filename}.{ext}" };
			var outputFile = new MediaFile { Filename = $@".\Converted\{filename}.mp3" };

			using (var engine = new Engine())
			{
				engine.Convert(inputFile, outputFile);
			}

			
			_progressBar.Value = 100;
			
		}


		
	}
}
