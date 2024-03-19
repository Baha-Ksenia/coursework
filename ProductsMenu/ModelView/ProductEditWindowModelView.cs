using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProductsMenu
{
	/// <summary>
	/// ModelView Окна редактирования информации о продукте
	/// </summary>
	public class ProductEditWindowModelView : ModelEditModelView
	{
		private ObservableCollection<UnitModel> _unitsList;
		private UnitModel _selectedUnit;

		private string _name;

		public UnitModel SelectedUnit
		{
			get { return _selectedUnit; }
			set
			{
				_selectedUnit = value;
				OnPropertyChanged(nameof(SelectedUnit));
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


		public string Name
		{
			get { return _name; }
			set
			{ 
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public ProductEditWindowModelView() : base() 
		{
			LoadData();
			Tittle = "Новый продукт";
			MainButtonContent = "Запись";
		}

		public ProductEditWindowModelView(ProductModel productModel) : base(productModel) 
		{
			LoadData();
			Tittle = $"{productModel.Name}. Редактирование продукта";
			MainButtonContent = "Редактирование";
			SelectedUnit = UnitsList.First(u => u.Id == productModel.UnitId);
			Name = productModel.Name;
		}

		private void LoadData()
		{
			UnitsList = new ObservableCollection<UnitModel>(Database.GetUnitsList());
		}

		protected override void Add(object obj)
		{
			try
			{
				if (SelectedUnit == null)
					throw new Exception("Ед. измерения - не выбрана");

				ProductModel productModel = new ProductModel()
				{
					Name = Name,
					Unit = SelectedUnit,
					UnitId = SelectedUnit.Id,
					
				};

				Database.Add(productModel);
				SuccessMessage("Успешно добавлено");
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

				if (SelectedUnit == null)
					throw new Exception("Ед. измерения - не выбрана");

				ProductModel productModel = new ProductModel()
				{
					Id = DataModel.Id,
					Name = Name,
					Unit = SelectedUnit,
					UnitId = SelectedUnit.Id,
				};

				Database.Edit(productModel);
				SuccessMessage("Информация обновлена");
				WindowVisibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
	}
}
