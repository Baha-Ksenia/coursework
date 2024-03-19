using System;
using System.Windows;
using ModelViewSystem;
using DatabaseManagement;

namespace StreetMenu
{
	/// <summary>
	/// ModelView Окна редактирования справочника улиц
	/// </summary>
	public class StreetEditWindowModelView : ModelEditModelView
	{
		private string _streetName;

		public string StreetName
		{
			get { return _streetName; }
			set
			{
				_streetName = value;
				OnPropertyChanged(nameof(StreetName));
			}
		}

		public StreetEditWindowModelView() : base()
		{
			Tittle = "Новая улица";
			StreetName = "";
			MainButtonContent = "Запись";
		}

		public StreetEditWindowModelView(StreetModel streetModel) : base(streetModel)
		{
			Tittle = "Редактирование улицы";
			StreetName = streetModel.Name;
			MainButtonContent = "Редактирование";
		}

		protected override void Add(object obj)
		{
			try
			{
				base.Add(obj);

				StreetModel streetModel = new StreetModel()
				{
					Name = StreetName,
				};
				Database.Add(streetModel);
				SuccessMessage("Улица добавлена");
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
				StreetModel streetModel = new StreetModel()
				{
					Id = DataModel.Id,
					Name = StreetName,
				};

				Database.Edit(streetModel);
				SuccessMessage("Информация отредактирована");
				WindowVisibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
	}
}
