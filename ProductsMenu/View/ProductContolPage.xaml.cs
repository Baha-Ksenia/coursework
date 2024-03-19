using ModelViewSystem;
using System.Windows.Controls;

namespace ProductsMenu
{
	/// <summary>
	/// Логика взаимодействия для ProductContolPage.xaml
	/// </summary>
	public partial class ProductContolPage : Page
	{
		private ProductControlPageModelView _modelView;

		public ProductContolPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new ProductControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, ReadButton, MainTable, LogOutButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);
		}
	}
}
