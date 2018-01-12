using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify2Youtube.Helpers;
using Spotify2Youtube.Exceptions;

namespace Spotify2YoutubeTests.Helpers
{
	[TestClass]
	public class YoutubeSearchTests
	{
		[TestMethod]
		public async Task SearchTest()
		{
			const string title = "Random";
			const string artist = "random";
			var query = ($"{artist} {title}");

			Debug.WriteLine($"Searchig for: {query}");
			try
			{
				var result = await new YoutubeSearch(query).Run();
				Assert.IsNotNull(result);
			}
			catch (YoutubeSearchNotFoundException e)
			{
				Assert.Fail();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				Debug.WriteLine(e.StackTrace);
//				Process.Start($"https://stackoverflow.com/search?q={e.Message}");

			}
		}
	}
}
