using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigelt.SourceToHtml
{
	/// <summary>
	/// A configurable text processor that creates HTML from a (source code) text.
	/// </summary>
	/// <remarks>
	/// Usage:
	/// <list type="bullet">
	/// <item>Create instance (with or without custom settings, <see cref="CreateSettings"/> class for helper functions)</item>
	/// <item>Call <see cref="GetHtml"/> with the text to be turned into HTML. The result is an HTML fragment that uses the CSS classes specified in the settings</item>
	/// <item>Make sure the CSS classes are defined in the HTML document where the fragment is used.</item>
	/// </list>
	/// </remarks>
	public class SourceToHtml
	{
		/// <summary>
		/// Gets or sets the settings that influence the HTML generation.
		/// </summary>
		public SourceToHtmlSettings Settings { get; set; }

		/// <summary>
		/// Returns the HTML for the specified source text.
		/// </summary>
		/// <param name="sourceText">The source text.</param>
		/// <returns></returns>
		public string GetHtml(string sourceText)
		{
			if (sourceText == null)
				throw new ArgumentNullException(nameof(sourceText));
			if (sourceText.Length == 0)
				return String.Empty;

			var text = new Text(TranslateTabs(sourceText, Settings.TabSize));
			var spans = new List<Span>();
			int textLiteralCounter = 0;
			bool spanForRestNecessary=false;
			while (!text.EndReached)
			{
				if (IsIdentifierStartChar(text.GetCurrentChar()))
				{
					Process(MoveBehindIdentifier, spans, text, result => IsKeyword(result) ? Settings.CssClasses.Keyword : Settings.CssClasses.Identifier);
					spanForRestNecessary = false;
					continue;
				}

				if (text.IsMatch(Settings.EndOfLineCommentMarker))
				{
					Process(MoveBehindEndOfLineComment, spans, text, result => Settings.CssClasses.Comment);
					spanForRestNecessary = false;
					continue;
				}

				if (text.IsMatch(Settings.BlockCommentStartMarker))
				{
					Process(MoveBehindBlockComment, spans, text, result => Settings.CssClasses.Comment);
					spanForRestNecessary = false;
					continue;
				}

				var textLiteralProcessed = false;
				foreach (char quoteChar in Settings.QuoteChars)
				{
					if (text.IsMatch(quoteChar))
					{
						var counter = textLiteralCounter;
						Process(t => MoveBehindTextLiteral(t, quoteChar, Settings.EscapeChar), spans, text, result => GetCssClass(counter, Settings.CssClasses.TextLiteral, Settings.CssClasses.FirstTextLiteral));
						++textLiteralCounter;
						textLiteralProcessed = true;
						break;
					}
				}

				if (textLiteralProcessed)
				{
					spanForRestNecessary = false;
					continue;
				}

				if (text.IsDigit())
				{
					Process(MoveBehindNumber, spans, text, result => Settings.CssClasses.Number);
					spanForRestNecessary = false;
					continue;
				}

				spanForRestNecessary = true;

				if ((text.IsMatch(Environment.NewLine)) || text.IsMatch(Settings.TextLiteralResetChars))
				{
					textLiteralCounter = 0;
				}

				text.TryMoveToNextChar();
			}
			if (spanForRestNecessary)
				spans.Add(text.GetSpan());

			return GetHtmlFromSpans(spans);
		}

		private string GetCssClass(int counter, string defaultCssClass, string firstCssClass)
		{
			if ((counter > 0) || String.IsNullOrEmpty(firstCssClass))
				return defaultCssClass;
			return firstCssClass;
		}

		private string TranslateTabs(string originalText, int tabSize)
		{
			var tabReplacement = new String(' ', tabSize);
			return originalText.Replace("\t", tabReplacement);
		}

		private bool IsIdentifierStartChar(char character)
		{
			return Char.IsLetter(character) || Settings.IdentifierSpecialChars.Contains(character);
		}

		private bool IsIdentifierInsideChar(char character)
		{
			return Char.IsLetterOrDigit(character) || Settings.IdentifierSpecialChars.Contains(character);
		}

		private bool IsKeyword(string identifier)
		{
			if ((Settings.Keywords == null) || (Settings.Keywords.Length == 0))
				return false;
			return Settings.Keywords.Any(keyword => String.Equals(identifier, keyword, StringComparison.Ordinal));
		}

		private static void Process(Action<Text> performProcessing, List<Span> spans, Text text, Func<string, string> getCssClass)
		{
			spans.Add(text.GetSpan()); // text before
			text.RememberStartPosition();
			performProcessing(text);
			var newSpan = text.GetSpan();
			newSpan.CssClass = getCssClass(newSpan.GetText());
			spans.Add(newSpan);
			text.RememberStartPosition();
		}

		private void MoveBehindIdentifier(Text text)
		{
			if (!text.TryMoveToNextChar())
				return;

			while (IsIdentifierInsideChar(text.GetCurrentChar())
				&& text.TryMoveToNextChar()) { }
		}

		private void MoveBehindTextLiteral(Text text, char quoteChar, char escapeChar)
		{
			if (!text.TryMoveToNextChar())
				return;
			if (!text.TryMoveTo(quoteChar, escapeChar))
			{
				// we've encountered an unclosed quote, so let's
				// end the processing as quickly as possible.
				text.MoveToEnd();
			}
			else
			{
				// skip the quote character to reach the desired state for further processing.
				text.TryMoveToNextChar();
			}
		}

		private void MoveBehindNumber(Text text)
		{
			while (IsInsideNumber(text) && text.TryMoveToNextChar()) ;
		}

		private bool IsInsideNumber(Text text)
		{
			return text.IsLetterOrDigit() || text.IsMatch(Settings.DecimalPoint) || text.IsMatch(Settings.NumberSeparators);
		}

		private void MoveBehindEndOfLineComment(Text text)
		{
			if (!text.TryMoveTo(Environment.NewLine))
			{
				// end-of-line comment in the last line of the text.
				text.MoveToEnd();
			}
		}

		private void MoveBehindBlockComment(Text text)
		{
			if (!text.TryMoveTo(Settings.BlockCommentEndMarker))
			{
				// we've encountered an unclosed block comment, so let's
				// end the processing as quickly as possible.
				text.MoveToEnd();
			}
			else
			{
				// skip the marker and to reach the desired state for further processing.
				text.TryMoveBy(Settings.BlockCommentEndMarker.Length);
			}
		}


		private string GetHtmlFromSpans(IEnumerable<Span> spans)
		{
			var html = new StringBuilder();
			foreach (var span in spans)
			{
				html.Append(span.GetHtml());
			}
			return html.ToString();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SourceToHtml"/> class.
		/// </summary>
		public SourceToHtml()
			: this(new SourceToHtmlSettings())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SourceToHtml" /> class,
		/// using the specified settings
		/// </summary>
		/// <param name="settings">The settings.</param>
		public SourceToHtml(SourceToHtmlSettings settings)
		{
			Settings = settings;
		}
	}
}