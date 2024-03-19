using DatabaseManagement;
using System;
using System.Windows.Controls;

namespace ModelViewSystem
{
	/// <summary>
	/// Класс, предоставляющий методы для управления окнами приложения.
	/// </summary>
	public class WindowService
	{
		public static Action<Page> OnLoadPage { get; set; }
		public static Action<UserModel> OnLogin { get; set; }
		public static Action OnLogOut { get; set; }
		public static Action<Type, ViewModelBase> OnOpenWindow { get; set; }

		public static void LoadPage(Page page) => OnLoadPage?.Invoke(page);
		public static void UserLogin(UserModel userModel) => OnLogin?.Invoke(userModel);
		public static void UserLogOut() => OnLogOut?.Invoke();
		public static void OpenWindow(Type windowType, ViewModelBase viewModel) => OnOpenWindow?.Invoke(windowType, viewModel);

	}
}
