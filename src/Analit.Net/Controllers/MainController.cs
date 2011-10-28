using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Analit.Net.Filters;
using Analit.Net.Models;
using Castle.MonoRail.Framework;

namespace Analit.Net.Controllers
{
	[Filter(ExecuteWhen.BeforeAction, typeof(BeforeFilter))]
	public class MainController : SmartDispatcherController
	{
		public void Save()
		{
			var localPath = Request.Form["LocalPath"];
			if (Regionaladmin.IsAccessiblePartner(Session["LoginPartner"]))
			{
				var htmlcode = Request.Form["htmlcode"];
				var views = SiteContent.FindAllByProperty("ViewName", localPath);
				if (views.Length == 0)
					new SiteContent
					{
						Content = htmlcode,
						ViewName = localPath
					}.SaveAndFlush();
				else
				{
					var forEdit = views.First();
					forEdit.Content = htmlcode;
					forEdit.ViewName = localPath;
					forEdit.UpdateAndFlush();
				}
			}
			var url = localPath == "HowPay" ? string.Empty : Context.ApplicationPath + "/Content/";
			RedirectToUrl(url + localPath);
		}
	}
}