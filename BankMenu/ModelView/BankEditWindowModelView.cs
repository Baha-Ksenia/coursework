using System;
using System.Linq;
using ModelViewSystem;
using DatabaseManagement;
using System.Collections.ObjectModel;

namespace BankMenu
{
	/// <summary>
	/// ModelView Окна редактирования справочника банков
	/// </summary>
	public class BankEditWindowModelView : ModelEditModelView
	{
		private string _name;
		private string _address;

		private ObservableCollection<StreetModel> _streetsList;
		private StreetModel _selectedStreet;

		public string Name
		{
			get { return _name;}
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
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


		public BankEditWindowModelView() : base()
		{
			StreetList = new ObservableCollection<StreetModel>(Database.GetStreetsList());
			SelectedStreet = null;

			Name = "";
			Address = "";
			Tittle = "Новый банк";
			MainButtonContent = "Запись";
		}

		public BankEditWindowModelView(BankModel bankModel) : base(bankModel) 
		{
			Tittle = $"{bankModel.Name}. Редактирование информации.";
			MainButtonContent = "Редактирование";
			StreetList = new ObservableCollection<StreetModel>(Database.GetStreetsList());
			SelectedStreet = Database.GetStreetsList().First(m => m.Id == bankModel.StreetId);

			Name = bankModel.Name;
			Address = bankModel.Address;
		}

		protected override void Add(object obj)
		{
			try
			{
				if (SelectedStreet == null)
					throw new Exception("Улица - не выбрана");

				if (Name == "")
					throw new Exception("Название - пусто");

				if(Address == "")
					throw new Exception("Адрес - пусто");

				BankModel bankModel = new BankModel()
				{
					Id = DataModel.Id,
					StreetId = SelectedStreet.Id,
					Name = Name,
					Street = SelectedStreet,
					Address = Address,
				};

				Database.Add(bankModel);
				SuccessMessage("Банк успешно добавлен");
				WindowVisibility = System.Windows.Visibility.Hidden;
			}
			catch(Exception ex) 
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Edit(object obj)
		{
			try
			{
				base.Edit(obj);

				if (SelectedStreet == null)
					throw new Exception("Улица - не выбрана");

				if (Name == "")
					throw new Exception("Название - пусто");

				if (Address == "")
					throw new Exception("Адрес - пусто");

				BankModel bankModel = new BankModel()
				{
					Id = DataModel.Id,
					Name = Name,
					StreetId = SelectedStreet.Id,
					Street = SelectedStreet,
					Address = Address,
				};

				Database.Edit(bankModel);
				SuccessMessage("Запись успешно изменена");
				WindowVisibility = System.Windows.Visibility.Hidden;
			}
			catch (Exception ex) 
			{
				ErrorMessage(ex.Message);
			}
		}
	}
}
