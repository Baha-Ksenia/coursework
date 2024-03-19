using DatabaseManagement;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApplicationsMenu
{
	/// <summary>
	/// Логика взаимодействия для ApplicationControlPage.xaml
	/// </summary>
	public partial class ApplicationControlPage : Page
	{
		private ApplicationControlPageModelView _modelView;

		public ApplicationControlPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new ApplicationControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, MainTable, LogOutButton, ExportButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);

			RestaurantsComboBox.ItemsSource = _modelView.RestaurantsList;
		}

		private void RestaurantsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_modelView.SelectRestaurant((RestaurantModel)RestaurantsComboBox.SelectedItem);
		}
	}
}
