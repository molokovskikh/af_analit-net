using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Analit.Net.Filters;
using Analit.Net.Models;
using Castle.MonoRail.Framework;
using Common.Web.Ui.Controllers;
using Common.Web.Ui.Helpers;
using Textile;
using Textile.Blocks;
using AppHelper = Analit.Net.Helpers.AppHelper;

namespace Analit.Net.Controllers
{
	[FilterAttribute(ExecuteWhen.BeforeAction, typeof(BeforeFilter))]
	[Helper(typeof(AppHelper), "app")]
	[Helper(typeof(ViewHelper))]
	public class ContentController : BaseContentController
	{
		public override bool IsAcces()
		{
			if (Session["LoginPartner"] != null)
				return Regionaladmin.IsAccessiblePartner(Session["LoginPartner"]);
			return false;
		}

		public override string MarkUpToHTML(string source)
		{
			var output = new EscapeOutputter();
			TextileFormatter.FormatString(source, output);
			return output.Result;
		}
	}

	public class EscapeOutputter : IOutputter
	{
		private bool _isEscape;
		private StringBuilder _resultBuilder;

		public string Result
		{
			get { return _resultBuilder.ToString(); }
		}

		public void Begin()
		{
			_resultBuilder = new StringBuilder();
			_isEscape = false;
		}

		public void Write(string text)
		{
			AppendString(text, false);
		}

		public void WriteLine(string line)
		{
			AppendString(line, true);
		}

		private void AppendString(string source, bool isLine)
		{
			if(CheckEscape(source) && _isEscape) {
				if(isLine)
					_resultBuilder.AppendLine(source.Replace("<", "&lt;")
						.Replace(">", "&gt;"));
				else
					_resultBuilder.Append(source.Replace("<", "&lt;")
						.Replace(">", "&gt;"));
			}
			else {
				_resultBuilder.Append(source);
			}
		}

		public void End()
		{
		}

		private bool CheckEscape(string source)
		{
			if(source.ToLower().Contains("<pre>")) {
				_isEscape = true;
				return false;
			}
			if(source.ToLower().Contains("</pre>")) {
				_isEscape = false;
				return false;
			}
			return true;
		}
	}
}