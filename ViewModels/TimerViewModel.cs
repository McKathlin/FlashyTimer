using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using FlashyTimer.Models;
using MVVM;

namespace FlashyTimer.ViewModels
{
    public class TimerViewModel : ObservableObject
    {
        public const string TIME_STOPPED_TEXT = "--:--";

        public static readonly Brush NORMAL_BACKGROUND =
            new SolidColorBrush(Colors.White);
        public static readonly Brush WARNING_BACKGROUND =
            new SolidColorBrush(Colors.Yellow);
        public static readonly Brush CRITICAL_BACKGROUND =
            new SolidColorBrush(Colors.Red);
        public static readonly Brush DISABLED_BACKGROUND =
            new SolidColorBrush(Colors.LightGray);

        private string _text = TIME_STOPPED_TEXT;
        private Brush _background = DISABLED_BACKGROUND;
        private ObservableCollection<TimeSettingsViewModel> _startOptions =
            new ObservableCollection<TimeSettingsViewModel>();

        private ICommand _stopCommand;
        private ICommand _pauseResumeCommand;
        private CountdownTimer _timer;

        #region init

        public TimerViewModel()
        {
            // TODO: Use an ObservableCollection of TimeSettingsViewModels.
            // https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-create-and-bind-to-an-observablecollection?view=netframeworkdesktop-4.8

            _stopCommand = new DelegateCommand(Stop);
            _pauseResumeCommand = new DelegateCommand(PauseOrResume);

            _timer = new CountdownTimer();
            _timer.PropertyChanged += OnTimerPropertyChanged;

            PopulateStartOptions();
        }

        private void PopulateStartOptions()
        {
            _startOptions.Add(MakeStartOption(1, 0.5, 0.1));
            _startOptions.Add(MakeStartOption(10, 1, 0.333));
            _startOptions.Add(MakeStartOption(40, 10, 1));
            _startOptions.Add(MakeStartOption(45, 10, 1));
        }

        private TimeSettingsViewModel MakeStartOption(double startMins, double warnMins, double critMins)
        {
            return new TimeSettingsViewModel(
                _timer,
                TimeSpan.FromMinutes(startMins),
                TimeSpan.FromMinutes(warnMins),
                TimeSpan.FromMinutes(critMins)
            );
        }

        #endregion
        #region properties

        public ObservableCollection<TimeSettingsViewModel> StartOptions
        {
            get
            {
                return _startOptions;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public Brush Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }
        public ICommand PauseResumeCommand
        {
            get { return _pauseResumeCommand; }
        }

        public ICommand StopCommand
        {
            get { return _stopCommand; }
        }

        #endregion
        #region event handling and updates

        private void OnTimerPropertyChanged(object? source, PropertyChangedEventArgs e)
        {
            if ("Status" == e.PropertyName || "TimeRemaining" == e.PropertyName)
            {
                Text = GetUpdatedText();
                Background = GetUpdatedBackground();
            }
        }

        private string GetUpdatedText()
        {
            if (_timer.IsStopped() &&
                _timer.TimeRemaining == TimeSpan.Zero)
            {
                return "--:--";
            }
            else
            {
                return _timer.TimeRemaining.ToString(@"mm\:ss");
            }
        }

        private Brush GetUpdatedBackground()
        {
            switch (_timer.Status)
            {
                case TimerStatus.Normal:
                    return NORMAL_BACKGROUND;
                case TimerStatus.Warning:
                    return WARNING_BACKGROUND;
                case TimerStatus.Critical:
                    return 0 == _timer.TimeRemaining.Seconds % 2 ?
                        CRITICAL_BACKGROUND : WARNING_BACKGROUND;
                case TimerStatus.Stopped:
                    return DISABLED_BACKGROUND;
                default:
                    throw new NotImplementedException(
                        "Unrecognized status: " + _timer.Status);
            }
        }

        #endregion
        #region methods used by commands

        private void Stop()
        {
            _timer.Stop();
        }

        private void PauseOrResume()
        {
            if (_timer.IsRunning())
            {
                _timer.Pause();
            }
            else
            {
                _timer.Resume();
            }
        }

        #endregion

    } // end class TimerViewModel
}
