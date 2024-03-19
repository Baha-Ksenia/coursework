using DatabaseManagement;
using ModelViewSystem;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AssortmentsMenu
{
	/// <summary>
	/// Логика взаимодействия для AssortmentControlPage.xaml
	/// </summary>
	public partial class AssortmentControlPage : Page
	{
		private AssortmentControlPageModelView _modelView;

		public AssortmentControlPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new AssortmentControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, ReadButton, MainTable, LogOutButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);

			RestaurantsComboBox.ItemsSource = _modelView.RestaurantsList;
		}

		private void RestaurantsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => _modelView.SelectRestaurant((RestaurantModel)RestaurantsComboBox.SelectedItem);

	}
}
