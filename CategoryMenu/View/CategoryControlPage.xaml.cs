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

namespace CategoryMenu
{
	/// <summary>
	/// Логика взаимодействия для CategoryControlPage.xaml
	/// </summary>
	public partial class CategoryControlPage : Page
	{
		private CategoryControlPageModelView _modelView;

		public CategoryControlPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new CategoryControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, ReadButton, MainTable, LogOutButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);
		}
	}
}
