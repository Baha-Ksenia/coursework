using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelViewSystem;
using DatabaseManagement;


namespace CategoryMenu
{
	/// <summary>
	/// ModelView Окна редактирования справочника категорий
	/// </summary>
	public class CategoryEditWindowModelView : ModelEditModelView
	{
		private string _name;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public CategoryEditWindowModelView() : base()
		{
			Name = "";
			Tittle = "Новая категория";
			MainButtonContent = "Запись";
		}

		public CategoryEditWindowModelView(CategoryModel categoryModel) : base(categoryModel) 
		{
			Name = categoryModel.Name;
			Tittle = "Редактирование категории";
			MainButtonContent = "Редактирование";
		}

		protected override void Add(object obj)
		{
			try
			{
				if (Name == "")
					throw new Exception("Наименование - пусто");

				CategoryModel categoryModel = new CategoryModel()
				{
					Name = Name,
				};
				Database.Add(categoryModel);
				SuccessMessage("Категория успешно добавлена");
				WindowVisibility = System.Windows.Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Edit(object obj)
		{
			try
			{
				base.Edit(obj);

				if (Name == "")
					throw new Exception("Наименование - пусто");

				CategoryModel categoryModel = new CategoryModel()
				{ 
					Id = DataModel.Id,
					Name = Name,
				};
				Database.Edit(categoryModel);
				SuccessMessage("Информация изменена");
				WindowVisibility = System.Windows.Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
	}
}
