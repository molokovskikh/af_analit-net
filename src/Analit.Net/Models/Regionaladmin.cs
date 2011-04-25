using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace Analit.Net.Models
{
	[ActiveRecord(Schema = "accessright", Table = "regionaladmins", Lazy = true)]
	public class Regionaladmin : ActiveRecordLinqBase<Regionaladmin>
	{
		[PrimaryKey]
		public virtual uint RowId { get; set; }

		[Property]
		public virtual string UserName { get; set; }

		[Property]
		public virtual string ManagerName { get; set; }

		[Property]
		public virtual string PhoneSupport { get; set; }

		[Property]
		public virtual string InternalPhone { get; set; }

		[Property]
		public virtual string Email { get; set; }

		[Property]
		public virtual int Department { get; set; }

		[HasAndBelongsToMany(typeof(Permission),
			RelationType.Bag,
			Table = "AdminsPermissions",
			Schema = "accessright",
			ColumnKey = "AdminId",
			ColumnRef = "PermissionId",
			Lazy = true)]
		public virtual IList<Permission> Permissions { get; set; }

		public static bool IsAccessiblePartner(object userName)
		{
			var admin = Queryable.Where(q => q.UserName == userName.ToString()).ToList();
			if (admin.Count != 0)
			{
				if (admin.First().Permissions.Select(p=>p.Shortcut).Contains("RCA"))
					return true;
			}
			return false;
		}
	}
}