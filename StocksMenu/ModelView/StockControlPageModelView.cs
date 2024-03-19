using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DatabaseManagement;
using DataExport;

namespace StocksMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о продукции на ЦС
	/// </summary>
	public class StockControlPageModelView : TableEditorViewModel
	{
		public StockControlPageModelView() : base() 
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new StockEditWindowModelView();
				WindowService.OpenWindow(typeof(StockEditWindow), modelView);
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

				var item = Database.GetStocksList().Find(s => s.Id == SelectedItem.Id);
				var modelView = new StockEditWindowModelView(item);
				WindowService.OpenWindow(typeof(StockEditWindow), modelView);
				LoadTable();
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void View(object obj)
		{
			try
			{
				var list = Database.GetStocksList();
				DocumentMaster.Instance().CreateStocksDocument(list);
				SuccessMessage("Документ успешно создан");
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
				var item = Database.GetStocksList().Find(a => a.Id == SelectedItem.Id);
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
			List<StockModel> assortmentsList = new List<StockModel>(0);
			var list = Database.GetStocksList();

			foreach (var item in list)
			{
				assortmentsList.Add(new StockModel()
				{ 
					Id = item.Id,
					Product = item.Product,
					Provider = item.Provider,
					Count = item.Count,
					Price = item.Price * item.Count,
				});
			}

			Items = new ObservableCollection<DataModel>(assortmentsList);
		}
	}
}
