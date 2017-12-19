﻿using System.Linq;
using MediaToolkit.Model;
using SpotifyAPI.Web.Models;

namespace Spotify2Youtube.Helpers
{
	public static class TagMp3
	{
		public static void Tag(MediaFile mediaFile, FullTrack track)
		{
			var file = TagLib.File.Create(mediaFile.Filename);


			file.Tag.Title = track.Name;
			file.Tag.AlbumArtists = track.Artists.Select(source => source.Name).ToArray();
			file.Tag.Album = track.Album.Name;
			
			// Save Changes:
			file.Save();
		}
	}
}
