using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DatabaseManagement;
using ModelViewSystem;

namespace ProductsMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о продукте
	/// </summary>
	public class ProductControlPageModelView : TableEditorViewModel
	{
		public ProductControlPageModelView() : base()
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new ProductEditWindowModelView();
				WindowService.OpenWindow(typeof(ProductEditWindow), modelView);
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
				var item = Database.GetProductsList().Find(a => a.Id == SelectedItem.Id);
				var modelView = new ProductEditWindowModelView(item);
				WindowService.OpenWindow(typeof(ProductEditWindow), modelView);
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
				var item = Database.GetProductsList().Find(a => a.Id == SelectedItem.Id);
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
			List<ProductModel> productsList = Database.GetProductsList();
			Items = new ObservableCollection<DataModel>(productsList);
			SelectedItem = null;
		}
	}
}
