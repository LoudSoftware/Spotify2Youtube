using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify2Youtube.Helpers;
using Xabe.FFmpeg;


namespace Spotify2YoutubeTests.Helpers
{
	[TestClass]
	public class FileConverterTests
	{
		[TestMethod]
		public async Task ConvertToMp3Test()
		{
			Debug.WriteLine(FFbase.FFmpegDir);

			const string inputFile = @"C:\Users\nicoz\Music\The #FRIENDZONE.mp3";
			const string outputFile = @"C:\Users\nicoz\Music\The #FRIENDZONE.aac";

			var result = await FileConverter.ConvertToMp3(inputFile, outputFile);

			if (!result) Assert.Fail();
		}
	}
}