using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AssortmentsMenu
{
	/// <summary>
	/// ModelView Окна редактирования ассортимента ресторана
	/// </summary>
	public class AssortmentEditWindowModelView : ModelEditModelView
	{
		private ObservableCollection<RestaurantModel> _restaurantList;
		private RestaurantModel _selectedRestaurant;

		private string _name;
		private string _price;

		private ObservableCollection<CategoryModel> _categoryList;
		private CategoryModel _selectedCategory;

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
			}
		}

		public ObservableCollection<CategoryModel> CategoryList
		{
			get { return _categoryList; }
			set
			{
				_categoryList = value;
				OnPropertyChanged(nameof(CategoryList));
			}
		}
		public CategoryModel SelectedCategory
		{
			get { return _selectedCategory; }
			set
			{
				_selectedCategory = value;
				OnPropertyChanged(nameof(SelectedCategory));
			}
		}

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public string Price
		{
			get { return _price; }
			set 
			{
				_price = value;
				OnPropertyChanged(nameof(Price));
			}
		}

		public AssortmentEditWindowModelView(RestaurantModel restaurantModel) : base() 
		{
			LoadData();
			Tittle = "Создание информации об ассортименте";
			MainButtonContent = "Запись";
			SelectedRestaurant = Database.GetRestaurantsList().First(r => r.Id == restaurantModel.Id);
		}

		public AssortmentEditWindowModelView(AssortmentModel assortmentModel) : base(assortmentModel) 
		{
			LoadData();
			Tittle = $"{assortmentModel.Name}. Редактирование";
			MainButtonContent = "Редактирование";
			SelectedCategory = CategoryList.First(c => c.Id == assortmentModel.CategoryId);
			SelectedRestaurant = Database.GetRestaurantsList().Find(r => r.Id == assortmentModel.RestaurantId);
			Price = assortmentModel.Price.ToString();
			Name = assortmentModel.Name;
		}

		private void LoadData()
		{
			RestaurantsList = new ObservableCollection<RestaurantModel>(Database.GetRestaurantsList());
			CategoryList = new ObservableCollection<CategoryModel>(Database.GetCategoriesList());
			SelectedRestaurant = null;
		}

		protected override void Add(object obj)
		{
			try
			{
				if (!decimal.TryParse(Price, out decimal price))
					throw new Exception("Цена - указано некорректно");

				if (SelectedRestaurant == null)
					throw new Exception("Ресторан - не выбрано");

				AssortmentModel assortmentModel = new AssortmentModel()
				{
					Name = Name,
					Price = price,
					RestaurantId = SelectedRestaurant.Id,
					Restaurant = SelectedRestaurant,
					Category = SelectedCategory,
					CategoryId = SelectedCategory.Id,
				};

				Database.Add(assortmentModel);
				SuccessMessage("Блюдо добавлено");
				WindowVisibility = System.Windows.Visibility.Hidden;
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
				if (!decimal.TryParse(Price, out decimal price))
					throw new Exception("Цена - указано некорректно");

				if (SelectedRestaurant == null)
					throw new Exception("Ресторан - не выбрано");

				AssortmentModel assortmentModel = new AssortmentModel()
				{
					Id = DataModel.Id,
					Name = Name,
					Price = price,
					RestaurantId = SelectedRestaurant.Id,
					Restaurant = SelectedRestaurant,
					Category = SelectedCategory,
					CategoryId = SelectedCategory.Id,
				};

				Database.Edit(assortmentModel);
				SuccessMessage("Ассортимент изменен");
				WindowVisibility = System.Windows.Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
	}
}
