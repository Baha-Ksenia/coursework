using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ReceiptsMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о поступлении продукции
	/// </summary>
	public class ReceiptEditWindowModelView : ModelEditModelView
	{
		private string _count;

		private ObservableCollection<ProviderModel> _providersList;
		private ProviderModel _selectedProvider;

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

		public ReceiptEditWindowModelView() : base() 
		{
			LoadData();
			Tittle = "Поставка на ЦС";
			MainButtonContent = "Запись";
		}

		public ReceiptEditWindowModelView(ReceiptModel receiptModel) : base(receiptModel) 
		{
			LoadData();
			SelectedProduct = ProductsList.First(p => p.Id == receiptModel.ProductId);
			SelectedProvider = ProvidersList.First(p => p.Id == receiptModel.ProviderId);
			Count = receiptModel.Count.ToString();
			Tittle = "Поставка на ЦС. Редатирование";
			MainButtonContent = "Редактирование";
		}

		private void LoadData()
		{
			ProvidersList = new ObservableCollection<ProviderModel>(Database.GetProvidersList());
			ProductsList = new ObservableCollection<ProductModel>(Database.GetProductsList());
			Count = "";
			SelectedProvider = null;
			SelectedProduct = null;
		}

		protected override void Add(object obj)
		{
			try
			{
				if (!int.TryParse(Count, out int count))
					throw new Exception("Количество - некорректный формат");

				ReceiptModel receiptModel = new ReceiptModel()
				{ 
					Provider = SelectedProvider,
					ProviderId = SelectedProvider.Id,

					Product = SelectedProduct,
					ProductId = SelectedProduct.Id,

					Count = count,
					Date = DateTime.Now.ToShortDateString(),
				};

				Database.Add(receiptModel);
				SuccessMessage("Создана информация о поставке");
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
				if (!int.TryParse(Count, out int count))
					throw new Exception("Количество - некорректный формат");

				ReceiptModel receiptModel = new ReceiptModel()
				{
					Id = DataModel.Id,
					Provider = SelectedProvider,
					ProviderId = SelectedProvider.Id,

					Product = SelectedProduct,
					ProductId = SelectedProduct.Id,

					Count = count,
					Date = ((ReceiptModel)DataModel).Date,
				};

				Database.Edit(receiptModel);
				SuccessMessage("Информация о поставке изменена");
				WindowVisibility = System.Windows.Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
	}
}
