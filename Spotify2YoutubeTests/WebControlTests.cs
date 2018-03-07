using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify2Youtube;
using Spotify2Youtube.Helpers;
using SpotifyAPI.Web.Models;

namespace Spotify2YoutubeTests
{
	[TestClass]
	public class WebControlTests
	{
		[TestMethod]
		public void TestDownloadSingleFile()
		{
			Image[] images = new Image[3];
			images[0] = new Image
			{
				Height = 640,
				Width = 640,
				Url = "https://i.scdn.co/image/46060b38317279b2435850644a97578679a10abe"
			};

			var album = new SimpleAlbum
			{
				Id = "111",
				Name = "Album Name",
				Type = "Single"
			};

			var fullAlbum = new FullAlbum
			{
				Id = "111",
				
			}

			var track = new FullTrack
			{
				Id = "111",

			};

			
			YoutubeDownload.Download(progress, id, track, fullAlbum).Wait();
		}
	}
}
