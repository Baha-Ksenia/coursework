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

namespace ReceiptsMenu
{
	/// <summary>
	/// Логика взаимодействия для ReceiptControlPage.xaml
	/// </summary>
	public partial class ReceiptControlPage : Page
	{
		private ReceiptControlPageModelView _modelView;

		public ReceiptControlPage(PageAccess pageAccess)
		{
			InitializeComponent();
			_modelView = new ReceiptControlPageModelView();
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, MainTable, LogOutButton, ExportButton);
			manager.LoadModelView(_modelView);
			manager.LoadPageAccess(pageAccess);
		}
	}
}
