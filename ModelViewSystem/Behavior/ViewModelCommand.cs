using System;
using System.Windows.Input;

namespace ModelViewSystem
{
	/// <summary>
	/// Реализация интерфейса ICommand для обработки команд, связанных с ViewModel.
	/// </summary>
	public class ViewModelCommand : ICommand
	{
		private readonly Action<object> _executeAction;

		public ViewModelCommand(Action<object> executeAction)
		{
			_executeAction = executeAction;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter) => _executeAction != null;
		public void Execute(object parameter) => _executeAction(parameter);
	}
}
