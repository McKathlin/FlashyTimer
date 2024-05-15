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
        const string DEFAULT_START_OPTIONS_PATH = "StartOptions.json";

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

        private ICommand _stopCommand;
        private ICommand _pauseResumeCommand;
        private CountdownTimer _timer;
        private StartOptionCollection _startOptions;

        #region init

        public TimerViewModel()
        {
            // TODO: Use an ObservableCollection of TimeSettingsViewModels.
            // https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-create-and-bind-to-an-observablecollection?view=netframeworkdesktop-4.8

            _stopCommand = new DelegateCommand(Stop);
            _pauseResumeCommand = new DelegateCommand(PauseOrResume);

            _timer = new CountdownTimer();
            _timer.PropertyChanged += OnTimerPropertyChanged;

            _startOptions = new StartOptionCollection(
                _timer, DEFAULT_START_OPTIONS_PATH);
        }

        #endregion
        #region properties

        public StartOptionCollection StartOptions
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
