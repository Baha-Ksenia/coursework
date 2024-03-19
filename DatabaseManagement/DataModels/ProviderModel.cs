
namespace DatabaseManagement
{
	public class ProviderModel : DataModel
	{
		public string Name { get; set; }

		public string SurnameDirector { get; set; }
		public string NameDirector { get; set; }
		public string PatronymicDirector { get; set; }

		public string TaxNumber { get; set; }
		public string Invoice { get; set; }

		public int StreetId { get; set; }
		public virtual StreetModel Street { get; set; }
		public string Address { get; set; }

		public int BankId { get; set; }
		public virtual BankModel Bank { get; set; }
	}
}
