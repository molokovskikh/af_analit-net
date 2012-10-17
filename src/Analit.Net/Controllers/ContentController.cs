using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Analit.Net.Filters;
using Analit.Net.Models;
using Castle.MonoRail.Framework;
using Common.Web.Ui.Controllers;
using Common.Web.Ui.Helpers;

namespace Analit.Net.Controllers
{
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof(BeforeFilter))]
	[Helper(typeof(AppHelperWeb), "app")]
	[Helper(typeof(ViewHelper))]
	public class ContentController : BaseContentController
	{
		public override bool IsAcces()
		{
			if (Session["LoginPartner"] != null)
				return Regionaladmin.IsAccessiblePartner(Session["LoginPartner"]);
			return false;
		}
	}
}