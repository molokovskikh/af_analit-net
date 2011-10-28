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
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof (BeforeFilter))]
	public class EditorController : SmartDispatcherController
	{
		public static IEnumerable<string> SpecialLinks
		{
			get
			{
				return new List<string> {
					"https://stat.analit.net/ci/auth/logon.aspx",
					"http://www.analit.net/apteka",
					//"Main/requisite"
				};
			}
		}

		public void Menu()
		{
			if (!Regionaladmin.IsAccessiblePartner(Session["LoginPartner"]))
				Redirecter.RedirectRoot(Context, this);
		}

		[return: JSONReturnBinder]
		public string Save(object obj)
		{
			if (Regionaladmin.IsAccessiblePartner(Session["LoginPartner"])) {
				ARSesssionHelper<MenuField>.QueryWithSession(session => {
					var query =
						session.CreateSQLQuery(
							@"delete from Analit.menufield; delete from Analit.submenufield;")
							.AddEntity(
								typeof (MenuField));
					query.ExecuteUpdate();
					return new List<MenuField>();
				});
				var id = Request.Form["id[]"].Split(new[] {','});
				var name = Request.Form["name[]"].Split(new[] {','});
				var link = Request.Form["link[]"].Split(new[] {','});
				var type = Request.Form["fieldType[]"].Split(new[] {','});
				var elCount = name.Length;
				var mainMenu = new MenuField();
				for (int i = 0; i < elCount; i++) {
					var subLink = link[i] == "Content" ? "/" + name[i] : string.Empty;
					if (link[i] == "Content" && SiteContent.Queryable.Count(iv => iv.ViewName == name[i]) == 0)
						new SiteContent {
							Content = string.Empty,
							ViewName = name[i]
						}.SaveAndFlush();
					if (type[i] == "main") {
						mainMenu = new MenuField {
							Name = name[i],
							Link = string.Format(link[i] + "{0}", subLink)
						};
						mainMenu.SaveAndFlush();
					}
					if (type[i] == "sub") {
						new SubMenuField {
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