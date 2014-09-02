using System;
using System.IO;
using System.Reflection;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Routing;
using Common.Web.Ui.Helpers;
using Common.Web.Ui.MonoRailExtentions;

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
				RoutingModuleEx.Engine.Add(new PatternRoute("/<controller>/[action]")
					.DefaultForAction().Is("Index"));
			}
			catch (Exception ex) {
				Log.Fatal("Ошибка при запуске страницы.", ex);
			}
		}
	}
}