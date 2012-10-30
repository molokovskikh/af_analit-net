using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Analit.Net.Controllers;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Castle.MonoRail.TestSupport;
using Common.Web.Ui.Models;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;

namespace Analit.Net.Tests
{
	[TestFixture]
	public class ContentControllerFixture : BaseControllerTest
	{
		private ContentController _controller;
		private ISession _session;
		[SetUp]
		public void SetUp()
		{
			if (!ActiveRecordStarter.IsInitialized) {
				ActiveRecordStarter.Initialize(
					new[] { Assembly.Load("Analit.Net"),
						Assembly.Load("Common.Web.UI")
					},
					ActiveRecordSectionHandler.Instance);
			}

			var holder = ActiveRecordMediator.GetSessionFactoryHolder();
			_session = holder.CreateSession(typeof(ActiveRecordBase));

			_controller = new ContentController();
			_controller.DbSession = _session;
			PrepareController(_controller);
		}

		[TearDown]
		public void TearDown()
		{
			if (_session != null) {
				var holder = ActiveRecordMediator.GetSessionFactoryHolder();
				holder.ReleaseSession(_session);
				_session = null;
			}
		}

		[Test]
		public void EscapeHtmlTest()
		{
			var source = @"h1. Тест:

Далее должно быть экранировано
<pre>
<p>экранированный текст</p>
&
</pre>";
			var result = _controller.MarkUpToHTML(source);
			Assert.That(result.Contains("&lt;p&gt;экранированный текст&lt;/p&gt;"));
			Assert.That(result.Contains("&amp;"));
			Assert.That(result.Contains("<h1>"));
		}

		[Test]
		public void GetRegionsTest()
		{
			var region = _session.Query<Region>().FirstOrDefault(r => r.Id == 1);
			var tmpPhone = region.DefaultPhone;
			region.DefaultPhone = "111-111";
			_session.Save(region);
			_session.Flush();
			_controller.GetContactPhones();
			Assert.That(_controller.PropertyBag.Contains("contactRegions"));
			var result = _controller.PropertyBag["contactRegions"] as List<Region>;
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Count(r => r.Id == 1 && r.DefaultPhone == "111-111") == 1);
			// возвращаем как было
			region.DefaultPhone = tmpPhone;
			_session.Save(region);
		}
	}
}
