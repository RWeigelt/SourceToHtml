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
		private int _Length;

		/// <summary>
		/// Gets or sets the CSS class.
		/// </summary>
		/// <remarks>
		/// Set to <see cref="String.Empty"/> if no CSS class should be used
		/// (i.e. to make <see cref="GetHtml"/> return the text without actual "span" tags).
		/// </remarks>
		public string CssClass { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Span"/>
		/// class that represents the source text from the specified
		/// <paramref name="startIndex"/> up to the end.
		/// </summary>
		/// <param name="source">The source string to use.</param>
		/// <param name="startIndex">The index of the first character.</param>
		public Span(string source, int startIndex):this(source,startIndex,-1)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Span"/> class.
		/// class that represents the source text starting at the 
		/// <paramref name="startIndex"/> with the specified
		/// <paramref name="length"/>.
		/// </summary>
		/// <param name="source">The source string to use.</param>
		/// <param name="startIndex">The index of the first character.</param>
		/// <param name="length">The length of the span</param>
		public Span(string source, int startIndex, int length)
		{
			_Source = source;
			_StartIndex = startIndex;
			_Length = length;
		}

		/// <summary>
		/// Returns the HTML for this span.
		/// </summary>
		/// <returns>
		/// The text with or without a surrounding "span" tag, depending on whether <see cref="CssClass"/> is set.
		/// </returns>
		public string GetHtml()
		{
			if (_Length == 0)
				return String.Empty;

			var text=WebUtility.HtmlEncode(GetText());
			return !String.IsNullOrEmpty(CssClass) ? $"<span class=\"{CssClass}\">{text}</span>" : text;
		}

		/// <summary>
		/// Returns the raw text for this span.
		/// </summary>
		/// <returns></returns>
		public string GetText()
		{
			if (_Length == -1)
				return _Source.Substring(_StartIndex);
			return _Source.Substring(_StartIndex, _Length);
		}
	}
}