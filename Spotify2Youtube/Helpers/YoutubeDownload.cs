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
	internal static class YoutubeDownload
	{
		public static async Task Download(Progress<double> progress, string videoId, FullTrack track, FullAlbum album)
		{
			var id = videoId;
			var title = track.Name;
			var artistsArray = track.Artists.Select(source => source.Name).ToArray();


			var client = new YoutubeClient();
			Debug.WriteLine($"Now finding stream info Set for {id}");

			var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(id);

			var streamInfo = streamInfoSet.Audio.WithHighestBitrate();
			var ext = streamInfo.Container.GetFileExtension();


			var artists = string.Join(",", artistsArray);

			var filename = $"{title} - {artists}";


			var inputFile = $@"{Settings.Default.DownloadPath}\{filename}.{ext}";
			Debug.WriteLine($"Input FilePath: {inputFile}");
			var outputFile = $@"{Settings.Default.ConvertedPath}\{filename}.mp3";
			Debug.WriteLine($"Output Filepath: {outputFile}");


			await client.DownloadMediaStreamAsync(streamInfo, $@"{Settings.Default.DownloadPath}\{filename}.{ext}", progress);

			Debug.WriteLine($"Download complete for: {outputFile}...");
			Debug.WriteLine($"Now converting the file: {outputFile}...");


			await Task.Run(async () => await FileConverter.ConvertToMp3(inputFile, outputFile));

			await Task.Run(() => TagMp3.Tag(outputFile, track, album));
		}
	}
}