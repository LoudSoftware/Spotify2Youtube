using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spotify2Youtube.Exceptions;
using Spotify2Youtube.Helpers;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace Spotify2Youtube
{
	public partial class WebControl : Form
	{
		private SpotifyWebAPI _spotify;

		// Grabbing the custom config and the neccessary API keys from it
		private static readonly Configuration Config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
		private static readonly string Clientid = Config.AppSettings.Settings["spotifyApi"].Value;


		// This is some magic voodoo shit I found on SOF this kinda fixes the flickering.
		protected override CreateParams CreateParams
		{
			get
			{
				var cp = base.CreateParams;
				cp.ExStyle = cp.ExStyle | 0x2000000;
				return cp;
			}
		}

		private List<FullTrack> SavedTracks { get; set; }

		public WebControl()
		{
			InitializeComponent();

			savedTracksListView.MouseClick += SavedTracksListView_MouseClick;
			savedTracksListView.MouseMove += SavedTracksListView_MouseMove;


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


			SavedTracks = await GetSavedTracksAsync();


			savedTracksCountLabel.Text = SavedTracks.Count.ToString();
			SavedTracks.ForEach(track =>
			{
				savedTracksListView.Items.Add(new ListViewItem()
				{
					Tag = track,
					Text = track.Name,
					SubItems =
					{
						string.Join(", ",
							track.Artists.Select(source => source.Name)),
						track.Album.Name,
						track.Uri
					}
				});
			});

			foreach (ListViewItem item in savedTracksListView.Items)
			{
				item.UseItemStyleForSubItems = false;
				item.SubItems[3].ForeColor = Color.Blue;
			}
		}


		/*
		 */
		/// <summary>
		/// Starts a youtube search with the given query
		/// </summary>
		/// <param name="query">The search query</param>
		/// <returns>A list containning the search results</returns>
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
			var savedTracks = await _spotify.GetSavedTracksAsync();
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
			Debug.WriteLine("Started authentication Task.");
			var webApiFactory = new WebAPIFactory(
				"http://localhost",
				8000,
				Clientid,
				Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
				Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead |
				Scope.PlaylistReadCollaborative |
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


		/*
		 * Starts the a youtube search when the search youtube button is clicked
		 */
		private async void BtnYoutubeSearch_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in savedTracksListView.Items)
			{
				var subItems = item.SubItems;

				var title = item.Text;
				var artist = subItems[1].Text;


				try
				{
					Debug.WriteLine("Searchig for: " + title + " " + artist);

					item.SubItems.Add(await SearchYoutube(title + " " + artist)); // TODO figure out if we can skip this await call

					item.UseItemStyleForSubItems = false;
					item.SubItems[4].ForeColor = Color.Blue;
					item.SubItems[4].Font =
						new Font(item.SubItems[4].Font.FontFamily, item.SubItems[4].Font.Size, FontStyle.Underline);
				}
				catch (YoutubeSearchNotFoundException ex)
				{
					Debug.WriteLine(ex.Message);
					item.BackColor = Color.Red;
				}
			}
		}


		/*
		 * Shows a hand cursor on clickable links if there are any
		 */
		private void SavedTracksListView_MouseMove(object sender, MouseEventArgs e)
		{
			var hit = savedTracksListView.HitTest(e.Location);
			if (hit.SubItem != null && (hit.SubItem == hit.Item.SubItems[3] || hit.Item.SubItems.Count == 5))
			{
				savedTracksListView.Cursor = Cursors.Hand;
			}
			else
			{
				savedTracksListView.Cursor = Cursors.Default;
			}
		}


		/*
		 * Gets the URL the user clicked on and opens the browser or the app with the URL
		 */
		private void SavedTracksListView_MouseClick(object sender, MouseEventArgs e)
		{
			var hit = savedTracksListView.HitTest(e.Location);
			if (hit.SubItem != null && hit.SubItem == hit.Item.SubItems[3])
			{
				var url = new Uri(hit.SubItem.Text);
				Process.Start(url.ToString());
			}
			else if (hit.SubItem != null && hit.Item.SubItems.Count == 5)
			{
				if (hit.SubItem != hit.Item.SubItems[4]) return;
				var url = new Uri("https://www.youtube.com/watch?v=" + hit.SubItem.Text);
				Process.Start(url.ToString());
			}
		}


		private async void DownloadAllBtn_Click(object sender, EventArgs e)
		{
			DownloadProgressBar.Maximum = 100;
			DownloadProgressBar.Step = 1;

			var progress = new Progress<double>(p => DownloadProgressBar.Value = (int) Math.Round(p * 85));


			foreach (ListViewItem item in savedTracksListView.Items)
			{
				var title = item.Text;
				var artist = item.SubItems[1].Text;

				DownloadingLabel.Text = $@"Downloading and converting: {title} by {artist}";

				string id;

				try
				{
					id = item.SubItems[4].Text;
				}
				catch (ArgumentException exception)
				{
					Debug.WriteLine(exception.Message);
					continue;
				}

				await Task.Run(() => new YoutubeDownload(progress, id, (FullTrack) item.Tag).Download());

				DownloadProgressBar.Value = 100;
			}
		}

		#endregion
	}
}