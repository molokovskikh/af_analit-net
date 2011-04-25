using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Analit.Net.Filters;
using Castle.MonoRail.Framework;

namespace Analit.Net.Controllers
{
	[Filter(ExecuteWhen.BeforeAction, typeof(BeforeFilter))]
	public class MainController : SmartDispatcherController 
	{
		public void Index()
		{
		}
	}
}