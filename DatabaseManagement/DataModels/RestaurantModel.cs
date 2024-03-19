
namespace DatabaseManagement
{
	public class RestaurantModel : DataModel
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }

		public string SurnameDirector { get; set; }
		public string NameDirector { get; set; }
		public string PatronymicDirector { get; set; }

		public int StreetId { get; set; }
		public virtual StreetModel Street { get; set; }
	}
}
