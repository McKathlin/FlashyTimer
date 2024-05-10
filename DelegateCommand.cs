using System.Windows.Input;

namespace MVVM
{
    class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
