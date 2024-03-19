using System;
using ModelViewSystem;
using DatabaseManagement;
using System.Collections.ObjectModel;
using System.Linq;
using DataExport;

namespace ReportMenu
{
	/// <summary>
	/// ModelView Окна редактирования отчетов по выручке
	/// </summary>
	public class ReportControlPageModelView : TableEditorViewModel
	{
		private ObservableCollection<RestaurantModel> _restaurantList;
		private RestaurantModel _selectedRestaurant;

		public ObservableCollection<RestaurantModel> RestaurantsList
		{
			get { return _restaurantList; }
			set
			{ 
				_restaurantList = value;
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
				LoadTable();
			}
		}


		public ReportControlPageModelView() : base() 
		{ 
			RestaurantsList = new ObservableCollection<RestaurantModel>(Database.GetRestaurantsList());
			SelectedRestaurant = null;
		}

		protected override void Add(object obj)
		{
			try
			{
				if (SelectedRestaurant == null)
					throw new Exception("Ресторан - не выбран");

				var modelView = new ReportEditWindowModelView(SelectedRestaurant);
				WindowService.OpenWindow(typeof(ReportEditWindow), modelView);
				LoadTable();
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

				if (SelectedRestaurant == null)
					throw new Exception("Ресторан - не выбран");

				var item = Database.GetRepostsList().Find(r => r.Id == SelectedItem.Id);
				var modelView = new ReportEditWindowModelView(SelectedRestaurant, item);
				WindowService.OpenWindow(typeof(ReportEditWindow), modelView);
				LoadTable();
			}
			catch(Exception ex) 
			{
				ErrorMessage(ex.Message);
			}
		}


		protected override void Delete(object obj)
		{
			try
			{
				base.Delete(obj);

				var item = Database.GetRepostsList().Find(r => r.Id == SelectedItem.Id);
				Database.Delete(item);
				LoadTable();
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Export(object obj)
		{
			try
			{
				base.Export(obj);

				if (Items.Count == 0)
					throw new Exception("Таблица пуста");

				DocumentMaster.Instance().CreateReportDocument(Database.GetRepostsList().Where(r => Items.Select(i => i.Id).Contains(r.Id)).ToList());
				SuccessMessage("Документ успешно создан");
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void LoadTable()
		{
			if (SelectedRestaurant == null)
				return;

			Items = new ObservableCollection<DataModel>(Database.GetRepostsList().Where(r => r.RestaurantId == SelectedRestaurant.Id));
		}

	}
}
