using System;
using System.IO;
using System.Text;

namespace Weigelt.SourceToHtml
{
	/// <summary>
	/// Simple command line tool using <see cref="SourceToHtml"/>.
	/// </summary>
	internal static class Program
	{
		private const string _HeaderText = "s2h - Source To Html";
		private const string _UsageText = "Usage: s2h inputfile.ext [outputfile.html]";

		static void Main(string[] args)
		{
			try
			{
				var commandLineArguments = GetCommandLineArguments(args);
				SourceToHtmlSettings settings;
				switch (Path.GetExtension(commandLineArguments.InputFile))
				{
					case ".js":
						settings = CreateSettings.ForJavaScript;
						break;
					case ".ts":
						settings = CreateSettings.ForTypeScript;
						break;
					case ".cs":
						settings = CreateSettings.ForCSharp;
						break;
					case ".json":
						settings = CreateSettings.ForJson;
						break;
					default:
						settings = CreateSettings.ForOther;
						break;
				}
				var sourceToHtml = new SourceToHtml(settings);
				var sourceText = File.ReadAllText(commandLineArguments.InputFile, Encoding.Default);
				var resultHtml = sourceToHtml.GetHtml(sourceText);
				var fileHtml = Properties.Resources.HtmlFileTemplate.Replace("$(HTML)", resultHtml);
				File.WriteAllText(commandLineArguments.OutputFile, fileHtml, Encoding.UTF8);
				Environment.ExitCode = 0;
			}
			catch (Exception exception)
			{
				Console.WriteLine(_HeaderText);
				if (!String.IsNullOrEmpty(exception.Message))
					Console.WriteLine(exception.Message);
				Console.WriteLine(_UsageText);
				Environment.ExitCode = 1;
			}
		}

		private static CommandLineArguments GetCommandLineArguments(string[] args)
		{
			if (args.Length == 0)
				throw new Exception("No input file specified.");
			var inputFilePath = Path.GetFullPath(args[0]);
			if (!File.Exists(inputFilePath))
				throw new Exception($"Cannot read input file {inputFilePath}");
			string outputFilePath;
			if (args.Length > 1)
			{
				outputFilePath = Path.GetFullPath(args[1]);
			}
			else
			{
				outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath) ?? Path.GetPathRoot(inputFilePath),Path.GetFileNameWithoutExtension(inputFilePath)) + ".html";
			}
			return new CommandLineArguments
			{
				InputFile = inputFilePath,
				OutputFile = outputFilePath
			};
		}
	}
}
