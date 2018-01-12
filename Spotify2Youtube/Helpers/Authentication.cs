using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;

namespace Spotify2Youtube.Helpers
{
	public class Authentication
	{
		private readonly WebControl _webControl;

		// Grabbing the custom config and the neccessary API keys from it
		private static readonly Configuration Config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
		private static readonly string Clientid = Config.AppSettings.Settings["spotifyApi"].Value;

		public Authentication(WebControl webControl)
		{
			_webControl = webControl;
		}

		/// <summary>
		/// Runs the authentication process
		/// </summary>
		public async void RunAuthentication()
		{
			Debug.WriteLine("Started authentication Task.");
			var webApiFactory = new WebAPIFactory(
				"http://localhost",
				8000, Clientid,
				Scope.UserLibraryRead);

			try
			{
				_webControl.Spotify = await webApiFactory.GetWebApi();
				_webControl.InitialSetup();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}

			if (_webControl.Spotify != null) return;
			Debug.WriteLine("_spotify is null");
		}
	}
}