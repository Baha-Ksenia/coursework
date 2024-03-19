using System;
using System.Collections.Generic;
using DatabaseManagement;
using ModelViewSystem;
using System.Collections.ObjectModel;

namespace UnitsMenu
{
	/// <summary>
	/// ModelView Окна редактирования справочника ед. измерения
	/// </summary>
	public class UnitControlPageModelView : TableEditorViewModel
	{
		public UnitControlPageModelView() : base()
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new UnitEditWindowModelView();
				WindowService.OpenWindow(typeof(UnitEditWindow), modelView);
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
				var item = Database.GetUnitsList().Find(a => a.Id == SelectedItem.Id);
				var modelView = new UnitEditWindowModelView(item);
				WindowService.OpenWindow(typeof(UnitEditWindow), modelView); 
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
				var item = Database.GetUnitsList().Find(a => a.Id == SelectedItem.Id);
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
			List<UnitModel> unitModels = Database.GetUnitsList();

			Items = new ObservableCollection<DataModel>(unitModels);
			SelectedItem = null;
		}
	}
}
