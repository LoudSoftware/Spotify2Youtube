using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Spotify2Youtube.Exceptions;
using static Google.Apis.YouTube.v3.SearchResource.ListRequest;

namespace Spotify2Youtube.Helpers
{
	/// <summary>
	/// YouTube Data API v3 sample: search by keyword.
	/// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
	/// See https://developers.google.com/api-client-library/dotnet/get_started
	///
	/// Set ApiKey to the API key value from the APIs & auth > Registered apps tab of
	///   https://cloud.google.com/console
	/// Please ensure that you have enabled the YouTube Data API for your project.
	/// </summary>
	internal class YoutubeSearch
	{
		private readonly string _query;

		public YoutubeSearch(string queryString)
		{
			Debug.WriteLine("YouTube Data API: Search");
			Debug.WriteLine("========================");

			_query = queryString;
		}

		public async Task<List<string>> Run()
		{
			var youtubeService = new YouTubeService(new BaseClientService.Initializer()
			{
				ApiKey = "AIzaSyBC1TEfgLkM8rIbPybAs4XosJ3yxO_yGO4",
				ApplicationName = GetType().ToString()
			});

			var searchListRequest = youtubeService.Search.List("snippet");
			searchListRequest.Q = _query;
			searchListRequest.MaxResults = 1;
			searchListRequest.VideoDuration =
				VideoDurationEnum.Short__; // TODO make a second search list for medium songs and combine the 2
			searchListRequest.Type = "video";


			// Search for Short Length videos
			var searchListResponse = await searchListRequest.ExecuteAsync();

			// Add each result to the appropriate list, and then display the lists of matching videos.
			var shortVideos = (from searchResult in searchListResponse.Items
				where searchResult.Id.Kind == "youtube#video"
				select $"{searchResult.Id.VideoId}").ToList();


			// Search for Medium Length videos
			searchListRequest.VideoDuration = VideoDurationEnum.Medium;
			searchListResponse = await searchListRequest.ExecuteAsync();

			var mediumVideos = (from searchResult in searchListResponse.Items
				where searchResult.Id.Kind == "youtube#video"
				select $"{searchResult.Id.VideoId}").ToList();


			// Merging the 2 searches starting with the medium length videos
			var videos = mediumVideos.Concat(shortVideos).ToList();

			// Printing the search results to the Debug Log
			Debug.WriteLine($"Videos:\n{string.Join("\n", shortVideos)}\n");


			if (videos.Count != 0) return videos;
			videos.Add("notFound");
			throw new YoutubeSearchNotFoundException($"Could not find: {_query} on YouTube");
		}
	}
}