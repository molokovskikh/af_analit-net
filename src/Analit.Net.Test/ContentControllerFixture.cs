using System;
using System.Collections.Generic;
using System.Linq;
using Analit.Net.Controllers;
using Common.Web.Ui.Models;
using Common.Web.Ui.Test.Controllers;
using NHibernate.Linq;
using NUnit.Framework;

namespace Analit.Net.Test
{
	[TestFixture]
	public class ContentControllerFixture : ControllerFixture
	{
		private ContentController _controller;

		[SetUp]
		public void SetUp()
		{
			_controller = new ContentController();
			Prepare(_controller);
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
			var region = session.Query<Region>().FirstOrDefault(r => r.Id == 1);
			var tmpPhone = region.DefaultPhone;
			region.DefaultPhone = "111-111";
			session.Save(region);
			session.Flush();
			ContentController.PrepareAction(_controller, session);
			Assert.That(_controller.PropertyBag.Contains("contactRegions"));
			var result = _controller.PropertyBag["contactRegions"] as List<Region>;
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Count(r => r.Id == 1 && r.DefaultPhone == "111-111") == 1);
			// возвращаем как было
			region.DefaultPhone = tmpPhone;
			session.Save(region);
		}
	}
}
