using System;
using System.Linq;
using ModelViewSystem;
using DatabaseManagement;
using System.Collections.ObjectModel;
using DataExport;

namespace ReceiptsMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о поступлениях продукции
	/// </summary>
	public class ReceiptControlPageModelView : TableEditorViewModel
	{
		public ReceiptControlPageModelView() : base() 
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new ReceiptEditWindowModelView();
				WindowService.OpenWindow(typeof(ReceiptEditWindow), modelView);
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

				var item = Database.GetReceiptsList().Find(s => s.Id == SelectedItem.Id);
				var modelView = new ReceiptEditWindowModelView(item);
				WindowService.OpenWindow(typeof(ReceiptEditWindow), modelView);
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
				var item = Database.GetReceiptsList().First(a => a.Id == SelectedItem.Id);
				Database.Delete(item);
				SuccessMessage("Запись удалено удалена");
				LoadTable();
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Export(object obj)
		{
			try
			{
				base.Export(obj);
				DocumentMaster.Instance().CreateReceiptsDocument(Database.GetReceiptsList());
				SuccessMessage("Документ успешно создан");
				LoadTable();
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void LoadTable()
		{
			Items = new ObservableCollection<DataModel>(Database.GetReceiptsList());
		}

	}
}
