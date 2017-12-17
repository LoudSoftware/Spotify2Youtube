﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Helpers
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
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = _query; // Replace with your search term.
            searchListRequest.MaxResults = 4;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();


			// Add each result to the appropriate list, and then display the lists of matching videos.
			var videos = (from searchResult in searchListResponse.Items where searchResult.Id.Kind == "youtube#video" select $"{searchResult.Id.VideoId}").ToList();

			Debug.WriteLine($"Videos:\n{string.Join("\n", videos)}\n");

            return videos;
        }
    }
}