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
        private List<FullTrack> _savedTracks;
        private string _CLIENTID = "18b96889c87947fc98a5e436d7bdc613";

        public List<FullTrack> SavedTracks { get => _savedTracks; set => _savedTracks = value; }

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

            _savedTracks = await GetSavedTracksAsync();


            savedTracksCountLabel.Text  = _savedTracks.Count.ToString();
            _savedTracks.ForEach(track => savedTracksListView.Items.Add(new ListViewItem()
            {
                Text = track.Name,
                SubItems =
                    {
                    string.Join(",",
                    track.Artists.Select(source => source.Name)),
                    track.Album.Name,
                    track.Uri,
                    "null"
                    }
            }));
        }

        /// <summary>
        /// Starts a youtube search with the given query
        /// </summary>
        /// <param name="query">The string we want to search for</param>
        /// <returns></returns>
        private async Task<string> SearchYoutube(string query)
        {
            

            List<string> results = new List<string>();

            try
            {
                results = await new YoutubeSearch(query).Run();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            string firstResult = results.First();
            Debug.WriteLine(firstResult);

            return results.First();
           

        }



        /// <summary>
        /// Gets all the authenticated user's saved tracks
        /// </summary>
        /// <returns>The list of all saved tracks</returns>
        private async Task<List<FullTrack>> GetSavedTracksAsync()
        {
            Paging<SavedTrack> savedTracks = await _spotify.GetSavedTracksAsync(20);
            List<FullTrack> list = savedTracks.Items.Select(track => track.Track).ToList();

            while (savedTracks.Next != null)
            {
                savedTracks = await _spotify.GetSavedTracksAsync(20, savedTracks.Offset + savedTracks.Limit);
                list.AddRange(savedTracks.Items.Select(track => track.Track));
            }

            return list;
        }

        private void BtnAuth_Click(object sender, EventArgs e)
        {
            Task.Run(() => RunAuthentication());
            
        }

        /// <summary>
        /// Runs the authentication process
        /// </summary>
        private async void RunAuthentication()
        {
            //TODO needs to be seperated in it's own class and have it save whatever we need to be able to no have to do this every time.
            Console.WriteLine("Started authentication Task.");
            WebAPIFactory webAPIFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                _CLIENTID,
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);


            try
            {
                _spotify = await webAPIFactory.GetWebApi();
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

                string title = item.Text;
                string artist = subItems[0].Text;

                item.SubItems[4].Text =  await SearchYoutube(title + " " + artist);
            }
            
        }

        private void SavedTracksListView_MouseMove(object sender, MouseEventArgs e)
        {
            var hit = savedTracksListView.HitTest(e.Location);
            if (hit.SubItem != null && 
                (hit.SubItem == hit.Item.SubItems[3] |
                hit.SubItem == hit.Item.SubItems[4] && hit.SubItem.Text != "null"))
            {
                savedTracksListView.Cursor = Cursors.Hand;
            }
            else
            {
                savedTracksListView.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// Generates links on the fly when mouse over a valid link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavedTracksListView_MouseUp(object sender, MouseEventArgs e)
        {
            var hit = savedTracksListView.HitTest(e.Location);
            if (hit.SubItem != null && hit.SubItem == hit.Item.SubItems[1])
            {
                var url = new Uri(hit.SubItem.Text);
                // etc..
            }
        }
    }
}
