using DatabaseManagement;
using ModelViewSystem;
using System.Windows.Controls;

namespace ReportMenu
{
	/// <summary>
	/// Логика взаимодействия для ReportControlPage.xaml
	/// </summary>
	public partial class ReportControlPage : Page
	{
		private ReportControlPageModelView _modelView;

		public ReportControlPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new ReportControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, MainTable, LogOutButton, ExportButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);
			RestaurantCombobox.ItemsSource = _modelView.RestaurantsList;
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_modelView.SelectedRestaurant = (RestaurantModel)RestaurantCombobox.SelectedItem;
		}
	}
}
