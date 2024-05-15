using System;
using System.Windows.Input;
using MVVM;
using FlashyTimer.Models;

namespace FlashyTimer.ViewModels
{
    public class TimeSettingsViewModel : ObservableObject
    {
        private CountdownTimer _flashyTimer;
        private TimeSettings _timeSettings;
        private ICommand _startCommand;

        public TimeSettingsViewModel(CountdownTimer fTimer,
            TimeSpan tStart, TimeSpan tWarn, TimeSpan tCrit)
        {
            _flashyTimer = fTimer;
            _timeSettings = new TimeSettings
            {
                StartingTime = tStart,
                WarningTime = tWarn,
                CriticalTime = tCrit
            };
            _startCommand = new DelegateCommand(Start);
        }

        public TimeSettingsViewModel(CountdownTimer fTimer, TimeSettings settings)
        {
            _flashyTimer = fTimer;
            _timeSettings = settings;
            _startCommand = new DelegateCommand(Start);
        }

        public string Name
        {
            get
            {
                return _timeSettings.StartingTime.Minutes.ToString() + "m";
            }
        }

        public TimeSettings Settings
        {
            get
            {
                return _timeSettings;
            }
            set
            {
                _timeSettings = value;
                OnPropertyChanged(nameof(Settings));
                OnPropertyChanged(nameof(Name));
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
