using ModelViewSystem;
using System;
using System.Collections.Generic;
using DatabaseManagement;
using System.Collections.ObjectModel;

namespace BankMenu
{
	/// <summary>
	/// ModelView Окна редактирования инфомации о банке
	/// </summary>
	public class BankControlPageModelView : TableEditorViewModel
	{

		public BankControlPageModelView() : base() 
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new BankEditWindowModelView();
				WindowService.OpenWindow(typeof(BankEditWindow), modelView);
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

				var item = Database.GetBankList().Find(a => a.Id == SelectedItem.Id);
				var modelView = new BankEditWindowModelView(item);
				WindowService.OpenWindow(typeof(BankEditWindow), modelView);
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
				var item = Database.GetBankList().Find(a => a.Id == SelectedItem.Id);
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
			List<BankModel> banksList = new List<BankModel>(0);

			foreach(var bank in Database.GetBankList()) 
			{
				banksList.Add(new BankModel() 
				{ 
					Id = bank.Id, 
					Name = bank.Name, 
					Address = bank.Street.Name + " " + bank.Address 
				});
			}
			Items = new ObservableCollection<DataModel>(banksList);
			SelectedItem = null;
		}
	}
}
