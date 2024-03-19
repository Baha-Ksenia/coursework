using DatabaseManagement;

namespace ModelViewSystem
{
	/// <summary>
	/// Информация о доступе к меню
	/// </summary>
	public struct PageAccess
	{
		public bool Read { get; private set; }
		public bool Add { get; private set; }
		public bool Edit { get; private set; }
		public bool Delete { get; private set; }

		public PageAccess(bool r, bool w, bool e, bool d, bool isDefault)
		{
			Read = isDefault ? true : r;
			Add = isDefault ? true : w;
			Edit = isDefault ? true : e;
			Delete = isDefault ? true : d;
		}

		public static PageAccess LoadFromItem(MenuItemAccessModel aM) => new PageAccess(aM.Read > 0, aM.Add > 0, aM.Edit > 0, aM.Delete > 0, aM.MenuItem.Default > 0);

		public bool HaveAccess => Read || Add || Edit || Delete;
	}
}
