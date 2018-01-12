using System;

namespace Spotify2Youtube.Exceptions
{
	class FileAlreadyConvertedException : Exception
	{
		public FileAlreadyConvertedException(string message) : base(message)
		{
		}
	}
}
