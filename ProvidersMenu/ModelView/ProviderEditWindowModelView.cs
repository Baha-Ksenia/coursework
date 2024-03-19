using DatabaseManagement;
using ModelViewSystem;
using System.Collections.ObjectModel;
using System;
using System.Windows;

namespace ProvidersMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о поставщике
	/// </summary>
	public class ProviderEditWindowModelView : ModelEditModelView
	{
		private string _name;

		private string _ditectorSurname;
		private string _directorName;
		private string _directorPatronymic;

		private string _invoice;

		private ObservableCollection<StreetModel> _streetsList;
		private StreetModel _selectedStreet;
		private string _address;

		private ObservableCollection<BankModel> _banksList;
		private BankModel _selectedBank;
		private string _taxNumber;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string DirectorSurname
		{
			get { return _ditectorSurname; }
			set
			{
				_ditectorSurname = value;
				OnPropertyChanged(nameof(DirectorSurname));
			}
		}
		public string DirectorName
		{
			get { return _directorName; }
			set
			{
				_directorName = value;
				OnPropertyChanged(nameof(DirectorName));
			}
		}
		public string DirectorPatronymic
		{
			get { return _directorPatronymic; }
			set
			{
				_directorPatronymic = value;
				OnPropertyChanged(nameof(DirectorPatronymic));
			}
		}

		public ObservableCollection<StreetModel> StreetList
		{
			get { return _streetsList; }
			set
			{
				_streetsList = value;
				OnPropertyChanged(nameof(StreetList));
			}
		}
		public StreetModel SelectedStreet
		{
			get { return _selectedStreet; }
			set
			{
				_selectedStreet = value;
				OnPropertyChanged(nameof(SelectedStreet));
			}
		}
		public string Address
		{
			get { return _address; }
			set
			{
				_address = value;
				OnPropertyChanged(nameof(Address));
			}
		}

		public ObservableCollection<BankModel> BankList
		{
			get { return _banksList; }
			set
			{
				_banksList = value;
				OnPropertyChanged(nameof(BankList));
			}
		}
		public BankModel SelectedBank
		{
			get { return _selectedBank; }
			set
			{
				_selectedBank = value;
				OnPropertyChanged(nameof(SelectedBank));
			}
		}

		public string Invoice
		{
			get { return _invoice; }
			set
			{
				_invoice = value; 
				OnPropertyChanged(nameof(Invoice));
			}
		}
		public string TaxNumber
		{
			get { return _taxNumber; }
			set
			{
				_taxNumber = value;
				OnPropertyChanged(nameof(TaxNumber));
			}
		}

		public ProviderEditWindowModelView() : base()
		{
			LoadData();
			Tittle = "Новый поставщик";
			MainButtonContent = "Запись";
			
		}

		public ProviderEditWindowModelView(ProviderModel providerModel) : base(providerModel)
		{
			LoadData();
			SelectedBank = providerModel.Bank;
			SelectedStreet = providerModel.Street;

			Name = providerModel.Name;
			TaxNumber = providerModel.TaxNumber;
			Address = providerModel.Address;
			Invoice = providerModel.Invoice;

			DirectorSurname = providerModel.SurnameDirector;
			DirectorName = providerModel.NameDirector;
			DirectorPatronymic = providerModel.PatronymicDirector;

			Tittle = $"Поставщик {providerModel.Name}. Редактирование.";
			MainButtonContent = "Редактирование";
		}

		private void LoadData()
		{ 
			BankList = new ObservableCollection<BankModel>(Database.GetBankList());
			StreetList = new ObservableCollection<StreetModel>(Database.GetStreetsList());
			SelectedBank = null;
			SelectedStreet = null;

			Address = "";
			TaxNumber = "";
			Name = "";
			Invoice = "";

			DirectorSurname = "";
			DirectorName = "";
			DirectorPatronymic = "";
		}

		protected override void Add(object obj)
		{
			try
			{
				if (SelectedBank == null)
					throw new Exception("Банк - не выбран");

				if (SelectedStreet == null)
					throw new Exception("Улица - не выбрана");

				ProviderModel providerModel = new ProviderModel()
				{
					Name = Name,
					Street = SelectedStreet,
					StreetId = SelectedStreet.Id,
					Address = Address,

					SurnameDirector = DirectorSurname,
					NameDirector = DirectorName,
					PatronymicDirector = DirectorPatronymic,

					Bank = SelectedBank,
					BankId = SelectedBank.Id,
					TaxNumber = TaxNumber,
					Invoice = Invoice,
				};

				Database.Add(providerModel);
				SuccessMessage("Поставщик добавлен");
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

				if (SelectedBank == null)
					throw new Exception("Банк - не выбран");

				if (SelectedStreet == null)
					throw new Exception("Улица - не выбрана");

				if (Address == "")
					throw new Exception("Адрес - пусто");

				if (Name == "")
					throw new Exception("Название - пусто");

				ProviderModel providerModel = new ProviderModel()
				{
					Id = DataModel.Id,
					Name = Name,
					Street = SelectedStreet,
					StreetId = SelectedStreet.Id,
					Address = Address,

					SurnameDirector = DirectorSurname,
					NameDirector = DirectorName,
					PatronymicDirector = DirectorPatronymic,

					Bank = SelectedBank,
					BankId = SelectedBank.Id,
					TaxNumber = TaxNumber,
					Invoice = Invoice,
				};

				Database.Edit(providerModel);
				SuccessMessage("Успешно");
				WindowVisibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
	}
}
