using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Weigelt.SourceToHtml.Tests
{
	public class Comments : TestBase
	{
		[Test]
		public void EndOfLineComment()
		{
			var result = Src2Html.GetHtml("// Hello World");
			Assert.AreEqual("<span class=\"srcComment\">// Hello World</span>", result);
		}

		[Test]
		public void PrefixAndSufficHandledCorrectlyForEndOfLineComment()
		{
			var result = Src2Html.GetHtml("@@@// Hello World"+Environment.NewLine+"@@@");
			Assert.AreEqual("@@@<span class=\"srcComment\">// Hello World</span>" + Environment.NewLine + "@@@", result);
		}


		[Test]
		public void BlockComment()
		{
			var result = Src2Html.GetHtml("/* Hello World */");
			Assert.AreEqual("<span class=\"srcComment\">/* Hello World */</span>", result);
		}

		[Test]
		public void PrefixHandledCorrectlyForBlockComment()
		{
			var result = Src2Html.GetHtml("@@@/* Hello" + Environment.NewLine + "World */@@@");
			Assert.AreEqual("@@@<span class=\"srcComment\">/* Hello" + Environment.NewLine + "World */</span>@@@", result);
		}
	}
}
