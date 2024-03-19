using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using DatabaseManagement;
using System.Collections.Generic;

namespace ProvidersMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о поставщиках
	/// </summary>
	public class ProviderControlPageModelView : TableEditorViewModel
	{
		public ProviderControlPageModelView() : base()
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new ProviderEditWindowModelView();
				WindowService.OpenWindow(typeof(ProviderEditWindow), modelView);
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

				var item = Database.GetProvidersList().Find(p => p.Id == SelectedItem.Id);
				var modelView = new ProviderEditWindowModelView(item);
				WindowService.OpenWindow(typeof(ProviderEditWindow), modelView);
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
				base.View(obj);
				var item = Database.GetProvidersList().Find(p => p.Id == SelectedItem.Id);
				WindowService.OpenWindow(typeof(ProviderViewWindow), new DataModelViewWindowModelView(item));
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
				var item = Database.GetProvidersList().Find(a => a.Id == SelectedItem.Id);
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
			List<ProviderModel> providers = new List<ProviderModel>(0);
			var list = Database.GetProvidersList();

			foreach (var provider in list)
			{
				ProviderModel providerModel = new ProviderModel()
				{
					Id = provider.Id,
					Name = provider.Name,
					Address = provider.Street.Name + " " + provider.Address,
					NameDirector = $"{provider.SurnameDirector} {provider.NameDirector[0]}.{provider.PatronymicDirector[0]}",
					Bank = provider.Bank,
				};
				providers.Add(providerModel);
			}

			Items = new ObservableCollection<DataModel>(providers);
			SelectedItem = null;
		}
	}
}
