using NUnit.Framework;

namespace Weigelt.SourceToHtml.Tests
{
	public class Numbers : TestBase
	{
		[Test]
		public void Unchanged()
		{
			var result = Src2Html.GetHtml("Lorem Test123 Ipsum");
			Assert.AreEqual($"Lorem Test123 Ipsum", result);
		}

		[Test]
		public void OnlyDigits()
		{
			var result = Src2Html.GetHtml("Lorem 12345 Ipsum");
			Assert.AreEqual($"Lorem <span class=\"{Src2Html.Settings.CssClasses.Number}\">12345</span> Ipsum", result);
		}

		[Test]
		public void Mixed()
		{
			var result = Src2Html.GetHtml("Lorem 0x123.45) Ipsum");
			Assert.AreEqual($"Lorem <span class=\"{Src2Html.Settings.CssClasses.Number}\">0x123.45</span>) Ipsum", result);
		}

		[Test]
		public void Separators()
		{
			var testText = "Lorem 123|456|789 Ipsum";
			var result = Src2Html.GetHtml(testText);
			Assert.AreEqual($"Lorem <span class=\"{Src2Html.Settings.CssClasses.Number}\">123</span>|<span class=\"{Src2Html.Settings.CssClasses.Number}\">456</span>|<span class=\"{Src2Html.Settings.CssClasses.Number}\">789</span> Ipsum", result);

			Src2Html.Settings.NumberSeparators = new[] {'|'};
			result = Src2Html.GetHtml(testText);
			Assert.AreEqual($"Lorem <span class=\"{Src2Html.Settings.CssClasses.Number}\">123|456|789</span> Ipsum", result);
		}
	}
}
