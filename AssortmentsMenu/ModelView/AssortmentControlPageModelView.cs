using System;
using System.Linq;
using ModelViewSystem;
using DatabaseManagement;
using System.Collections.ObjectModel;

namespace AssortmentsMenu
{
	/// <summary>
	/// ModelView Окна редактирования таблицы ассортиментов ресторанов
	/// </summary>
	public class AssortmentControlPageModelView : TableEditorViewModel
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

		public AssortmentControlPageModelView() : base()
		{
			RestaurantsList = new ObservableCollection<RestaurantModel>(Database.GetRestaurantsList());
			SelectedRestaurant = null;
		}

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new AssortmentEditWindowModelView(SelectedRestaurant);
				WindowService.OpenWindow(typeof(AssortmentEditWindow), modelView);
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
				var item = Database.GetAssortmentsList().Find(a => a.Id == SelectedItem.Id);
				var modelView = new AssortmentEditWindowModelView(item);
				WindowService.OpenWindow(typeof(AssortmentEditWindow), modelView);
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
				var item = Database.GetAssortmentsList().Find(a => a.Id == SelectedItem.Id);
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
			if (SelectedRestaurant == null)
				return;

			var list = Database.GetAssortmentsList().Where(a => a.RestaurantId == SelectedRestaurant.Id);
			Items = new ObservableCollection<DataModel>(list);
		}

		public void SelectRestaurant(RestaurantModel restaurantModel)
		{
			SelectedRestaurant = restaurantModel;
		}

	}
}
