using System;

namespace Spotify2Youtube.Exceptions
{
	[Serializable()]
	internal class YoutubeSearchNotFoundException : Exception
	{
		public YoutubeSearchNotFoundException()
		{
		}

		public YoutubeSearchNotFoundException(string message) : base(message)
		{
		}

		// A constructor is needed for serialization when an
		// exception propagates from a remoting server to the client. 
		protected YoutubeSearchNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}