
namespace DatabaseManagement
{
	public class ApplicationModel : DataModel
	{
		public int RestaurantId { get; set; }
		public int ProductId { get; set; }

		public string Date { get; set; }
		public int Count { get; set; }

		public virtual RestaurantModel Restaurant { get; set; }
		public virtual ProductModel Product { get; set; }
	}
}
