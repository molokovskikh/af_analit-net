using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Analit.Net.Models;
using Castle.ActiveRecord;
using Castle.MonoRail.Framework;
using Common.Web.Ui.ActiveRecordExtentions;
using Common.Web.Ui.Models;

namespace Analit.Net.Filters
{
	public class LoginFilter : IFilter
	{
		public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller,
			IControllerContext controllerContext)
		{
			return context.Session["Admin"] != null;
		}
	}

	public class BeforeFilter : IFilter
	{
		public bool Perform(ExecuteWhen exec,
			IEngineContext context,
			IController controller,
			IControllerContext controllerContext)
		{
			controllerContext.PropertyBag["ViewName"] =
				Path.GetFileNameWithoutExtension(context.Request.Uri.Segments.Last());
			controllerContext.PropertyBag["LocalPath"] =
				Path.GetFileNameWithoutExtension(context.Request.Uri.LocalPath);

			if (context.Session["AdminId"] != null)
				ArHelper.WithSession(s => {
					context.Session["Admin"] = s.Get<Admin>(context.Session["AdminId"]);
				});
			controllerContext.PropertyBag["AccessEditLink"] = context.Session["Admin"] != null;
			controllerContext.PropertyBag["SiteAdress"] = context.ApplicationPath + "/";
			return true;
		}
	}
}