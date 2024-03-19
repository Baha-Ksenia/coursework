using System;
using System.Collections.ObjectModel;
using DatabaseManagement;

namespace ModelViewSystem
{
	/// <summary>
	/// Класс, представляющий ModelView для окна, предназначенного для просмотра и редактирования таблицы базы данных.
	/// </summary>
	public class TableEditorViewModel : ViewModelBase
	{
		public ViewModelCommand AddCommand { get; protected set; }
		public ViewModelCommand EditCommand { get; protected set; }
		public ViewModelCommand DeleteCommand { get; protected set; }
		public ViewModelCommand ViewCommand { get; protected set; }

		public ViewModelCommand LogOutCommand { get; private set; }
		public ViewModelCommand ExportCommand { get; private set; }

		private ObservableCollection<DataModel> _items;
		private DataModel _selectedItem;

		public ObservableCollection<DataModel> Items
		{
			get { return _items; }
			set
			{
				_items = value;
				OnPropertyChanged(nameof(Items));
			}
		}
		public DataModel SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));
			}
		}

		/// <summary>
		/// Класс доступа к базе данных
		/// </summary>
		protected DatabaseManager Database => DatabaseManager.GetInstance();

		public TableEditorViewModel()
		{
			SelectedItem = null;
			Items = new ObservableCollection<DataModel>();

			AddCommand = new ViewModelCommand(Add);
			EditCommand = new ViewModelCommand(Edit);
			DeleteCommand = new ViewModelCommand(Delete);
			ViewCommand = new ViewModelCommand(View);

			LogOutCommand = new ViewModelCommand(LogOut);
			ExportCommand = new ViewModelCommand(Export);
			LoadTable();
		}

		private void LogOut(object obj) => WindowService.UserLogOut();

		/// <summary>
		/// Добавление строки.
		/// </summary>
		/// <param name="obj"></param>
		protected virtual void Add(object obj)
		{ }
		/// <summary>
		/// Редактирование строки.
		/// </summary>
		protected virtual void Edit(object obj)
		{
			if (SelectedItem == null)
				throw new Exception("Строка не выбрана");
		}
		/// <summary>
		/// Удаление строки.
		/// </summary>
		protected virtual void Delete(object obj)
		{
			if (SelectedItem == null)
				throw new Exception("Строка не выбрана");
		}

		/// <summary>
		/// Просмотр информации из таблицы.
		/// </summary>
		protected virtual void View(object obj)
		{
			if (SelectedItem == null)
				throw new Exception("Строка не выбрана");
		}

		/// <summary>
		/// Экспорт информации 
		/// </summary>
		protected virtual void Export(object obj)
		{ 
			
		}

		/// <summary>
		/// Загрузка таблицы данных
		/// </summary>
		protected virtual void LoadTable()
		{ }
	}
}
