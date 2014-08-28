using System.Reflection;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using NUnit.Framework;

namespace Analit.Net.Test
{
	[SetUpFixture]
	public class SetupFixture
	{
		[SetUp]
		public void Setup()
		{
			ActiveRecordStarter.Initialize(
				new[] { Assembly.Load("Analit.Net"),
					Assembly.Load("Common.Web.UI")
				},
				ActiveRecordSectionHandler.Instance);
		}
	}
}