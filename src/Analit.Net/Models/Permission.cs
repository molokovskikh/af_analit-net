using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.MonoRail.Framework;

namespace Analit.Net.Models
{
	[ActiveRecord(Schema = "accessright", Table = "Permissions", Lazy = true)]
	public class Permission : ActiveRecordLinqBase<Permission>
	{
		[PrimaryKey]
		public virtual uint Id { get; set; }

		[Property]
		public virtual string Name { get; set; }

		[Property]
		public virtual int Type { get; set; }

		[Property]
		public virtual string Shortcut { get; set; }
	}
}