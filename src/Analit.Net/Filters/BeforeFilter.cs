using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Analit.Net.Models;
using Castle.ActiveRecord;
using Castle.MonoRail.Framework;

namespace Analit.Net.Filters
{
	public class Redirecter
	{
		public static void RedirectRoot(IEngineContext context, Controller controller)
		{
			controller.RedirectToUrl(context.ApplicationPath + "/");
		}
	}

	public class LoginFilter : IFilter
	{
		public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller,
		                    IControllerContext controllerContext)
		{
			if (!Regionaladmin.IsAccessiblePartner(context.Session["LoginPartner"]))
				Redirecter.RedirectRoot(context, (Controller) controller);
			else {
				return true;
			}
			return false;
		}
	}


	public class BeforeFilter : IFilter
	{
		public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
		{
			controllerContext.PropertyBag["ViewName"] = Path.GetFileNameWithoutExtension(context.Request.Uri.Segments.Last());
			controllerContext.PropertyBag["LocalPath"] = Path.GetFileNameWithoutExtension(context.Request.Uri.LocalPath);
			if (context.Session["LoginPartner"] == null)
			{ context.Session["LoginPartner"] = context.CurrentUser.Identity.Name; }
			controllerContext.PropertyBag["AccessEditLink"] = Regionaladmin.IsAccessiblePartner(context.Session["LoginPartner"]);
			controllerContext.PropertyBag["SiteAdress"] = context.ApplicationPath + "/";
			return true;
		}
	}
}