
namespace DatabaseManagement
{
	public class StockModel : DataModel
	{
		public int ProductId { get; set; }
		public int ProviderId { get; set; }
		public int Count { get; set; }

		public decimal Price { get; set; }

		public virtual ProductModel Product { get; set; }
		public virtual ProviderModel Provider { get; set; }
	}
}
