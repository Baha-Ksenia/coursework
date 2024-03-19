using System;
using System.Collections.ObjectModel;
using System.Linq;
using DatabaseManagement;
using ModelViewSystem;

namespace RestaurantsMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о ресторане
	/// </summary>
	public class RestaurantEditWindowModelView : ModelEditModelView
	{
		private string _name;
		private string _address;

		private ObservableCollection<StreetModel> _streetsList;
		private StreetModel _selectedStreet;

		private string _ditectorSurname;
		private string _directorName;
		private string _directorPatronymic;

		private string _phoneNumber;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public string PhoneNumber
		{
			get { return _phoneNumber; }
			set
			{ 
				_phoneNumber = value;
				OnPropertyChanged(nameof(PhoneNumber));
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

		public RestaurantEditWindowModelView() : base()
		{
			StreetList = new ObservableCollection<StreetModel>(Database.GetStreetsList());
			SelectedStreet = null;

			Name = "";
			Address = "";

			Tittle = "Новый ресторан.";
			MainButtonContent = "Запись";
		}

		public RestaurantEditWindowModelView(RestaurantModel restaurantModel) : base(restaurantModel)
		{
			Name = restaurantModel.Name;

			StreetList = new ObservableCollection<StreetModel>(Database.GetStreetsList());
			SelectedStreet = StreetList.First(s => s.Id == restaurantModel.StreetId);
			Address = restaurantModel.Address;

			DirectorSurname = restaurantModel.SurnameDirector;
			DirectorName = restaurantModel.NameDirector;
			DirectorPatronymic = restaurantModel.PatronymicDirector;

			PhoneNumber = restaurantModel.PhoneNumber;
			Tittle = $"Редактирование {restaurantModel.Name}";
			MainButtonContent = "Редактирование";
		}

		protected override void Add(object obj)
		{
			try
			{
				if (SelectedStreet == null)
					throw new Exception("Улица не выбрана");

				RestaurantModel restaurantModel = new RestaurantModel()
				{ 
					Name = Name,
					StreetId = SelectedStreet.Id,
					Street = SelectedStreet,

					SurnameDirector = DirectorSurname,
					NameDirector = DirectorName,
					PatronymicDirector = DirectorPatronymic,

					Address = Address,
					PhoneNumber = PhoneNumber,
				};

				Database.Add(restaurantModel);
				SuccessMessage("Ресторан успешно добавлен");
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

				if (SelectedStreet == null)
					throw new Exception("Улица не выбрана");

				RestaurantModel restaurantModel = new RestaurantModel()
				{
					Id = DataModel.Id,
					Name = Name,
					StreetId = SelectedStreet.Id,
					Street = SelectedStreet,

					SurnameDirector = DirectorSurname,
					NameDirector = DirectorName,
					PatronymicDirector = DirectorPatronymic,

					Address = Address,
					PhoneNumber = PhoneNumber,
				};

				Database.Edit(restaurantModel);
				SuccessMessage("Успешное изменение");
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

	}
}
