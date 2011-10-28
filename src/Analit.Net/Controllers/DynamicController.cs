using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Analit.Net.Filters;
using Analit.Net.Models;
using Castle.MonoRail.Framework;

namespace Analit.Net.Controllers
{
	public class DynamicAction : IDynamicAction
	{
		public object Execute(IEngineContext engineContext, IController controller, IControllerContext context)
		{
			var baseCtlr = (Controller)controller;
			var content = SiteContent.FindAllByProperty("ViewName", Path.GetFileNameWithoutExtension(context.SelectedViewName));
			context.PropertyBag["Content"] = content.Length > 0 ? content.First().Content : string.Empty;
			if (Convert.ToBoolean(engineContext.Request.QueryString["Edit"]))
			{
				baseCtlr.LayoutName = "TinyMCE";
				context.PropertyBag["ShowEditLink"] = false;
			}
			else
			{
				baseCtlr.LayoutName = "Main";
				context.PropertyBag["ShowEditLink"] = true;
			}
			baseCtlr.SelectedViewName = @"Main\Dynamic";
			return null;
		}
	}


	[Filter(ExecuteWhen.BeforeAction, typeof(BeforeFilter))]
	public class ContentController : SmartDispatcherController
	{
		public override void Contextualize(IEngineContext engineContext, IControllerContext context)
		{
			base.Contextualize(engineContext, context);
			foreach (var ivrnContent in SiteContent.FindAll())
			{
				DynamicActions[ivrnContent.ViewName] = new DynamicAction();
			}
		}
	}
}