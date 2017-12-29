using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Spotify2Youtube.Exceptions;
using Xabe.FFmpeg;

namespace Spotify2Youtube.Helpers
{
	public static class FileConverter
	{
		public static async Task<bool> ConvertToMp3(string inputFile, string outputFile)
		{
			bool result;
			try
			{
				TestExistence(inputFile, outputFile);

				var assemblyLocation = Assembly.GetExecutingAssembly().Location;
				FFbase.FFmpegDir = Path.GetDirectoryName(assemblyLocation);

				result = await new Conversion()
					.SetInput(inputFile)
					.SetOutput(outputFile)
					.Start();


			}
			catch (FileAlreadyConvertedException e)
			{
				Console.WriteLine(e);
				return true;
			}

			return result;

		}

		private static void TestExistence(string inputFile, string outputFile)
		{
			if (File.Exists(outputFile))
			{
				throw new FileAlreadyConvertedException($"The file {inputFile} has already been encoded in {outputFile}");
			}
		}
	}
}