using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weigelt.SourceToHtml
{
	/// <summary>
	/// Contains the information specified on the command line,
	/// normalized (full paths) and completed (default output file path).
	/// </summary>
	public class CommandLineArguments
	{
		/// <summary>
		/// Gets or sets the full path of the input file.
		/// </summary>
		public string InputFile { get; set; }

		/// <summary>
		/// Gets or sets the full path output file.
		/// </summary>
		public string OutputFile { get; set; }
	}
}
