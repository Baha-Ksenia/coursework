using System;
using ModelViewSystem;
using DatabaseManagement;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;

namespace ReportMenu
{
	/// <summary>
	/// ModelView Окна редактирования отчета по выручке
	/// </summary>
	public class ReportEditWindowModelView : ModelEditModelView
	{
		private RestaurantModel _restaurant;
		private string _soldVolume;
		private string _proceeds;

		private ObservableCollection<UnitModel> _unitsList;
		private UnitModel _selectedUnit;

		private ObservableCollection<CategoryModel> _categoriesList;
		private CategoryModel _selectedCategory;

		public ObservableCollection<CategoryModel> CategoriesList
		{
			get { return _categoriesList; }
			set
			{ 
				_categoriesList = value;
				OnPropertyChanged(nameof(CategoriesList));
			}
		}
		public CategoryModel SelectedCategory {
			get
			{
				return _selectedCategory; 
			}
			set 
			{
				_selectedCategory = value;
				OnPropertyChanged(nameof(SelectedCategory));
			}
		}

		public ObservableCollection<UnitModel> UnitsList
		{
			get { return _unitsList; }
			set
			{ 
				_unitsList = value;
				OnPropertyChanged(nameof(UnitsList));
			}
		}
		public UnitModel SelectedUnit
		{
			get { return _selectedUnit; }
			set
			{ 
				_selectedUnit = value;
				OnPropertyChanged(nameof(SelectedUnit));
			}
		}

		public string SoldVolume
		{
			get { return _soldVolume; }
			set
			{
				_soldVolume = value;
				OnPropertyChanged(nameof(SoldVolume));
			}
		}
		public string Proceeds
		{
			get { return _proceeds; }
			set
			{
				_proceeds = value;
				OnPropertyChanged(nameof(Proceeds));
			}
		}

		public ReportEditWindowModelView(RestaurantModel restaurantModel) : base()
		{
			LoadData(restaurantModel);
			Tittle = "Новый отчёт о выручке.";
			MainButtonContent = "Запись";
		}

		public ReportEditWindowModelView(RestaurantModel restaurantModel, ReportModel reportModel) : base(reportModel) 
		{
			LoadData(restaurantModel);
			SelectedCategory = Database.GetCategoriesList().First(c => c.Id == reportModel.Categoryid);
			SelectedUnit = Database.GetUnitsList().First(u => u.Id == reportModel.UnitId);
			Proceeds = reportModel.Proceeds.ToString();
			SoldVolume = reportModel.Count.ToString();
			Tittle = $"Отчёт {restaurantModel.Name}. Редактирование.";
			MainButtonContent = "Редактирование";
		}

		private void LoadData(RestaurantModel restaurantModel)
		{
			_restaurant = restaurantModel;
			CategoriesList = new ObservableCollection<CategoryModel>(Database.GetCategoriesList());
			UnitsList = new ObservableCollection<UnitModel>(Database.GetUnitsList());
			SelectedCategory = null;
			SelectedUnit = null;
		}

		protected override void Add(object obj)
		{
			try
			{
				if (!int.TryParse(SoldVolume, out int count))
					throw new Exception("Объем продукции - должно быть целое чило");

				if(!decimal.TryParse(Proceeds, out decimal proceeds))
					throw new Exception("Выручка - некорректный формат");

				ReportModel reportModel = new ReportModel()
				{
					Date = DateTime.Now.ToShortDateString(),
					RestaurantId = _restaurant.Id,
					Restaurant = _restaurant,

					Categoryid = SelectedCategory.Id,
					Category = SelectedCategory,

					Unit = SelectedUnit,
					UnitId = SelectedUnit.Id,

					Count = count,
					Proceeds = proceeds,
				};

				Database.Add(reportModel);
				SuccessMessage("Отчет загружен");
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
				if (!int.TryParse(SoldVolume, out int count))
					throw new Exception("Объем продукции - должно быть целое чило");

				if (!decimal.TryParse(Proceeds, out decimal proceeds))
					throw new Exception("Выручка - некорректный формат");

				ReportModel reportModel = new ReportModel()
				{
					Id = DataModel.Id,
					Date = DateTime.Now.ToShortDateString(),
					RestaurantId = _restaurant.Id,
					Restaurant = _restaurant,

					Categoryid = SelectedCategory.Id,
					Category = SelectedCategory,

					Count = count,
					Proceeds = proceeds,
				};

				Database.Add(reportModel);
				SuccessMessage("Данные в отчете изменены");
				WindowVisibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

	}
}
