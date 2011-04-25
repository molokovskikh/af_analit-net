using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Analit.Net.Filters;
using Analit.Net.Helpers;
using Analit.Net.Models;
using Castle.MonoRail.Framework;

namespace Analit.Net.Controllers
{
	[Layout("Main")]
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof(BeforeFilter))]
	public class EditorController : SmartDispatcherController
	{
		public static IEnumerable<string> SpecialLinks
		{
			get
			{
				return new List<string>{
					"https://stat.analit.net/ci/auth/logon.aspx",
					"http://www.analit.net/apteka",
					"http://cert.analit.net/CertEnroll/acdcserv.adc.analit.net_!0421!043b!0443!0436!0431!0430%20!0441!0435!0440!0442!0438!0444!0438!043a!0430!0446!0438!0438%20!0410!041a%20!0022!0418!043d!0444!043e!0440!0443!043c!0022(1).crt"
					//"Main/requisite"
				};
			}
		}

		public void Menu()
		{
			if (!Regionaladmin.IsAccessiblePartner(Session["LoginPartner"]))
				//RedirectToSiteRoot();
				Redirecter.RedirectRoot(Context, this);
		}

		[return: JSONReturnBinder]
		public string Save(object obj)
		{
			if (Regionaladmin.IsAccessiblePartner(Session["LoginPartner"]))
			{
				ARSesssionHelper<MenuField>.QueryWithSession(session =>
				{
					var query =
						session.CreateSQLQuery(
							@"delete from Analit.menufield; delete from Analit.submenufield;")
							.AddEntity(
								typeof(MenuField));
					query.ExecuteUpdate();
					return new List<MenuField>();
				});
				var id = Request.Form["id[]"].Split(new[] { ',' });
				var name = Request.Form["name[]"].Split(new[] { ',' });
				var link = Request.Form["link[]"].Split(new[] { ',' });
				var type = Request.Form["fieldType[]"].Split(new[] { ',' });
				var elCount = name.Length;
				var mainMenu = new MenuField();
				for (int i = 0; i < elCount; i++)
				{
					//var subLink = IVRNContent.Queryable.Where(p => p.ViewName == Path.GetFileNameWithoutExtension(link[i])).Count() > 0  
					//var subLink =  SpecialLinks.Where(t => t == link[i]).Count() > 0 || link[i].Contains("Content/") ? string.Empty : "Content/";
					var subLink = link[i] == "Content" ? "/" + name[i] : string.Empty;
					if (link[i] == "Content")
						new IVRNContent
						{
							Content = string.Empty,
							ViewName = name[i]
						}.SaveAndFlush();
					if (type[i] == "main")
					{
						mainMenu = new MenuField
						{
							Name = name[i],
							Link = string.Format(link[i] + "{0}", subLink)
						};
						mainMenu.SaveAndFlush();
					}
					if (type[i] == "sub")
					{
						new SubMenuField
						{
							//Link = link[i],
							Link = string.Format(link[i] + "{0}", subLink),
							Name = name[i],
							MenuField = mainMenu
						}.SaveAndFlush();
					}
				}
				//Response.Write("Закончено");
				return "Сохранено";
			}
			//RedirectToUrl("../Editor/Menu");
			return "Не достаточно прав доступа";
		}
	}
}