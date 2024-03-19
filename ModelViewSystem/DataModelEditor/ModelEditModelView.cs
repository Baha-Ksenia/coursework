using DatabaseManagement;
using System.Windows;

namespace ModelViewSystem
{
	/// <summary>
	/// Класс, представляющий ModelView для окна, предназначенного для редактирования строки из таблицы базы данных.
	/// </summary>
	public class ModelEditModelView : ViewModelBase
	{
		private Visibility _windowVisibility = Visibility.Visible;

		/// <summary>
		/// Команда кнопки записи
		/// </summary>
		public ViewModelCommand EditModelCommand { get; private set; }

		/// <summary>
		/// Создаваемая/Редактируемая строка
		/// </summary>
		public DataModel DataModel { get; private set; }
		/// <summary>
		/// Заголовок для окна
		/// </summary>
		public string Tittle { get; set; }
		/// <summary>
		/// Текст кнопки создания/редакирования
		/// </summary>
		public string MainButtonContent { get; set; }

		/// <summary>
		/// Видимость окно
		/// </summary>
		public Visibility WindowVisibility
		{
			get { return _windowVisibility; }
			set
			{
				_windowVisibility = value;
				OnPropertyChanged(nameof(WindowVisibility));
			}
		}
		
		/// <summary>
		/// Класс доступа к базе данных
		/// </summary>
		protected DatabaseManager Database => DatabaseManager.GetInstance();

		public ModelEditModelView() : base()
		{
			Tittle = string.Empty;
			DataModel = new DataModel();
			EditModelCommand = new ViewModelCommand(Add);
		}

		public ModelEditModelView(DataModel dataModel) : base()
		{
			Tittle = string.Empty;
			DataModel = dataModel;
			EditModelCommand = new ViewModelCommand(Edit);
		}

		/// <summary>
		/// Добавление строки в таблицу
		/// </summary>
		/// <param name="obj"></param>
		protected virtual void Add(object obj)
		{

		}

		/// <summary>
		/// Редактирование существующей строки
		/// </summary>
		/// <param name="obj"></param>
		protected virtual void Edit(object obj)
		{

		}

	}
}
