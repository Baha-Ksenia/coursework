using DatabaseManagement;
using System.Windows;

namespace ModelViewSystem
{
	/// <summary>
	/// ModelView Страницы для просмотра информации из базы данных
	/// </summary>
	public class DataModelViewWindowModelView : ViewModelBase
	{
		private DataModel _dataModel;
		private string _title;
		private Visibility _windowVisibility;

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
		public DataModel DataModel
		{
			get { return _dataModel; }
			set 
			{
				_dataModel = value; 
				OnPropertyChanged(nameof(DataModel));
			}
		}
		public string Title
		{
			get { return _title; }
			set
			{ 
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}


		public DataModelViewWindowModelView(DataModel dataModel) : base()
		{
			DataModel = dataModel;
			WindowVisibility = Visibility.Visible;
		}

	}
}
