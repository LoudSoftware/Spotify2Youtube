using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using SpotifyAPI.Web.Models;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace Spotify2Youtube.Helpers
{
	internal class YoutubeDownload
	{
		private readonly string _id;
		private readonly string _title;
		private readonly string[] _artists;

		private readonly Progress<double> _progress;
		private readonly FullTrack _track;


		public YoutubeDownload(Progress<double> progress,string videoId, FullTrack track)
		{
			_track = track;

			_id = videoId;
			_title = track.Name;
			_artists = track.Artists.Select(source => source.Name).ToArray();

			_progress = progress;

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


			var artists = string.Join(",", _artists);

			var filename = $"{_title} - {artists}";

			await client.DownloadMediaStreamAsync(streamInfo, $@".\Downloads\{filename}.{ext}", _progress);

			Debug.WriteLine("Download complete!");
			Debug.WriteLine("Now converting the file");
			ConvertToMp3(filename, ext);


		}

		private void ConvertToMp3(string filename, string ext)
		{
			

			var inputFile = new MediaFile { Filename = $@".\Downloads\{filename}.{ext}" };
			var outputFile = new MediaFile { Filename = $@".\Converted\{filename}.mp3" };

			using (var engine = new Engine())
			{
				engine.Convert(inputFile, outputFile);
			}

			TagMp3.Tag(outputFile, _track);
		}


		
	}
}
