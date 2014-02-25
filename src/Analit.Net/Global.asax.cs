using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Configuration;
using Castle.MonoRail.Framework.Container;
using Castle.MonoRail.Framework.Internal;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.Framework.Services;
using Castle.MonoRail.Views.Brail;
using Common.Web.Ui.Helpers;
using Common.Web.Ui.MonoRailExtentions;
using log4net;
using log4net.Config;

namespace Analit.Net
{
	public class Global : WebApplication
	{
		public Global()
			: base(Assembly.Load("Analit.Net"))
		{
			Logger = new HttpSessionLog(typeof(Global));
			LibAssemblies.Add(Assembly.Load("Common.Web.Ui"));
			Logger.ErrorSubject = "Ошибка в AnalitNet";
			Logger.SmtpHost = "box.analit.net";
			Logger.ExcludeExceptionTypes.Add(typeof(ControllerNotFoundException));
			Logger.ExcludeExceptionTypes.Add(typeof(FileNotFoundException));
		}

		private void Application_Start(object sender, EventArgs e)
		{
			try {
				ActiveRecordStarter.Initialize(new[] {
					Assembly.Load("Analit.Net"), Assembly.Load("Common.Web.Ui")
				},
					ActiveRecordSectionHandler.Instance);

				RoutingModuleEx.Engine.Add(new PatternRoute("/")
					.DefaultForController().Is("Content")
					.DefaultForAction().Is("Программа"));
			}
			catch (Exception ex) {
				Log.Fatal("Ошибка при запуске страницы.", ex);
			}
		}
	}
}