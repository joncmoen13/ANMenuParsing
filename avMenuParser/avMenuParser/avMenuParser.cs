using System;
using System.IO;
using Parser = XmlParser.XmlParser;

namespace avMenuParser
{
	internal class AvMenuParser
	{
		/// <summary>
		/// Console App to parse xml into a display of menu items.
		/// </summary>
		/// <param name="args"></param>
		private static void Main(string[] args)
		{
			if(args.Length < 2)
			{
				WriteAndLogError("Arguments missing, please provide the correct arguments.");
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

			var displayString = Parser.ParseXml(menuFilePath, pathToMatch);

			Console.WriteLine(displayString);
			WriteAndLogError("");
			Console.ReadLine();
		}

		/// <summary>
		/// Determines if there are any issues with the argument inputs
		/// </summary>
		/// <param name="menuFilePath"></param>
		/// <param name="pathToMatch"></param>
		/// <returns>true if there are any input errors, false otherwise</returns>
		private static bool HasInputErrors(string menuFilePath, string pathToMatch)
		{
			var hasErrors = false;
			var filePath = menuFilePath;

			if (string.IsNullOrEmpty(menuFilePath))
			{
				WriteAndLogError("No file path");
				filePath = "Entered file path";
				hasErrors = true;
			}

			if (string.IsNullOrEmpty(pathToMatch))
			{
				WriteAndLogError("No path for matching");
				hasErrors = true;
			}

			if (!File.Exists(menuFilePath))
			{
				WriteAndLogError(filePath + " does not exist, please provide the correct path to your file.");
				hasErrors = true;
			}

			return hasErrors;
		}

		/// <summary>
		/// Writes the error to console and records the error in a log
		/// </summary>
		/// <param name="error"></param>
		private static void WriteAndLogError(string error)
		{
			if (!string.IsNullOrEmpty(error))
				Console.WriteLine(error);
			else
				error = "Success!";

			var logError = DateTime.Now + ": " + error + '\n';
			File.AppendAllText("log.txt", logError);
		}
	}
}
