namespace Weigelt.SourceToHtml
{
	public class SourceToHtmlSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SourceToHtmlSettings"/> class
		/// with a default configuration (not language-specific).
		/// </summary>
		public SourceToHtmlSettings()
		{
			Keywords = new string[0];
			this.CssClasses = new CssClasses();
			EndOfLineCommentMarker = "//";
			BlockCommentStartMarker = "/*";
			BlockCommentEndMarker = "*/";
			IdentifierSpecialChars = new[] { '_' };
			QuoteChars = new[] { '\'', '"' };
			TabSize = 4;
			EscapeChar = '\\';
		}

		/// <summary>
		/// Gets or sets the keywords.
		/// </summary>
		/// <value>Default: Empty</value>
		public string[] Keywords { get; set; }

		/// <summary>
		/// Gets the marker for end-of-line comments
		/// </summary>
		public string EndOfLineCommentMarker { get; set; }

		/// <summary>
		/// Gets or sets the marker for the start of a block comment.
		/// </summary>
		public string BlockCommentStartMarker { get; set; }

		/// <summary>
		/// Gets or sets the marker the end of a block comment.
		/// </summary>
		public string BlockCommentEndMarker { get; set; }

		/// <summary>
		/// Gets or sets the special characters that are allowed in an identifier name.
		/// </summary>
		public char[] IdentifierSpecialChars { get; set; }

		/// <summary>
		/// The CSS classes to be used.
		/// </summary>
		public CssClasses CssClasses { get; }

		/// <summary>
		/// Gets or sets the characters that are allowed for text literals.
		/// </summary>
		public char[] QuoteChars { get; set; }

		/// <summary>
		/// Gets or sets the escape character used inside text literals.
		/// </summary>
		/// <value>
		/// Default: <c>\</c>. Set to <c>'\0'</c> to not specify an escape character.
		/// </value>
		public char EscapeChar { get; set; }

		/// <summary>
		/// Gets or sets the number of spaces that are equivalent to one tab character.
		/// </summary>
		/// <value>
		/// Default: <c>4</c>.
		/// </value>
		public int TabSize { get; set; }
	}
}