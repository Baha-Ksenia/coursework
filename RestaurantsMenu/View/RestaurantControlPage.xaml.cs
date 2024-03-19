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

namespace RestaurantsMenu
{
	/// <summary>
	/// Логика взаимодействия для RestaurantControlPage.xaml
	/// </summary>
	public partial class RestaurantControlPage : Page
	{
		private RestaurantControlPageModelView _modelView;

		public RestaurantControlPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new RestaurantControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, ReadButton, MainTable, LogOutButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);
		}
	}
}
