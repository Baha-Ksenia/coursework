
namespace DatabaseManagement
{
	public class ReportModel : DataModel
	{
		public string Date { get; set; }

		public int RestaurantId { get; set; }
		public int Categoryid { get; set; }
		public int UnitId { get; set; }

		public decimal Proceeds { get; set; }
		public int Count { get; set; }

		public virtual RestaurantModel Restaurant { get; set; }
		public virtual CategoryModel Category { get; set; }
		public virtual UnitModel Unit { get; set; }
	}
}
