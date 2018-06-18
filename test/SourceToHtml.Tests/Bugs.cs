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

	    [Test]
	    public void Bug002()
	    {
	        var sourceText1 = @"{
	""fooA"" : { ""bar"" : [ ""valueA1"", ""valueA2"" ] },
	""fooB"" : { ""bar"" : ""valueB"" }
}";

            var sourceText2 = @"{
	""fooA"" : {
		""bar"" : [ ""valueA1"", ""valueA2"" ]
	},
	""fooB"" : {
		""bar"" : ""valueB""
	}
}";

	        Src2Html.Settings = CreateSettings.ForJson;
	        var result1 = StripWhitespace(Src2Html.GetHtml(sourceText1));
	        var result2 = StripWhitespace(Src2Html.GetHtml(sourceText2));

            Assert.AreEqual(result1, result2);
	    }

	    private string StripWhitespace(string text)
	    {
	        return text.Replace("span class", "span_class")
	            .Replace(" ", String.Empty)
	            .Replace(Environment.NewLine, String.Empty)
	            .Replace("span_class", "span class");
	    }

        [Test]
	    public void Bug003()
	    {
	        var sourceText = @"Hello ""World"" 123 Test";

	        Src2Html.Settings = CreateSettings.ForPlainText;
	        var result = Src2Html.GetHtml(sourceText);

	        Assert.AreEqual(sourceText, result);
	    }
    }
	}
