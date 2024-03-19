using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement
{
	public class MenuItemAccessModel : DataModel
	{
		public int UserId { get; set; }
		public int MenuId { get; set; }


		public int Read { get; set; }
		public int Add { get; set; }
		public int Edit { get; set; }
		public int Delete { get; set; }

		// Связанные данные
		public virtual UserModel User { get; set; }
		public virtual MenuItemModel MenuItem { get; set; }
	}
}
