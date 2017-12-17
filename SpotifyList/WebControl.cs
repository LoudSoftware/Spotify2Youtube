using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helpers;

namespace SpotifyList
{
    public partial class WebControl : Form
    {
        private SpotifyWebAPI _spotify;

        private PrivateProfile _profile;
	    private string _CLIENTID = "18b96889c87947fc98a5e436d7bdc613";

        public List<FullTrack> SavedTracks { get; set; }

	    public WebControl()
        {
            InitializeComponent();

            SavedTracks = new List<FullTrack>();

        }

        private async void InitialSetup()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(InitialSetup));
                return;
            }

            btnAuth.Enabled = false;
            _profile = await _spotify.GetPrivateProfileAsync();

            SavedTracks = await GetSavedTracksAsync();


            savedTracksCountLabel.Text  = SavedTracks.Count.ToString();
            SavedTracks.ForEach(track => savedTracksListView.Items.Add(new ListViewItem()
            {
                Text = track.Name,
                SubItems =
                    {
                    string.Join(",",
                    track.Artists.Select(source => source.Name)),
                    track.Album.Name,
					track.Uri
                    }
            }));

	        foreach (ListViewItem item in savedTracksListView.Items)
	        {
		        item.UseItemStyleForSubItems = false;
		        item.SubItems[3].ForeColor = Color.Blue;
	        }
		}

        /// <summary>
        /// Starts a youtube search with the given query
        /// </summary>
        /// <param name="query">The string we want to search for</param>
        /// <returns></returns>
        private static async Task<string> SearchYoutube(string query)
        {
            

            var results = new List<string>();

            try
            {
                results = await new YoutubeSearch(query).Run();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Debug.WriteLine("Error: " + e.Message);
                }
            }

            var firstResult = results.First();
            Debug.WriteLine(firstResult);

            return results.First();
           

        }



        /// <summary>
        /// Gets all the authenticated user's saved tracks
        /// </summary>
        /// <returns>The list of all saved tracks</returns>
        private async Task<List<FullTrack>> GetSavedTracksAsync()
        {
            var savedTracks = await _spotify.GetSavedTracksAsync(20);
            var list = savedTracks.Items.Select(track => track.Track).ToList();

            while (savedTracks.Next != null)
            {
                savedTracks = await _spotify.GetSavedTracksAsync(20, savedTracks.Offset + savedTracks.Limit);
                list.AddRange(savedTracks.Items.Select(track => track.Track));
            }

            return list;
        }

        /// <summary>
        /// Runs the authentication process
        /// </summary>
        private async void RunAuthentication()
        {
            //TODO needs to be seperated in it's own class and have it save whatever we need to be able to no have to do this every time.
            Debug.WriteLine("Started authentication Task.");
            var webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                _CLIENTID,
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);


            try
            {
                _spotify = await webApiFactory.GetWebApi();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            if (_spotify == null)
            {
                Debug.WriteLine("_spotify is null");
                return;
            }


            InitialSetup();
        }



		#region UI action Listeners

		private void BtnAuth_Click(object sender, EventArgs e)
	    {
		    Task.Run(() => RunAuthentication());

	    }

		/// <summary>
		/// Starts the a youtube search when the search youtube button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtnYoutubeSearch_Click(object sender, EventArgs e)
        {
            
            foreach (ListViewItem item in savedTracksListView.Items)
            {
                var subItems = item.SubItems;

                var title = item.Text;
                var artist = subItems[1].Text;

	            item.SubItems.Add(await SearchYoutube(title + " " + artist));
//                item.SubItems[4].Text =  await SearchYoutube(title + " " + artist);

				Debug.WriteLine("Searchig for: " + title + " " + artist);

	            item.UseItemStyleForSubItems = false;
	            item.SubItems[4].ForeColor = Color.Blue;
	            item.SubItems[4].Font = new Font(item.SubItems[4].Font.FontFamily, item.SubItems[4].Font.Size, FontStyle.Underline);

            }
            
        }

        private void SavedTracksListView_MouseMove(object sender, MouseEventArgs e)
        {
            var hit = savedTracksListView.HitTest(e.Location);
            if (hit.SubItem != null && (hit.SubItem == hit.Item.SubItems[3] || hit.SubItem == hit.Item.SubItems[4]))
            {

				savedTracksListView.Cursor = Cursors.Hand;
            }
            else
            {
	            savedTracksListView.Cursor = Cursors.Default;
            }

        }


		private void SavedTracksListView_MouseClick(object sender, MouseEventArgs e)
	    {
			var hit = savedTracksListView.HitTest(e.Location);
		    if (hit.SubItem != null && hit.SubItem == hit.Item.SubItems[3])
		    {
			    var url = new Uri(hit.SubItem.Text);
			    System.Diagnostics.Process.Start(url.ToString());
		    }
		    else if (hit.SubItem != null && hit.SubItem == hit.Item.SubItems[4])
		    {
			    var url = new Uri("https://www.youtube.com/watch?v=" + hit.SubItem.Text);
			    System.Diagnostics.Process.Start(url.ToString());

		    }
		}
		#endregion
	}
}
