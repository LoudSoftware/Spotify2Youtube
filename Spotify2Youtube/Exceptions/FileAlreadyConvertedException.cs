using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify2Youtube.Exceptions
{
	class FileAlreadyConvertedException : Exception
	{
		public FileAlreadyConvertedException(string message) : base(message)
		{
		}
	}
}
