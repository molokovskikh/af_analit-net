using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Analit.Net.Filters;
using Analit.Net.Models;
using Castle.MonoRail.Framework;
using Common.Web.Ui.Controllers;

namespace Analit.Net.Controllers
{
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof (BeforeFilter))]
	public class ContentController : BaseContentController
	{
		public override bool IsAcces()
		{
			return Regionaladmin.IsAccessiblePartner(Session["LoginPartner"]);
		}
	}
}