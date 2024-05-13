using System.Windows.Input;

namespace FlashyTimer
{
    internal class TimeSetCommand : ICommand
    {
        private readonly Action<int> _action;
        private readonly int _minuteCount;

        public event EventHandler? CanExecuteChanged;

        public TimeSetCommand(Action<int> action, int numMinutes)
        {
            _action = action;
            _minuteCount = numMinutes;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action(_minuteCount);
        }
    }
}
