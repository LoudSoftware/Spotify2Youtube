using System;

namespace Spotify2Youtube.Exceptions
{
	[Serializable()]
	public class YoutubeSearchNotFoundException : Exception
	{
		public YoutubeSearchNotFoundException()
		{
		}

		public YoutubeSearchNotFoundException(string query) : base($"Warning: Could not find {query} on YouTube")
		{
		}

		// A constructor is needed for serialization when an
		// exception propagates from a remoting server to the client. 
		protected YoutubeSearchNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}