using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuAccessControl
{
	public class AccessItem : MenuItemAccessModel
	{
		public string MenuName { get; set; }
		public string UserLogin { get; set; }
	}

	/// <summary>
	/// ModelView Окна редактирования доступа 
	/// </summary>
	public class AccessControlPageModelView : TableEditorViewModel
	{

		public AccessControlPageModelView() : base()
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new AccessEditModelView();
				WindowService.OpenWindow(typeof(AccessEditWindow), modelView);
				LoadTable();
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Edit(object obj)
		{
			try
			{
				base.Edit(obj);

				var item = Database.GetMenuAccessList().Find(a => a.Id == SelectedItem.Id);
				var modelView = new AccessEditModelView(item);
				WindowService.OpenWindow(typeof(AccessEditWindow), modelView);
				LoadTable();
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
		protected override void Delete(object obj)
		{
			try
			{
				base.Delete(obj);
				var item = Database.GetMenuAccessList().Find(a => a.Id == SelectedItem.Id);
				Database.Delete(item);
				SuccessMessage("Запись удалено удалена");
				LoadTable();
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
		protected override void LoadTable()
		{
			List<AccessItem> accessItems = new List<AccessItem>(0);

			var accessList = Database.GetMenuAccessList();

			foreach (var access in accessList)
			{
				accessItems.Add(new AccessItem()
				{
					Id = access.Id,
					MenuName = access.MenuItem.Name,
					UserLogin = access.User.Login,
					Read = access.Read,
					Add = access.Add,
					Edit = access.Edit,
					Delete = access.Delete,
				});
			}

			Items = new ObservableCollection<DataModel>(accessItems);
			SelectedItem = null;
		}

	}
}
