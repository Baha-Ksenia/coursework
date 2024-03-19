using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DatabaseManagement;
using ModelViewSystem;


namespace StreetMenu
{
	/// <summary>
	/// ModelView Окна редактирования справочника улиц
	/// </summary>
	public class StreetControlPageModelView : TableEditorViewModel
	{
		public StreetControlPageModelView() : base()
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new StreetEditWindowModelView();
				WindowService.OpenWindow(typeof(StreetEditWindow), modelView);
				LoadTable();
			}
			catch(Exception ex) 
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Edit(object obj)
		{
			try
			{
				base.Edit(obj);
				var item = Database.GetStreetsList().Find(a => a.Id == SelectedItem.Id);
				var modelView = new StreetEditWindowModelView(item);
				WindowService.OpenWindow(typeof(StreetEditWindow), modelView);
				LoadTable();
			}
			catch(Exception ex) 
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Delete(object obj)
		{
			try
			{
				base.Delete(obj);
				var item = Database.GetStreetsList().Find(a => a.Id == SelectedItem.Id);
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
			List<StreetModel> streetModels = Database.GetStreetsList();

			Items = new ObservableCollection<DataModel>(streetModels);
			SelectedItem = null;
		}
	}
}
