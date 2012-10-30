using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Analit.Net.Controllers;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Castle.MonoRail.TestSupport;
using NUnit.Framework;

namespace Analit.Net.Tests
{
	[TestFixture]
	public class ContentControllerFixture : BaseControllerTest
	{
		private ContentController _controller;
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
			_controller = new ContentController();
			PrepareController(_controller);
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
	}
}
