﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using MediaToolkit;
using MediaToolkit.Model;
using Spotify2Youtube.Configs;
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
		}

		public async Task Download()
		{
			var client = new YoutubeClient();

			var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(_id);

			var streamInfo = streamInfoSet.Audio.WithHighestBitrate();
			var ext = streamInfo.Container.GetFileExtension();


			var artists = string.Join(",", _artists);

			var filename = $"{_title} - {artists}";

			var inputFile = $@"{MainConfig.DownloadDir}\{filename}.{ext}";
			var outputFile = $@"{MainConfig.ConvertedDir}\{filename}.mp3";


			await client.DownloadMediaStreamAsync(streamInfo, $@"{MainConfig.DownloadDir}\{filename}.{ext}", _progress);

			Debug.WriteLine("Download complete!");
			Debug.WriteLine("Now converting the file");


			await Task.Run(async () => await FileConverter.ConvertToMp3(inputFile, outputFile));

			await Task.Run(() => TagMp3.Tag(outputFile, _track));

		}
	}
}