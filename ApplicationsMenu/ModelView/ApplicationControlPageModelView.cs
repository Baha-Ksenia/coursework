using System;
using System.Collections.Generic;
using ModelViewSystem;
using DatabaseManagement;
using System.Linq;
using System.Collections.ObjectModel;
using DataExport;

namespace ApplicationsMenu
{
	/// <summary>
	/// ModelView Окна редактирования таблицы заявок
	/// </summary>
	public class ApplicationControlPageModelView : TableEditorViewModel
	{
		private ObservableCollection<RestaurantModel> _restaurantList;
		private RestaurantModel _selectedRestaurant;

		public ObservableCollection<RestaurantModel> RestaurantsList
		{
			get { return _restaurantList; }
			set
			{
				_restaurantList = value;
				OnPropertyChanged(nameof(RestaurantsList));
			}
		}
		public RestaurantModel SelectedRestaurant
		{
			get { return _selectedRestaurant; }
			set
			{
				_selectedRestaurant = value;
				OnPropertyChanged(nameof(SelectedRestaurant));
				LoadTable();
			}
		}

		public ApplicationControlPageModelView() : base()
		{
			RestaurantsList = new ObservableCollection<RestaurantModel>(Database.GetRestaurantsList());
			SelectedRestaurant = null;
		}

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new ApplicationEditWindowModelView(SelectedRestaurant);
				WindowService.OpenWindow(typeof(ApplicationEditWindow), modelView);
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

				var item = Database.GetApplicationsList().First(a => a.Id == SelectedItem.Id);
				var modelView = new ApplicationEditWindowModelView(item);
				WindowService.OpenWindow(typeof(ApplicationEditWindow), modelView);
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
				var item = Database.GetApplicationsList().First(a => a.Id == SelectedItem.Id);
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

				if (Items.Count == 0)
					throw new Exception("Таблица пуста");

				DocumentMaster.Instance().CreateApplicationDocument(Database.GetApplicationsList().Where(a => Items.Select(i => i.Id).Contains(a.Id)).ToList());
				SuccessMessage("Документ успешно создан");
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void LoadTable()
		{
			if (SelectedRestaurant == null)
				return;

			Items = new ObservableCollection<DataModel>(Database.GetApplicationsList().Where(a => a.RestaurantId == SelectedRestaurant.Id));
		}

		public void SelectRestaurant(RestaurantModel restaurantModel)
		{
			SelectedRestaurant = restaurantModel;
			LoadTable();
		}
	}
}
