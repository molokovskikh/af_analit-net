using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analit.Net.Helpers
{
	public class AppHelper : Common.Web.Ui.Helpers.AppHelper
	{
		public override bool HavePermission(string controller, string action)
		{
			return true;
		}
	}
}