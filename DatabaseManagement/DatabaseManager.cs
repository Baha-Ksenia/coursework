using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DatabaseManagement
{
	/// <summary>
	/// Управление базой данных SQLite.
	/// </summary>
	public class DatabaseManager : DbContext
	{
		// Реаоизовация паттерна SINGLETON
		#region SINGLETON

		private static DatabaseManager _instance;
		public static DatabaseManager GetInstance()
		{
			if (_instance == null)
				_instance = new DatabaseManager();
			return _instance;
		}

		#endregion

		//Связь с таблицами базы данных
		/// <summary> Пользователи </summary>
		private DbSet<UserModel> Users { get; set; }
		/// <summary> Меню </summary>
		private DbSet<MenuItemModel> MenuItems { get; set; }
		/// <summary> Доступ к меню </summary>
		private DbSet<MenuItemAccessModel> AccessList { get; set; }


		/// <summary> Ед. измерения </summary>
		private DbSet<UnitModel> Units { get; set; }
		/// <summary> Улицы </summary>
		private DbSet<StreetModel> Streets { get; set; }
		/// <summary> Категории </summary>
		private DbSet<CategoryModel> Categories { get; set; }


		/// <summary> Информация о продуктах на ЦС </summary>
		private DbSet<StockModel> Stocks { get; set; }
		/// <summary>  Данные ассортимента ресторанов </summary>
		private DbSet<AssortmentModel> Assortments { get; set; }

		/// <summary> Заявки </summary>
		private DbSet<ApplicationModel> Applications { get; set; }
		/// <summary> Поступления </summary>
		private DbSet<ReceiptModel> Receipts { get; set; }
		/// <summary> Отчеты ресторанов </summary>
		private DbSet<ReportModel> Reports { get; set; }

		/// <summary> Банки </summary>
		private DbSet<BankModel> Banks { get; set; }
		/// <summary> Продукция </summary>
		private DbSet<ProductModel> Products { get; set; }
		/// <summary> Рестораны </summary>
		private DbSet<RestaurantModel> Restaurants { get; set; }
		/// <summary> Поставщики </summary>
		private DbSet<ProviderModel> Providers { get; set; }


		#region MODEL_CREATING
		/// <summary>
		/// Переопределенный метод для настройки параметров подключения к базе данных.
		/// </summary>
		/// <param name="optionsBuilder">Построитель опций контекста базы данных.</param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

			connectString = baseDirectory + "\\" + connectString;

			optionsBuilder.UseSqlite($"Data Source={connectString}");
			optionsBuilder.UseLazyLoadingProxies();
		}

		public DatabaseManager() : base()
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UnitModel>().ToTable("Units");
			modelBuilder.Entity<UnitModel>().HasKey(u => u.Id);

			modelBuilder.Entity<StreetModel>().ToTable("Streets");
			modelBuilder.Entity<StreetModel>().HasKey(e => e.Id);

			modelBuilder.Entity<CategoryModel>().ToTable("Categories");
			modelBuilder.Entity<CategoryModel>().HasKey(e => e.Id);

			modelBuilder.Entity<UserModel>().ToTable("Users");
			modelBuilder.Entity<UserModel>().HasKey(e => e.Id);

			modelBuilder.Entity<MenuItemModel>().ToTable("MenuItems");
			modelBuilder.Entity<MenuItemModel>().HasKey(e => e.Id);

			modelBuilder.Entity<MenuItemAccessModel>().ToTable("AccessList");
			modelBuilder.Entity<MenuItemAccessModel>().HasKey(e => e.Id);
			modelBuilder.Entity<MenuItemAccessModel>()
				.HasOne(m => m.MenuItem)
				.WithMany()
				.HasForeignKey(m => m.MenuId);
			modelBuilder.Entity<MenuItemAccessModel>()
				.HasOne(m => m.User)
				.WithMany()
				.HasForeignKey(m => m.UserId);

			modelBuilder.Entity<BankModel>().ToTable("Banks");
			modelBuilder.Entity<BankModel>().HasKey(e => e.Id);
			modelBuilder.Entity<BankModel>()
				.HasOne(m => m.Street)
				.WithMany()
				.HasForeignKey(m => m.StreetId);

			modelBuilder.Entity<ProductModel>().ToTable("Products");
			modelBuilder.Entity<ProductModel>().HasKey(e => e.Id);
			modelBuilder.Entity<ProductModel>()
				.HasOne(m => m.Unit)
				.WithMany()
				.HasForeignKey(m => m.UnitId);

			modelBuilder.Entity<RestaurantModel>().ToTable("Restaurants");
			modelBuilder.Entity<RestaurantModel>().HasKey(e => e.Id);
			modelBuilder.Entity<RestaurantModel>()
				.HasOne(m => m.Street)
				.WithMany()
				.HasForeignKey(m => m.StreetId);

			modelBuilder.Entity<StockModel>().ToTable("Stocks");
			modelBuilder.Entity<StockModel>().HasKey(e => e.Id);
			modelBuilder.Entity<StockModel>()
				.HasOne(m => m.Product)
				.WithMany()
				.HasForeignKey(m => m.ProductId);
			modelBuilder.Entity<StockModel>()
				.HasOne(m => m.Provider)
				.WithMany()
				.HasForeignKey(m => m.ProviderId);

			modelBuilder.Entity<ReceiptModel>().ToTable("Receipts");
			modelBuilder.Entity<ReceiptModel>().HasKey(e => e.Id);
			modelBuilder.Entity<ReceiptModel>()
				.HasOne(m => m.Product)
				.WithMany()
				.HasForeignKey(m => m.ProductId);
			modelBuilder.Entity<ReceiptModel>()
				.HasOne(m => m.Provider)
				.WithMany()
				.HasForeignKey(m => m.ProviderId);

			modelBuilder.Entity<AssortmentModel>().ToTable("Assortments");
			modelBuilder.Entity<AssortmentModel>().HasKey(e => e.Id);
			modelBuilder.Entity<AssortmentModel>()
				.HasOne(m => m.Restaurant)
				.WithMany()
				.HasForeignKey(m => m.RestaurantId);
			modelBuilder.Entity<AssortmentModel>()
				.HasOne(m => m.Category)
				.WithMany()
				.HasForeignKey(m => m.CategoryId);

			modelBuilder.Entity<ReportModel>().ToTable("Reports");
			modelBuilder.Entity<ReportModel>().HasKey(e => e.Id);
			modelBuilder.Entity<ReportModel>()
				.HasOne(m => m.Category)
				.WithMany()
				.HasForeignKey(m => m.Categoryid);
			modelBuilder.Entity<ReportModel>()
				.HasOne(m => m.Restaurant)
				.WithMany()
				.HasForeignKey(m => m.RestaurantId);
			modelBuilder.Entity<ReportModel>()
				.HasOne(m => m.Unit)
				.WithMany()
				.HasForeignKey(m => m.UnitId);

			modelBuilder.Entity<ApplicationModel>().ToTable("Applications");
			modelBuilder.Entity<ApplicationModel>().HasKey(e => e.Id);
			modelBuilder.Entity<ApplicationModel>()
				.HasOne(m => m.Product)
				.WithMany()
				.HasForeignKey(m => m.ProductId);
			modelBuilder.Entity<ApplicationModel>()
				.HasOne(m => m.Restaurant)
				.WithMany()
				.HasForeignKey(m => m.RestaurantId);

		}

        #endregion

        // Пользователи
        #region USERS

        // Код для шифрования AES - 32 бита
        private const string EncryptKey = "12345678901234567890123456789012";

        public List<UserModel> GetUsersList() => Users.ToList();

        public void Add(UserModel user)
        {
            if (user.Login.Length < 4)
                throw new Exception("Логин слишком короткий");

            if (user.Password.Length < 5)
                throw new Exception("Пароль слишком короткий");

            if (Users.Any(u => u.Login == user.Login))
                throw new Exception($"Пользователь с логином {user.Login} уже существует");

            StringEncryption encryption = new StringEncryption(EncryptKey);
            user.Password = encryption.Encrypt(user.Password);

            Users.Add(user);
			SaveChanges();

			//открываем ему доступ к стандартным меню

			var defaultMenu = GetMenuList().Where(m => m.Default > 0).ToList();

			foreach (var menuItem in defaultMenu)
			{
				Add(new MenuItemAccessModel()
				{
					UserId = user.Id,
					User = user,
					MenuId = menuItem.Id,
					MenuItem = menuItem,
					Read = 1,
					Add = 1,
					Edit = 1,
					Delete = 1,
				});
			}
        }

        public void Edit(UserModel user)
        {
            var userToEdit = Users.Find(user.Id);

            if (Users.Any(u => u.Login == user.Login && u.Id != user.Id))
                throw new Exception($"Пользователь с логином {user.Login} уже существует");

            if (user.Login.Length < 4)
                throw new Exception("Логин слишком короткий");

            if (user.Password.Length < 5)
                throw new Exception("Пароль слишком короткий");

            if (userToEdit == null)
                throw new Exception("Данные для редактирования не найдены");

            StringEncryption encryption = new StringEncryption(EncryptKey);
            user.Password = encryption.Encrypt(user.Password);

            Entry(userToEdit).CurrentValues.SetValues(user);
            SaveChanges();

        }

        public void Delete(UserModel user)
        {
            if (!Users.Any(u => u.Id == user.Id))
                throw new Exception("Данного пользователя не существует");

            Users.Remove(user);
            SaveChanges();
        }

        public int Authorization(string login, string password)
        {
            StringEncryption encryption = new StringEncryption(EncryptKey);
            string cryptPassword = encryption.Encrypt(password);

            if (!Users.Any(u => u.Login == login && u.Password == cryptPassword))
                throw new Exception("Неверный логин или пароль");

            return Users.First(u => u.Login == login && u.Password == cryptPassword).Id;
        }

        #endregion

        // Меню
        #region MENU_ITEMS

        public List<MenuItemModel> GetMenuList() => MenuItems.ToList();

		#endregion

		// Доступ к меню
		#region ACCESS

		public List<MenuItemAccessModel> GetMenuAccessList() => AccessList.OrderBy(access => access.MenuItem.Order).ToList();

		public List<MenuItemAccessModel> LoadItems(UserModel user)
		{
			var userAcess = AccessList.ToList().Where(a => a.UserId == user.Id).OrderBy(access => access.MenuItem.Order).ToList();
			var otherMenu = MenuItems.ToList().Where(m => !userAcess.Any(a => a.MenuId == m.Id));

			foreach (var menuItem in otherMenu)
			{
				userAcess.Add(new MenuItemAccessModel()
				{
					User = user,
					UserId = user.Id,
					MenuItem = menuItem,
					MenuId = menuItem.Id,
					Read = 0,
					Add = 0,
					Edit = 0,
					Delete = 0,
				});
			}

			return userAcess.OrderBy(a => a.MenuItem.Order).ToList();
		}

		public void Add(MenuItemAccessModel menuItemAccessModel)
		{
			if (AccessList.Any(access => access.MenuId == menuItemAccessModel.MenuId && access.UserId == menuItemAccessModel.UserId))
				throw new Exception("Подобный доступ уже открыт");

			AccessList.Add(menuItemAccessModel);
			SaveChanges();
		}

		public void Edit(MenuItemAccessModel menuItemAccessModel)
		{
			var accessToEdit = AccessList.Find(menuItemAccessModel.Id);
			
			if (accessToEdit.MenuItem.Default > 1)
				throw new Exception("Данное меню доступно по умолчанию. Доступ всем пользователям.");

			if (accessToEdit == null)
				throw new Exception("Данные для редактирования не найдены");

			Entry(accessToEdit).CurrentValues.SetValues(menuItemAccessModel);
			SaveChanges();

		}

		public void Delete(MenuItemAccessModel menuItemAccessModel)
		{
			var accessToDelete = AccessList.Find(menuItemAccessModel.Id);

			if (accessToDelete.MenuItem.Default > 1)
				throw new Exception("Данное меню доступно по умолчанию. Удаление невозможно");

			if (accessToDelete == null)
				throw new Exception("Данные для редактирования не найдены");

			AccessList.Remove(accessToDelete);
			SaveChanges();
		}

		#endregion

		// Улицы
		#region STREETS

		public List<StreetModel> GetStreetsList() => Streets.ToList();

		public void Add(StreetModel streetModel)
		{
			if (streetModel.Name == "")
				throw new Exception("Название улицы - пусто");

			if (Streets.Any(s => s.Name == streetModel.Name))
				throw new Exception("Улица с таким названием существует");

			Streets.Add(streetModel);
			SaveChanges();
		}

		public void Edit(StreetModel streetModel)
		{
			if (streetModel.Name == "")
				throw new Exception("Название улицы - пусто");

			if (Streets.Any(s => s.Name == streetModel.Name && s.Id != streetModel.Id))
				throw new Exception("Улица с таким названием существует");

			var streetToEdit = Streets.Find(streetModel.Id);

			Entry(streetToEdit).CurrentValues.SetValues(streetModel);
			SaveChanges();
		}

		public void Delete(StreetModel streetModel)
		{
			Streets.Remove(streetModel);
			SaveChanges();
		}

		#endregion

		// Ед. измерения
		#region UNITS

		public List<UnitModel> GetUnitsList() => Units.ToList();

		public void Add(UnitModel unitModel)
		{
			if (unitModel.Name == "")
				throw new Exception("Название - пусто");

			if (Streets.Any(s => s.Name == unitModel.Name))
				throw new Exception("Единица измерения с таким названием существует");

			Units.Add(unitModel);
			SaveChanges();
		}

		public void Edit(UnitModel unitModel)
		{
			if (unitModel.Name == "")
				throw new Exception("Название - пусто");

			if (Units.Any(s => s.Name == unitModel.Name && s.Id != unitModel.Id))
				throw new Exception("Единица измерения с таким названием существует");

			var unitToEdit = Units.Find(unitModel.Id);

			Entry(unitToEdit).CurrentValues.SetValues(unitModel);
			SaveChanges();
		}

		public void Delete(UnitModel unitModel)
		{
			Units.Remove(unitModel);
			SaveChanges();
		}

		#endregion

		// Категории
		#region CATEGORY

		public List<CategoryModel> GetCategoriesList() => Categories.ToList();

		public void Add(CategoryModel categoryModel)
		{
			if (categoryModel.Name == "")
				throw new Exception("Название - пусто");

			if (Categories.Any(c => c.Name == categoryModel.Name && c.Id != categoryModel.Id))
				throw new Exception("Категория с таким названием существует");

			Categories.Add(categoryModel);
			SaveChanges();
		}

		public void Edit(CategoryModel categoryModel)
		{
			if (categoryModel.Name == "")
				throw new Exception("Название - пусто");

			if (Categories.Any(c => c.Name == categoryModel.Name && c.Id != categoryModel.Id))
				throw new Exception("Категория с таким названием существует");

			var categoryToEdit = Categories.Find(categoryModel.Id);
			Entry(categoryToEdit).CurrentValues.SetValues(categoryModel);
			SaveChanges();
		}

		public void Delete(CategoryModel categoryModel)
		{
			Categories.Remove(categoryModel);
			SaveChanges();
		}

		#endregion

		// Банки
		#region BANKS

		public List<BankModel> GetBankList() => Banks.ToList();

		public void Add(BankModel bankModel)
		{
			if (bankModel.Name == "")
				throw new Exception("Название - пусто");

			if (Banks.Any(b => b.Name == bankModel.Name && b.Id != bankModel.Id))
				throw new Exception("Банк с таким названием существует");

			Banks.Add(bankModel);
			SaveChanges();
		}

		public void Edit(BankModel bankModel)
		{
			if (bankModel.Name == "")
				throw new Exception("Название - пусто");

			if (Banks.Any(b => b.Name == bankModel.Name && b.Id != bankModel.Id))
				throw new Exception("Банк с таким названием существует");

			var bankToEdit = Banks.Find(bankModel.Id);
			Entry(bankToEdit).CurrentValues.SetValues(bankModel);
			SaveChanges();
		}

		public void Delete(BankModel bankModel)
		{
			Banks.Remove(bankModel);
			SaveChanges();
		}

		#endregion

		// Продукция
		#region PRODUCTS

		public List<ProductModel> GetProductsList() => Products.ToList();

		public void Add(ProductModel productModel)
		{
			if (productModel.Name == "")
				throw new Exception("Название - пусто");

			if (Products.Any(p => p.Name == productModel.Name && p.Id != productModel.Id))
				throw new Exception("Категория с таким названием существует");

			Products.Add(productModel);
			SaveChanges();
		}

		public void Edit(ProductModel productModel)
		{
			if (productModel.Name == "")
				throw new Exception("Название - пусто");

			if (Products.Any(p => p.Name == productModel.Name && p.Id != productModel.Id))
				throw new Exception("Категория с таким названием существует");

			var productToEdit = Products.Find(productModel.Id);
			Entry(productToEdit).CurrentValues.SetValues(productModel);
			SaveChanges();
		}

		public void Delete(ProductModel productModel)
		{
			Products.Remove(productModel);
			SaveChanges();
		}

		#endregion

		// Рестораны
		#region RESTAURANT

		public List<RestaurantModel> GetRestaurantsList() => Restaurants.ToList();

		public void Add(RestaurantModel restaurantModel)
		{
			if (restaurantModel.Name == "")
				throw new Exception("Название - пусто");

			if (restaurantModel.Address == "")
				throw new Exception("Адрес - пусто");

			Restaurants.Add(restaurantModel);
			SaveChanges();
		}

		public void Edit(RestaurantModel restaurantModel)
		{
			if (restaurantModel.Name == "")
				throw new Exception("Название - пусто");

			if (restaurantModel.Address == "")
				throw new Exception("Адрес - пусто");

			var restaurantToEdit = Restaurants.Find(restaurantModel.Id);
			Entry(restaurantToEdit).CurrentValues.SetValues(restaurantModel);
			SaveChanges();
		}

		public void Delete(RestaurantModel restaurantModel)
		{
			Restaurants.Remove(restaurantModel);
			SaveChanges();
		}

		#endregion

		// Поставщики
		#region PROVIDER

		public List<ProviderModel> GetProvidersList() => Providers.ToList();

		public void Add(ProviderModel providerModel)
		{
			if (providerModel.Address == "")
				throw new Exception("Адрес - пусто");

			if (providerModel.Name == "")
				throw new Exception("Название - пусто");

			Providers.Add(providerModel);
			SaveChanges();
		}

		public void Edit(ProviderModel providerModel)
		{
			if (providerModel.Address == "")
				throw new Exception("Адрес - пусто");

			if (providerModel.Name == "")
				throw new Exception("Название - пусто");

			var restaurantToEdit = Providers.Find(providerModel.Id);
			Entry(restaurantToEdit).CurrentValues.SetValues(providerModel);
			SaveChanges();
		}

		public void Delete(ProviderModel providerModel)
		{
			Providers.Remove(providerModel);
			SaveChanges();
		}

		#endregion

		// Информация о продуктах на ЦС
		#region STOCKS

		public List<StockModel> GetStocksList() => Stocks.ToList();

		public void Add(StockModel stockModel)
		{
			//if (stockModel.Count <= 0)
			//	throw new Exception("Полонение меньше или равно 0");

			Stocks.Add(stockModel);
			SaveChanges();
		}

		public void Edit(StockModel stockModel)
		{
			var stockToEdit = Stocks.Find(stockModel.Id);
			Entry(stockToEdit).CurrentValues.SetValues(stockModel);

			SaveChanges();
		}

		public void Delete(StockModel stockModel)
		{
			Stocks.Remove(stockModel);
			SaveChanges();
		}

		#endregion

		// Поступления
		#region RECEIPT

		public List<ReceiptModel> GetReceiptsList() => Receipts.ToList();

		public void Add(ReceiptModel receiptModel)
		{
            if (receiptModel.Count <= 0)
                throw new Exception("Полонение меньше или равно 0");

            Receipts.Add(receiptModel);

			

			if (!Stocks.Any(s => s.ProductId == receiptModel.ProductId && s.ProviderId == receiptModel.ProviderId))
			{
				StockModel stockModel = new StockModel()
				{
					ProductId = receiptModel.ProductId,
					Product = receiptModel.Product,

					ProviderId = receiptModel.ProviderId,
					Provider = receiptModel.Provider,

					Count = receiptModel.Count,
					Price = 0,
				};

				Stocks.Add(stockModel);
			}
			else
			{
				var stockToEdit = Stocks.First(s => s.ProductId == receiptModel.ProductId && s.ProviderId == receiptModel.ProviderId);
				stockToEdit.Count += receiptModel.Count;
			}
		
			SaveChanges();
		}

		public void Edit(ReceiptModel receiptModel)
		{
			var receiptToEdit = Receipts.Find(receiptModel.Id);
			Entry(receiptToEdit).CurrentValues.SetValues(receiptModel);
			SaveChanges();
		}

		public void Delete(ReceiptModel receiptModel)
		{
			Receipts.Remove(receiptModel);
			SaveChanges();
		}

		#endregion

		// Данные ассортимента ресторанов
		#region ASSORTMENT

		public List<AssortmentModel> GetAssortmentsList() => Assortments.ToList();

		public void Add(AssortmentModel assortmentModel)
		{
			if (Assortments.Any(a => a.Name == assortmentModel.Name && a.RestaurantId == assortmentModel.RestaurantId))
				throw new Exception("Блюдо с таким именем уже существует");

			Assortments.Add(assortmentModel);
			SaveChanges();
		}

		public void Edit(AssortmentModel assortmentModel)
		{
			if (Assortments.Any(a => a.Name == assortmentModel.Name && a.RestaurantId == assortmentModel.RestaurantId && a.Id != assortmentModel.Id))
				throw new Exception("Блюдо с таким именем уже существует");

			var assortmentToEdit = Assortments.Find(assortmentModel.Id);
			Entry(assortmentToEdit).CurrentValues.SetValues(assortmentModel);
			SaveChanges();
		}

		public void Delete(AssortmentModel assortmentModel)
		{
			Assortments.Remove(assortmentModel);
			SaveChanges();
		}

		#endregion

		// Отчеты ресторанов
		#region REPOST

		public List<ReportModel> GetRepostsList() => Reports.ToList();

		public void Add(ReportModel reportModel)
		{
			if (reportModel.Count <= 0)
				throw new Exception("Количество меньше или равно 0");

			Reports.Add(reportModel);
			SaveChanges();
		}

		public void Edit(ReportModel reportModel)
		{
			if (reportModel.Count <= 0)
				throw new Exception("Количество меньше или равно 0");

			var reportToEdit = Reports.Find(reportModel.Id);
			Entry(reportToEdit).CurrentValues.SetValues(reportModel);
			SaveChanges();
		}

		public void Delete(ReportModel reportModel)
		{
			Reports.Remove(reportModel);
			SaveChanges();
		}

		#endregion

		// Заявки
		#region APPLICATION

		public List<ApplicationModel> GetApplicationsList() => Applications.ToList();

		public void Add(ApplicationModel applicationModel)
		{
			if (!Stocks.Any(s => s.ProductId == applicationModel.ProductId))
				throw new Exception("Запрашиваемой продукции нет на складе");

			var stockList = Stocks.Where(s => s.ProductId == applicationModel.ProductId);
			int sumCount = stockList.Sum(s => s.Count);

			if (sumCount < applicationModel.Count)
				throw new Exception("Запрашиваемого количества продукции нет на складе");

			int request = applicationModel.Count;

			foreach (StockModel item in stockList)
			{
				if (request == 0)
					break;

				int recd = (item.Count >= request) ? request : request - item.Count;

				item.Count -= recd;
				request -= recd;

				Edit(item);
			}
			Applications.Add(applicationModel);
			SaveChanges();
		}

		public void Edit(ApplicationModel applicationModel)
		{
			var reportToEdit = Applications.Find(applicationModel.Id);
			Entry(reportToEdit).CurrentValues.SetValues(applicationModel);

			SaveChanges();
		}

		public void Delete(ApplicationModel applicationModel)
		{
			Applications.Remove(applicationModel);
			SaveChanges();
		}

		#endregion
	}
}
