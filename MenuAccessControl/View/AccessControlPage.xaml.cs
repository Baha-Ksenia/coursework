using ModelViewSystem;
using System.Windows;
using System.Windows.Controls;

namespace MenuAccessControl
{
	/// <summary>
	/// Логика взаимодействия для UserControl1.xaml
	/// </summary>
	public partial class AccessControlPage : Page
    {
        private AccessControlPageModelView _modelView;

		public AccessControlPage(PageAccess pageAccess)
        {
            InitializeComponent();
			_modelView = new AccessControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, ReadButton, MainTable, LogOutButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);
        }

		private void LogOutButton_Click(object sender, RoutedEventArgs e) => WindowService.UserLogOut();

	}
}
