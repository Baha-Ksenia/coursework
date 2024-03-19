
namespace DatabaseManagement
{
	public class ReceiptModel : DataModel
	{
		public int ProviderId { get; set; }
		public int ProductId { get; set; }

		public string Date { get; set; }
		public int Count { get; set; }

		public virtual ProviderModel Provider { get; set; }
		public virtual ProductModel Product { get; set; }
	}
}
