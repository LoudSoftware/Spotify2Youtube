using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
	public class YoutubeSearch
	{
		private readonly string _query;

		// Grabbing the custom config and the neccessary API keys from it
		private static readonly Configuration Config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
		private static readonly string YoutubeApi = Config.AppSettings.Settings["youtubeApi"].Value;

		public YoutubeSearch(string queryString)
		{
			_query = queryString;
			Debug.WriteLine($"========== Begin Search for {_query} ==========");
		}

		public async Task<List<string>> Run()
		{

			var youtubeService = new YouTubeService(new BaseClientService.Initializer()
			{
				ApiKey = YoutubeApi,
				ApplicationName = GetType().ToString()
			});


			List<string> mediumVideos;
			List<string> shortVideos;


			var searchListRequest = youtubeService.Search.List("snippet");
			searchListRequest.Q = _query;
			searchListRequest.MaxResults = 1;
			searchListRequest.VideoDuration =
				VideoDurationEnum.Medium;
			searchListRequest.Type = "video";


			// Search for Medium Length videos
			var searchListResponse = await searchListRequest.ExecuteAsync();

			// Add each result to the appropriate list, and then display the lists of matching videos.
			mediumVideos = (from searchResult in searchListResponse.Items
				where searchResult.Id.Kind == "youtube#video"
				select $"{searchResult.Id.VideoId}").ToList();


			// If medium empty, search for Short Length videos
			if (mediumVideos.Count != 0)
			{
				return mediumVideos;
			}

			searchListRequest.VideoDuration = VideoDurationEnum.Medium;
			searchListResponse = await searchListRequest.ExecuteAsync();

			shortVideos = (from searchResult in searchListResponse.Items
				where searchResult.Id.Kind == "youtube#video"
				select $"{searchResult.Id.VideoId}").ToList();


			// Printing the search results to the Debug Log
			Debug.WriteLine($"Videos:\n{string.Join("\n", shortVideos)}\n");


			if (shortVideos.Count != 0) return shortVideos;
			throw new YoutubeSearchNotFoundException(_query);
		}
	}
}