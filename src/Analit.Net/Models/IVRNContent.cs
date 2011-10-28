using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace Analit.Net.Models
{
	[ActiveRecord(Schema = "Analit", Table = "SiteContent")]
	public class SiteContent : ActiveRecordLinqBase<SiteContent>
	{

		[PrimaryKey]
		public uint Id { get; set; }

		[Property]
		public string Content { get; set; }

		[Property]
		public string ViewName { get; set; }
	}
}