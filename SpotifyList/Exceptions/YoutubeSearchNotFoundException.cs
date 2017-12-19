using System;

namespace Spotify2Youtube.Exceptions
{
	[Serializable()]
	internal class YoutubeSearchNotFoundException : Exception
	{
		public YoutubeSearchNotFoundException(string message) : base(message)
		{
		}

		// A constructor is needed for serialization when an
		// exception propagates from a remoting server to the client. 
		protected YoutubeSearchNotFoundException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
		{
		}
	}
}