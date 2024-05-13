using System.Windows.Input;

namespace FlashyTimer
{
    internal class TimeSetCommand : ICommand
    {
        private readonly Action<TimeSettings> _action;
        private readonly TimeSettings _timeSettings;

        public event EventHandler? CanExecuteChanged;

        public TimeSetCommand(Action<TimeSettings> action, TimeSettings settings)
        {
            _action = action;
            _timeSettings = settings;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action(_timeSettings);
        }
    }
}
