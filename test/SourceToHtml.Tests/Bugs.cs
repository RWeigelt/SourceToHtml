using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Weigelt.SourceToHtml.Tests
{
	public class Bugs : TestBase
	{
		[Test]
		public void Bug001()
		{
			var sourceText = @"var example1=""Hello"";
var example2=""World"";";

			Src2Html.Settings = CreateSettings.ForJavaScript;
			var result = Src2Html.GetHtml(sourceText);

			Assert.AreEqual(@"<span class=""srcKeyword"">var</span> example1=<span class=""srcTextLiteral"">&quot;Hello&quot;</span>;
<span class=""srcKeyword"">var</span> example2=<span class=""srcTextLiteral"">&quot;World&quot;</span>;", result);
		}
	}
	}
