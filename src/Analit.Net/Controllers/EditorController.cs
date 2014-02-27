﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Analit.Net.Filters;
using Analit.Net.Helpers;
using Analit.Net.Models;
using Castle.MonoRail.Framework;
using Common.Web.Ui.Controllers;
using Common.Web.Ui.Models;
using NHibernate;
using NHibernate.Linq;


namespace Analit.Net.Controllers
{
	[Layout("Main")]
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof(BeforeFilter))]
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof(LoginFilter))]
	public class EditorController : BaseEditorController
	{
		public EditorController()
		{
			BeforeAction += (action, context1, controller, controllerContext) => {
				ContentController.PrepareAction(this, DbSession);
			};
		}

		public override IEnumerable<string> SpecialLinks
		{
			get
			{
				return new List<string> {
					"https://stat.analit.net/ci/auth/logon.aspx",
					"http://www.analit.net/apteka",
				};
			}
		}
	}
}