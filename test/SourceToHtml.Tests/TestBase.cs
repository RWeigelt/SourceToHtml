using NUnit.Framework;

namespace Weigelt.SourceToHtml.Tests
{
	[TestFixture]
	public class TestBase
	{
		protected SourceToHtml Src2Html;

		[SetUp]
		public void Init()
		{
			Src2Html = new SourceToHtml();
		}

		[TearDown]
		public void Cleanup()
		{
			Src2Html = null;
		}
	}
}