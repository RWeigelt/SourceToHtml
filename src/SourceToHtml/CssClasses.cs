using System;

namespace Weigelt.SourceToHtml
{
	/// <summary>
	/// Contains the configuration for the CSS classes to be used for keywords, identiers, etc.
	/// </summary>
	public class CssClasses
	{
		/// <summary>
		/// Gets or sets the CSS class for keywords.
		/// </summary>
		/// <value>
		/// Default is "srcKeyword"; set this to empty if you don't want keywords to be surrounded by a span tag.
		/// </value>
		public string Keyword { get; set; }

		/// <summary>
		/// Gets or sets the CSS class for identifiers.
		/// </summary>
		/// <value>
		/// Default is empty, so normal identifiers (i.e. non-keywords) are not surrounded by a span tag.
		/// </value>
		public string Identifier { get; set; }

		/// <summary>
		/// Gets or sets the CSS class for comments.
		/// </summary>
		/// <value>
		/// Default is "srcComment"; set this to empty if you don't want comments to be surrounded by a span tag.
		/// </value>
		public string Comment { get; set; }

		/// <summary>
		/// Gets or sets the CSS class for text literals.
		/// </summary>
		/// <value>
		/// Default is "srcTextLiteral"; set this to empty if you don't want text literals to be surrounded by a span tag.
		/// </value>
		public string TextLiteral { get; set; }

		/// <summary>
		/// Gets or sets the CSS class for the first text literal (if it should be different).
		/// </summary>
		/// <value>
		/// Default is empty, i.e. the first text literal uses the CSS class specified in <see cref="TextLiteral"/>
		/// </value>
		/// <remarks>
		/// This can be used for JSON files to make them more readable.
		/// </remarks>
		public string FirstTextLiteral { get; set; }

		/// <summary>
		/// Gets or sets the CSS class for numbers.
		/// </summary>
		/// <value>
		/// Default is "srcNumber"; set this to empty if you don't want numbers to be surrounded by a span tag.
		/// </value>
		public string Number { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CssClasses"/> class.
		/// </summary>
		public CssClasses()
		{
			Keyword = "srcKeyword";
			Identifier = String.Empty;
			Comment = "srcComment";
			TextLiteral = "srcTextLiteral";
			// FirstTextLiteral isn't set by default
			Number = "srcNumber";
		}
	}
}