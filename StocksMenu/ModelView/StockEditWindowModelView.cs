using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace StocksMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о продукции на ЦС
	/// </summary>
	public class StockEditWindowModelView : ModelEditModelView
	{
		private string _price;

		private ObservableCollection<ProviderModel> _providersList;
		private ProviderModel _selectedProvider;

		private ObservableCollection<ProductModel> _productsList;
		private ProductModel _selectedProduct;

		public string Price
		{
			get { return _price; }
			set
			{
				_price = value;
				OnPropertyChanged(nameof(Price));
			}
		}

		public ObservableCollection<ProviderModel> ProvidersList
		{
			get { return _providersList; }
			set
			{
				_providersList = value;
				OnPropertyChanged(nameof(ProvidersList));
			}
		}
		public ProviderModel SelectedProvider
		{
			get { return _selectedProvider; }
			set
			{
				_selectedProvider = value;
				OnPropertyChanged(nameof(SelectedProvider));
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

		public StockEditWindowModelView() : base()
		{
			LoadData();
			Tittle = "Новая группа.";
			MainButtonContent = "Запись";
		}

		public StockEditWindowModelView(StockModel stockModel) : base(stockModel)
		{
			LoadData();
			SelectedProvider = Database.GetProvidersList().First(p => p.Id == stockModel.ProviderId);
			SelectedProduct = Database.GetProductsList().First(p => p.Id == stockModel.ProductId);

			Price = stockModel.Price.ToString();
			Tittle = "Редактирование.";
			MainButtonContent = "Редактирование";
		}

		private void LoadData()
		{
			ProductsList = new ObservableCollection<ProductModel>(Database.GetProductsList());
			ProvidersList = new ObservableCollection<ProviderModel>(Database.GetProvidersList());

			SelectedProduct = null;
			SelectedProvider = null;
		}

		protected override void Add(object obj)
		{
			try
			{
				if (SelectedProvider == null)
					throw new Exception("Поставщик не выбран");

				if (!decimal.TryParse(Price, out decimal price))
					throw new Exception("Цена - некорректный формат");

				StockModel stockModel = new StockModel()
				{
					ProviderId = SelectedProvider.Id,
					Provider = SelectedProvider,
					Product = SelectedProduct,
					ProductId = SelectedProvider.Id,
					Price = price,
				};

				Database.Add(stockModel);
				WindowVisibility = Visibility.Hidden;
				SuccessMessage("Запись создана");
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
				if (SelectedProvider == null)
					throw new Exception("Поставщик не выбран");

				if (!decimal.TryParse(Price, out decimal price))
					throw new Exception("Цена - некорректный формат");

				StockModel stockModel = new StockModel()
				{ 
					Id = DataModel.Id,
					ProviderId = SelectedProvider.Id,
					Provider = SelectedProvider,
					Product = SelectedProduct,
					ProductId = SelectedProduct.Id,
					Price = price,
					Count = ((StockModel)DataModel).Count,
				};

				Database.Edit(stockModel);
				WindowVisibility = Visibility.Hidden;
				SuccessMessage("Запись изменена");
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}


	}
}
