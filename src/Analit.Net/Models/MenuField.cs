using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace Analit.Net.Models
{
	[ActiveRecord(Schema = "Analit", Table = "MenuField", Lazy = true)]
	public class MenuField : ActiveRecordLinqBase<MenuField>
	{

		[PrimaryKey]
		public virtual uint Id { get; set; }

		[Property]
		public virtual string Name { get; set; }

		[Property]
		public virtual string Link { get; set; }

		[HasMany]
		public virtual IList<SubMenuField> subMenu { get; set; }
	}
}