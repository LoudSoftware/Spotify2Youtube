using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Spotify2Youtube.Properties;
using SpotifyAPI.Web.Models;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace Spotify2Youtube.Helpers
{
	internal class YoutubeDownload
	{
		public static async Task Download(Progress<double> progress, string videoId, FullTrack track)
		{
			string id;
			string title;
			string[] _artists;

			Progress<double> _progress;
			FullTrack _track;


			_track = track;

			id = videoId;
			title = track.Name;
			_artists = track.Artists.Select(source => source.Name).ToArray();
			_progress = progress;

			var client = new YoutubeClient();
			Debug.WriteLine($"Now finding stream info Set for {id}");

			var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(id);

			var streamInfo = streamInfoSet.Audio.WithHighestBitrate();
			var ext = streamInfo.Container.GetFileExtension();


			var artists = string.Join(",", _artists);

			var filename = $"{title} - {artists}";


			var inputFile = $@"{Settings.Default.DownloadPath}\{filename}.{ext}";
			Debug.WriteLine($"Input FilePath: {inputFile}");
			var outputFile = $@"{Settings.Default.ConvertedPath}\{filename}.mp3";
			Debug.WriteLine($"Output Filepath: {outputFile}");


			await client.DownloadMediaStreamAsync(streamInfo, $@"{Settings.Default.DownloadPath}\{filename}.{ext}", _progress);

			Debug.WriteLine("Download complete!");
			Debug.WriteLine("Now converting the file");


			await Task.Run(async () => await FileConverter.ConvertToMp3(inputFile, outputFile));

			await Task.Run(() => TagMp3.Tag(outputFile, _track));
		}
	}
}