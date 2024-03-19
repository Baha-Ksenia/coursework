using ModelViewSystem;
using System.Windows.Controls;

namespace ProvidersMenu
{
	/// <summary>
	/// Логика взаимодействия для ProviderControlPage.xaml
	/// </summary>
	public partial class ProviderControlPage : Page
	{
		private ProviderControlPageModelView _modelView;

		public ProviderControlPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new ProviderControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, ReadButton, MainTable, LogOutButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);
		}
	}
}
