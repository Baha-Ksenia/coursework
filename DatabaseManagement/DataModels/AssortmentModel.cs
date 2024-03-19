using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement
{
	public class AssortmentModel : DataModel
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public int RestaurantId { get; set; }

		public virtual RestaurantModel Restaurant { get; set; }
		public virtual CategoryModel Category { get; set; }
	}
}
