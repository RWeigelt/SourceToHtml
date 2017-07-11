using NUnit.Framework;

namespace Weigelt.SourceToHtml.Tests
{
	public class TextLiterals : TestBase
	{
		[Test]
		public void SingleQuotes()
		{
			var result = Src2Html.GetHtml("'Hello \"quoted\" world'");
			Assert.AreEqual($"<span class=\"{Src2Html.Settings.CssClasses.TextLiteral}\">&#39;Hello &quot;quoted&quot; world&#39;</span>", result);
		}

		[Test]
		public void DoubleQuotes()
		{
			var result = Src2Html.GetHtml("\"Hello 'quoted' world\"");
			Assert.AreEqual($"<span class=\"{Src2Html.Settings.CssClasses.TextLiteral}\">&quot;Hello &#39;quoted&#39; world&quot;</span>", result);
		}

		[Test]
		public void BacktickQuotes()
		{
			Src2Html.Settings.QuoteChars = new[] {'\'', '"', '`'};
			var result = Src2Html.GetHtml("`Hello world`");
			Assert.AreEqual($"<span class=\"{Src2Html.Settings.CssClasses.TextLiteral}\">`Hello world`</span>", result);
		}

		[Test]
		public void PrefixAndSuffixHandledCorrectly()
		{
			var result = Src2Html.GetHtml("$$$'Hello \"quoted\" world'$$$");
			Assert.AreEqual($"$$$<span class=\"{Src2Html.Settings.CssClasses.TextLiteral}\">&#39;Hello &quot;quoted&quot; world&#39;</span>$$$", result);
		}
	}
}
