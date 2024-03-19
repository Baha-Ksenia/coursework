using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Windows;

namespace UnitsMenu
{
	/// <summary>
	/// ModelView Окна редактирования справочника ед. измерения
	/// </summary>
	public class UnitEditWindowModelView : ModelEditModelView
	{
		private string _unitName;

		public string UnitName
		{
			get { return _unitName; }
			set
			{ 
				_unitName = value;
				OnPropertyChanged(nameof(UnitName));
			}
		}

		public UnitEditWindowModelView() : base()
		{
			Tittle = "Новая единица измерения";
			UnitName = "";
			MainButtonContent = "Запись";
			
		}

		public UnitEditWindowModelView(UnitModel unitModel) : base(unitModel)
		{
			Tittle = "Редактирование ед. измерения";
			UnitName = unitModel.Name;
			MainButtonContent = "Редактирование";
		}

		protected override void Add(object obj)
		{
			try
			{
				base.Add(obj);

				UnitModel unitModel = new UnitModel
				{
					Name = UnitName,
				};
				Database.Add(unitModel);
				SuccessMessage("Единица измерения добавлена");
				WindowVisibility = Visibility.Hidden;
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

				UnitModel unitModel = new UnitModel
				{
					Id = DataModel.Id,
					Name = UnitName,
				};
				Database.Edit(unitModel);
				SuccessMessage("Информация изменена");
				WindowVisibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

	}
}
