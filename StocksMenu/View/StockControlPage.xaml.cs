using ModelViewSystem;
using System.Windows.Controls;

namespace StocksMenu
{
	/// <summary>
	/// Логика взаимодействия для StockControlPage.xaml
	/// </summary>
	public partial class StockControlPage : Page
	{
		private StockControlPageModelView _modelView;

		public StockControlPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new StockControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, ReadButton, MainTable, LogOutButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);
		}
	}
}
