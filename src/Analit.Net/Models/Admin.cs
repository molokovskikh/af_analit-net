using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace Analit.Net.Models
{
	[ActiveRecord(Schema = "accessright", Table = "regionaladmins", Lazy = true)]
	public class Admin : ActiveRecordLinqBase<Admin>
	{
		[PrimaryKey(Column = "RowId")]
		public virtual uint Id { get; set; }

		[Property]
		public virtual string UserName { get; set; }

		[HasAndBelongsToMany(typeof(Permission),
			RelationType.Bag,
			Table = "AdminsPermissions",
			Schema = "accessright",
			ColumnKey = "AdminId",
			ColumnRef = "PermissionId",
			Lazy = true)]
		public virtual IList<Permission> Permissions { get; set; }
	}
}