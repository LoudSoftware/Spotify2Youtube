using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using SpotifyAPI.Web.Models;
using TagLib;
using File = System.IO.File;

namespace Spotify2Youtube.Helpers
{
	public static class TagMp3
	{
		public static void Tag(string inputFile, FullTrack track, FullAlbum album)
		{
			var file = TagLib.File.Create(inputFile);


			file.Tag.Title = track.Name;
			file.Tag.Performers = track.Artists.Select(source => source.Name).ToArray();
			file.Tag.AlbumArtists = track.Artists.Select(source => source.Name).ToArray();
			file.Tag.Album = album.Name;
			file.Tag.Genres = album.Genres.ToArray();
			file.Tag.Year = Convert.ToUInt32(album.ReleaseDate.Remove(4));
			file.Tag.Comment = track.ExternUrls["spotify"];


			var albumPictures = album.Images;
			var pictures = new List<IPicture>(albumPictures.Count);
			foreach (var albumPicture in albumPictures)
			{
				pictures.Add(new Picture(DownloadRemoteImageFile(albumPicture.Url, file.Name)));
			}



			file.Tag.Pictures = pictures.ToArray();
			// Save Changes:
			file.Save();
		}

		private static string DownloadRemoteImageFile(string uri, string fileName)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			// Check that the remote file was found. The ContentType
			// check is performed since a request for a non-existent
			// image file might be redirected to a 404-page, which would
			// yield the StatusCode "OK", even though the image was not
			// found.
			if ((response.StatusCode == HttpStatusCode.OK ||
			     response.StatusCode == HttpStatusCode.Moved ||
			     response.StatusCode == HttpStatusCode.Redirect) &&
			    response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
			{

				// if the remote file was found, download oit
				using (Stream inputStream = response.GetResponseStream())
				using (Stream outputStream = File.OpenWrite(fileName))
				{
					byte[] buffer = new byte[4096];
					int bytesRead;
					do
					{
						bytesRead = inputStream.Read(buffer, 0, buffer.Length);
						outputStream.Write(buffer, 0, bytesRead);
					} while (bytesRead != 0);
				}

				return fileName;
			}

			return null;
		}
	}
}
