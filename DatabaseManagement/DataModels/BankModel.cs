
namespace DatabaseManagement
{
	public class BankModel : DataModel
	{
		public string Name { get; set; }
		public int StreetId { get; set; }
		public string Address { get; set; }

		public virtual StreetModel Street { get; set; }
	}
}
