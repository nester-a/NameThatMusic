using System;
using System.Windows.Input;

namespace TabControlTestMVVM.ViewModel
{
    class DelegateCommand : ICommand
    {
        private Func<object, bool> canExecute;
        private Action<object> execute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public DelegateCommand(Action<object> _execute, Func<object, bool> _canExecute)
        {
            execute = _execute;
            canExecute = _canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
