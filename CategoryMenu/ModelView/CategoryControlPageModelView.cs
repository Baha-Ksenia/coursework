using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CategoryMenu
{
	/// <summary>
	/// ModelView Окна редактирования справочника категорий
	/// </summary>
	public class CategoryControlPageModelView : TableEditorViewModel
	{
		public CategoryControlPageModelView() : base() 
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new CategoryEditWindowModelView();
				WindowService.OpenWindow(typeof(CategoryEditWindow), modelView);
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

				var item = Database.GetCategoriesList().Find(c => c.Id == SelectedItem.Id);
				var modelView = new CategoryEditWindowModelView(item);
				WindowService.OpenWindow(typeof(CategoryEditWindow), modelView);
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
				var item = Database.GetCategoriesList().Find(a => a.Id == SelectedItem.Id);
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
			List<CategoryModel> categoriesList = Database.GetCategoriesList();
			Items = new ObservableCollection<DataModel>(categoriesList);
			SelectedItem = null;
		}

	}
}
