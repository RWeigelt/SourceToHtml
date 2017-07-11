using System;
using NUnit.Framework;

namespace Weigelt.SourceToHtml.Tests
{
	public class KeywordsAndIdentifiers : TestBase
	{
		private const string _TestText = "Test";

		[Test]
		public void KeywordHandledCorrectly()
		{
			Src2Html.Settings.Keywords = new[] { _TestText };
			var result = Src2Html.GetHtml(_TestText);
			Assert.AreEqual($"<span class=\"{Src2Html.Settings.CssClasses.Keyword}\">{_TestText}</span>", result);
		}

		[Test]
		public void IdentifierHandledCorrectly()
		{
			Src2Html.Settings.Keywords = new string[0];

			Src2Html.Settings.CssClasses.Identifier = "withCssClass";
			var result = Src2Html.GetHtml(_TestText);
			Assert.AreEqual($"<span class=\"{Src2Html.Settings.CssClasses.Identifier}\">{_TestText}</span>", result);

			Src2Html.Settings.CssClasses.Identifier = String.Empty; // without CSS class
			result = Src2Html.GetHtml(_TestText);
			Assert.AreEqual(_TestText, result);
		}

		[Test]
		public void CustomIdentifierCharacters()
		{
			Src2Html.Settings.CssClasses.Identifier = "withCssClass";

			Src2Html.Settings.IdentifierSpecialChars = new[] { '_' };
			var result = Src2Html.GetHtml("$_Hello$_World");
			Assert.AreEqual($"$<span class=\"{Src2Html.Settings.CssClasses.Identifier}\">_Hello</span>$<span class=\"{Src2Html.Settings.CssClasses.Identifier}\">_World</span>", result);

			Src2Html.Settings.IdentifierSpecialChars = new[] { '_', '$' };
			result = Src2Html.GetHtml("$_Hello$_World");
			Assert.AreEqual($"<span class=\"{Src2Html.Settings.CssClasses.Identifier}\">$_Hello$_World</span>", result);

			Src2Html.Settings.CssClasses.Identifier = String.Empty; // without CSS class

			Src2Html.Settings.IdentifierSpecialChars = new[] { '_' };
			result = Src2Html.GetHtml("$_Hello$_World");
			Assert.AreEqual("$_Hello$_World", result);

			Src2Html.Settings.IdentifierSpecialChars = new[] { '_', '$' };
			result = Src2Html.GetHtml("$_Hello$_World");
			Assert.AreEqual("$_Hello$_World", result);

		}

		[Test]
		public void PrefixAndSuffixHandledCorrectly()
		{
			Src2Html.Settings.CssClasses.Identifier = "withCssClass";

			Src2Html.Settings.Keywords = new string[0];
			var result = Src2Html.GetHtml($"@@@{_TestText}@@@");
			Assert.AreEqual($"@@@<span class=\"{Src2Html.Settings.CssClasses.Identifier}\">{_TestText}</span>@@@", result);

			Src2Html.Settings.CssClasses.Identifier = String.Empty; // without CSS class

			Src2Html.Settings.Keywords = new string[0];
			result = Src2Html.GetHtml($"@@@{_TestText}@@@");
			Assert.AreEqual($"@@@{_TestText}@@@", result);
		}
	}
}