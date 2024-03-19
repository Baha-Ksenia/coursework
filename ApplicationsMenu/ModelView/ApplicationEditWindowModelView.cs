using ModelViewSystem;
using System;
using System.Linq;
using DatabaseManagement;
using System.Collections.ObjectModel;
using System.Windows;

namespace ApplicationsMenu
{
	/// <summary>
	/// ModelView Окна редактирования заявки
	/// </summary>
	public class ApplicationEditWindowModelView : ModelEditModelView
	{
		private string _count;

		private ObservableCollection<RestaurantModel> _restaurantsList;
		private RestaurantModel _selectedRestaurant;

		private ObservableCollection<ProductModel> _productsList;
		private ProductModel _selectedProduct;

		public string Count
		{
			get { return _count; }
			set
			{
				_count = value;
				OnPropertyChanged(nameof(Count));
			}
		}

		public ObservableCollection<RestaurantModel> RestaurantsList
		{
			get { return _restaurantsList; }
			set
			{
				_restaurantsList = value;
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

		public ObservableCollection<ProductModel> ProductsList
		{
			get { return _productsList; }
			set
			{
				_productsList = value;
				OnPropertyChanged(nameof(ProductsList));
			}
		}
		public ProductModel SelectedProduct
		{
			get { return _selectedProduct; }
			set
			{
				_selectedProduct = value;
				OnPropertyChanged(nameof(SelectedProduct));
			}
		}

		public ApplicationEditWindowModelView(RestaurantModel restaurantModel) : base()
		{
			LoadData();
			Tittle = "Создание заявки";
			MainButtonContent = "Запись";
			if (restaurantModel != null)
				SelectedRestaurant = Database.GetRestaurantsList().Find(r => r.Id == restaurantModel.Id);
		}

		public ApplicationEditWindowModelView(ApplicationModel applicationModel) : base(applicationModel)
		{
			LoadData();
			Tittle = "Редактирование заявки";
			MainButtonContent = "Редактирование";
			SelectedProduct = ProductsList.First(p => p.Id == applicationModel.ProductId);
			SelectedRestaurant = RestaurantsList.First(p => p.Id == applicationModel.RestaurantId);
			Count = applicationModel.Count.ToString();
		}

		private void LoadData()
		{
			RestaurantsList = new ObservableCollection<RestaurantModel>(Database.GetRestaurantsList());
			ProductsList = new ObservableCollection<ProductModel>(Database.GetProductsList());
			Count = "";
			SelectedRestaurant = null;
			SelectedProduct = null;
		}

		protected override void Add(object obj)
		{
			try
			{
				if (SelectedProduct == null)
					throw new Exception("Продукция - не выбрана");

				if (SelectedRestaurant == null)
					throw new Exception("Рестаран - не выбран");

				if (!int.TryParse(Count, out int count))
					throw new Exception("Количество - некорректный формат");


				ApplicationModel applicationModel = new ApplicationModel()
				{
					Restaurant = SelectedRestaurant,
					RestaurantId = SelectedRestaurant.Id,
					Product = SelectedProduct,
					ProductId = SelectedProduct.Id,
					Date = DateTime.Now.ToShortDateString(),
					Count = count
				};

				Database.Add(applicationModel);
				SuccessMessage("Заявка успешно загружена");
				WindowVisibility = Visibility.Hidden;
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

				if (SelectedProduct == null)
					throw new Exception("Продукция - не выбрана");

				if (SelectedRestaurant == null)
					throw new Exception("Рестаран - не выбран");


				if (!int.TryParse(Count, out int count))
					throw new Exception("Количество - некорректный формат");


				ApplicationModel applicationModel = new ApplicationModel()
				{
					Id = DataModel.Id,
					Restaurant = SelectedRestaurant,
					RestaurantId = SelectedRestaurant.Id,
					Product = SelectedProduct,
					ProductId = SelectedProduct.Id,
					Date = DateTime.Now.ToShortDateString(),
					Count = count
				};

				Database.Edit(applicationModel);
				SuccessMessage("Данные о заявке изменены");
				WindowVisibility = Visibility.Hidden;

			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}


	}
}
