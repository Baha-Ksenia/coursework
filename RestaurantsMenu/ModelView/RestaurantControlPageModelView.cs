using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestaurantsMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о ресторанах
	/// </summary>
	public class RestaurantControlPageModelView : TableEditorViewModel
	{
		public RestaurantControlPageModelView() : base()
		{ }

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new RestaurantEditWindowModelView();
				WindowService.OpenWindow(typeof(RestaurantEditWindow), modelView);
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
				var item = Database.GetRestaurantsList().Find(r => r.Id == SelectedItem.Id);
				var modelView = new RestaurantEditWindowModelView(item);
				WindowService.OpenWindow(typeof(RestaurantEditWindow), modelView);
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
				var item = Database.GetRestaurantsList().Find(a => a.Id == SelectedItem.Id);
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
			var list = Database.GetRestaurantsList();

			List<RestaurantModel> restaurants = new List<RestaurantModel>(0);

			foreach (var restaurant in list)
			{
				RestaurantModel model = new RestaurantModel()
				{
					Id = restaurant.Id,
					Name = restaurant.Name,
					Address = restaurant.Street.Name + " " + restaurant.Address,
					NameDirector = $"{restaurant.SurnameDirector} {restaurant.NameDirector[0]}.{restaurant.PatronymicDirector[0]}",
				};
				restaurants.Add(model);
			}

			Items = new ObservableCollection<DataModel>(restaurants);
			SelectedItem = null;
		}
	}
}
