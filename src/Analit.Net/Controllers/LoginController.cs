using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Analit.Net.Filters;
using Analit.Net.Helpers;
using Analit.Net.Models;
using Castle.MonoRail.Framework;
using Common.Tools;
using Common.Web.Ui.Controllers;
using log4net;
using NHibernate.Linq;

namespace Analit.Net.Controllers
{
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof(BeforeFilter))]
	public class LoginController : BaseController
	{
		public LoginController()
		{
			BeforeAction += (action, context1, controller, controllerContext) => {
				ContentController.PrepareAction(this, DbSession);
			};
		}

		public void Index(string login, string password)
		{
			var id = Session["AdminId"];
			if (id != null)
				RedirectToSiteRoot();
			if (IsPost) {
				if (ActiveDirectoryHelper.IsAuthenticated(login, password)) {
					var admin = DbSession.Query<Admin>().FirstOrDefault(a => a.UserName == login);
					if (admin != null && admin.Permissions.Any(p => p.Shortcut.Match("RCA"))) {
						Session["AdminId"] = admin.Id;
						FormsAuthentication.RedirectFromLoginPage(login, true);
						RedirectToSiteRoot();
						return;
					}
				}
				Logger.Info("Авторизация отклонена");
				Error("Не верное имя пользователя или пароль");
				RedirectToAction("Index");
			}
		}
	}
}