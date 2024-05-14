using System;
using System.Windows.Input;
using MVVM;

namespace FlashyTimer
{
    public class TimeSettingsViewModel : ObservableObject
    {
        private FlashyTimer.Timer _flashyTimer;
        private TimeSettings _timeSettings;
        private ICommand _startCommand;

        public TimeSettingsViewModel(FlashyTimer.Timer fTimer,
            TimeSpan tStart, TimeSpan tWarn, TimeSpan tCrit) 
        {
            _flashyTimer = fTimer;
            _timeSettings = new TimeSettings(tStart, tWarn, tCrit);
            _startCommand = new DelegateCommand(Start);
        }

        public string Name
        {
            get
            {
                return StartingTime.Minutes.ToString() + "m";
            }
        }

        public TimeSpan StartingTime
        {
            get
            {
                return _timeSettings.StartingTime;
            }
            set
            {
                TimeSpan start = value;
                TimeSpan warn = _timeSettings.WarningTime;
                TimeSpan crit = _timeSettings.CriticalTime;
                _timeSettings = new TimeSettings(start, warn, crit);
                OnPropertyChanged(nameof(StartingTime));
                OnPropertyChanged(nameof(Name));
            }
        }

        public TimeSpan WarningTime
        {
            get
            {
                return _timeSettings.WarningTime;
            }
            set
            {
                TimeSpan start = _timeSettings.StartingTime;
                TimeSpan warn = value;
                TimeSpan crit = _timeSettings.CriticalTime;
                _timeSettings = new TimeSettings(start, warn, crit);
                OnPropertyChanged(nameof(WarningTime));
            }
        }

        public TimeSpan CriticalTime
        {
            get
            {
                return _timeSettings.CriticalTime;
            }
            set
            {
                TimeSpan start = _timeSettings.StartingTime;
                TimeSpan warn = _timeSettings.WarningTime;
                TimeSpan crit = value;
                _timeSettings = new TimeSettings(start, warn, crit);
                OnPropertyChanged(nameof(CriticalTime));
            }
        }

        public ICommand StartCommand
        {
            get
            {
                return _startCommand;
            }
        }

        private void Start()
        {
            _flashyTimer.StartingTime = _timeSettings.StartingTime;
            _flashyTimer.WarningTime = _timeSettings.WarningTime;
            _flashyTimer.CriticalTime = _timeSettings.CriticalTime;
            _flashyTimer.Start();
        }
    }
}
