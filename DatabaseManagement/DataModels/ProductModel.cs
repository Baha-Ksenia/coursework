
namespace DatabaseManagement
{
	public class ProductModel : DataModel
	{
		public string Name { get; set; }
		public int UnitId { get; set; }

		public virtual UnitModel Unit { get; set; }
	}
}
