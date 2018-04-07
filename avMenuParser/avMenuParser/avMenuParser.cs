using System;
using System.IO;
using XmlParser;

namespace avMenuParser
{
	class avMenuParser
	{
		static void Main(string[] args)
		{
			if(args.Length < 2)
			{
				WriteError("Arguments missing, please provide the correct arguments.");
				Console.ReadLine();
				return;
			}

			var menuFilePath = args[0];
			var pathToMatch = args[1];

			if (HasInputErrors(menuFilePath, pathToMatch))
			{
				Console.ReadLine();
				return;
			}

			var displayString = XmlParser.XmlParser.ParseXml(menuFilePath, pathToMatch);

			//placeholder
			WriteError("No errors");
			Console.ReadLine();
		}

		private static bool HasInputErrors(string menuFilePath, string pathToMatch)
		{
			var hasErrors = false;
			var filePath = menuFilePath;

			if (string.IsNullOrEmpty(menuFilePath))
			{
				WriteError("No file path");
				filePath = "Entered file path";
				hasErrors = true;
			}

			if (string.IsNullOrEmpty(pathToMatch))
			{
				WriteError("No path for matching");
				hasErrors = true;
			}

			if (!File.Exists(menuFilePath))
			{
				WriteError(filePath + " does not exist, please provide the correct path to your file.");
				hasErrors = true;
			}

			return hasErrors;
		}

		private static void WriteError(string error)
		{
			var logError = DateTime.Now + ": " + error + '\n';
			File.AppendAllText("log.txt", logError);
			Console.WriteLine(error);
		}
	}
}
