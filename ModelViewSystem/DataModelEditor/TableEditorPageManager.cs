using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ModelViewSystem
{
	/// <summary>
	/// Класс, представляющий ModelView для окна, предназначенного для просмотра и редактирования таблицы базы данных.
	/// </summary>
	public class TableEditorPageManager
	{
		private DataGrid _table;
		private Button _addButton;
		private Button _editButton;
		private Button _deleteButton;

		private Button _viewButton;
		private Button _exportButton;

		private Button _logOutButton;

		public TableEditorPageManager(Button addButton, Button editButton, Button deleteButton, Button viewButton, DataGrid table, Button loginButton)
		{
			_table = table;

			_addButton = addButton;
			_editButton = editButton;
			_deleteButton = deleteButton;
			_viewButton = viewButton;

			_logOutButton = loginButton;
		}
		public TableEditorPageManager(Button addButton, Button editButton, Button deleteButton, DataGrid table, Button loginButton)
		{
			_table = table;

			_addButton = addButton;
			_editButton = editButton;
			_deleteButton = deleteButton;

			_logOutButton = loginButton;
		}
		public TableEditorPageManager(Button addButton, Button editButton, Button deleteButton, DataGrid table, Button loginButton, Button exportButton): this (addButton, editButton, deleteButton, table, loginButton)
		{
			_exportButton = exportButton;
		}

		/// <summary>
		/// Установка параметров
		/// </summary>
		private void SetSettings()
		{
			_table.AutoGenerateColumns = false;
			_table.SelectionMode = DataGridSelectionMode.Single;
			_table.IsReadOnly = true;
			_table.CanUserResizeColumns = false;
			_table.CanUserDeleteRows = false;
			_table.CanUserAddRows = false;
			_table.CanUserReorderColumns = false;
		}

		/// <summary>
		/// Формирование связей для ModelView (MVVM)
		/// </summary>
		private void SetBinding(TableEditorViewModel viewModel)
		{
			if (viewModel == null)
				return;

			Binding itemsBinding = new Binding("Items");
			itemsBinding.Source = viewModel;

			Binding selectedItemBinding = new Binding("SelectedItem");
			selectedItemBinding.Mode = BindingMode.TwoWay;
			selectedItemBinding.Source = viewModel;

			_table.AutoGenerateColumns = false;
			_table.SelectionMode = DataGridSelectionMode.Single;
			_table.IsReadOnly = true;

			_table.SetBinding(DataGrid.SelectedItemProperty, selectedItemBinding);
			_table.SetBinding(DataGrid.ItemsSourceProperty, itemsBinding);
		}

		/// <summary>
		/// Загрузка ModelView (MVVM)
		/// </summary>
		public void LoadModelView(TableEditorViewModel viewModel)
		{
			if (viewModel == null)
				return;

			_addButton.Command = viewModel.AddCommand;
			_editButton.Command = viewModel.EditCommand;
			_deleteButton.Command = viewModel.DeleteCommand;

			if(_viewButton != null)
				_viewButton.Command = viewModel.ViewCommand;

			_logOutButton.Command = viewModel.LogOutCommand;

			if(_exportButton != null)
				_exportButton.Command = viewModel.ExportCommand;

			SetBinding(viewModel);
			SetSettings();
		}

		/// <summary>
		/// Доступ к кнопкам, в зависимости от доступа
		/// </summary>
		public void LoadPageAccess(PageAccess pageAccess)
		{
			if(_viewButton != null)
				_viewButton.IsEnabled = pageAccess.Read;
			_addButton.IsEnabled = pageAccess.Add;
			_editButton.IsEnabled = pageAccess.Edit;
			_deleteButton.IsEnabled = pageAccess.Delete;	
		}
	}
}
