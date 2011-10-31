using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using Analit.Net.Filters;
using Analit.Net.Helpers;
using Analit.Net.Models;
using Castle.MonoRail.Framework;
using log4net;

namespace Analit.Net.Controllers
{
	[Layout("Main")]
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof (BeforeFilter))]
	public class LoginController : SmartDispatcherController
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof (LoginController));

		public void LoginPage(bool partner)
		{
			if (!partner)
				PropertyBag["AcceptName"] = "AcceptClient";
			else {
				if (Regionaladmin.IsAccessiblePartner(Session["LoginPartner"]))
					Redirecter.RedirectRoot(Context, this);
				PropertyBag["AcceptName"] = "AcceptPartner";
			}
		}

		[AccessibleThrough(Verb.Post)]
		public void AcceptPartner(string Login, string Password)
		{
			if (ActiveDirectoryHelper.IsAuthenticated(Login, Password)) {
				_log.Info("Авторизация выполнена");
				FormsAuthentication.RedirectFromLoginPage(Login, true);
				Session["LoginPartner"] = Login;
				Redirecter.RedirectRoot(Context, this);
			}
			else {
				_log.Info("Авторизация отклонена");
				RedirectToUrl(@"..//Login/LoginPage?partner=true");
			}
		}
	}
}