using System;
using System.Net;

namespace Weigelt.SourceToHtml
{
	/// <summary>
	/// Contains (part of) a text that later (when calling <see cref="GetHtml"/>) may or may not be surrounded
	/// with a "span" tag, depending depending on whether <see cref="CssClass"/> is set.
	/// </summary>
	internal class Span
	{
		private readonly string _Source;
		private readonly int _StartIndex;

		/// <summary>
		/// Gets or sets the CSS class.
		/// </summary>
		/// <remarks>
		/// Set to <see cref="String.Empty"/> if no CSS class should be used
		/// (i.e. to make <see cref="GetHtml"/> return the text without actual "span" tags).
		/// </remarks>
		public string CssClass { get; set; }

		/// <summary>
		/// Gets or sets the index of the last character that is contained in the span.
		/// </summary>
		/// <value>
		/// The index  of the last character; the default is <c>-1</c> which indicates that the span is completely empty.
		/// </value>
		public int EndIndex { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Span"/>
		/// class that represents the source text from the specified
		/// <paramref name="startIndex"/> up to the end.
		/// </summary>
		/// <param name="source">The source string to use.</param>
		/// <param name="startIndex">The index of the first character.</param>
		public Span(string source, int startIndex)
		{
			_Source = source;
			_StartIndex = startIndex;
			EndIndex = -1;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Span"/> class.
		/// class that represents the source text from the specified
		/// <paramref name="startIndex"/> up to the
		/// <paramref name="endIndex"/>.
		/// </summary>
		/// <param name="source">The source string to use.</param>
		/// <param name="startIndex">The index of the first character.</param>
		/// <param name="endIndex">The index of the last character.</param>
		public Span(string source, int startIndex, int endIndex) : this(source, startIndex)
		{
			EndIndex = endIndex;
		}

		/// <summary>
		/// Returns the HTML for this span.
		/// </summary>
		/// <returns>
		/// The text with or without a surrounding "span" tag, depending on whether <see cref="CssClass"/> is set.
		/// </returns>
		public string GetHtml()
		{
			var text=WebUtility.HtmlEncode(GetText());
			return !String.IsNullOrEmpty(CssClass) ? $"<span class=\"{CssClass}\">{text}</span>" : text;
		}

		/// <summary>
		/// Returns the raw text for this span.
		/// </summary>
		/// <returns></returns>
		public string GetText()
		{
			return _Source.Substring(_StartIndex, Math.Max(0, EndIndex - _StartIndex + 1));
		}
	}
}