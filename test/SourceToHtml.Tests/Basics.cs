using System;
using NUnit.Framework;

namespace Weigelt.SourceToHtml.Tests
{
	public class Basics : TestBase
	{
		[Test]
		public void EmptyStaysEmpty()
		{
			var result = Src2Html.GetHtml(String.Empty);
			Assert.AreEqual(String.Empty, result);
		}

		[Test]
		public void NullThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				Src2Html.GetHtml(null);
			});
		}

		[Test]
		public void NothingToDoKeepsTextUnchanged()
		{
			var text = "!,-@{}=?";
			var result = Src2Html.GetHtml(text);
			Assert.AreEqual(text, result);
		}

		[Test]
		public void SafeHtml()
		{
			var text = "<>&";
			var result = Src2Html.GetHtml(text);
			Assert.AreEqual("&lt;&gt;&amp;", result);
		}
	}
}
