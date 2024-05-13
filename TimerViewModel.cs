using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using MVVM;

namespace FlashyTimer
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

        private ICommand _startFortyMinutesCommand;
        private ICommand _stopCommand;
        private FlashyTimer.Timer _timer;

        #region init

        public TimerViewModel()
        {
            _startFortyMinutesCommand = new DelegateCommand(StartFortyMinutes);
            _stopCommand = new DelegateCommand(Stop);

            _timer = new FlashyTimer.Timer();
            _timer.PropertyChanged += OnTimerPropertyChanged;
        }

        #endregion
        #region properties

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

        public ICommand StartFortyMinutesCommand
        {
            get { return _startFortyMinutesCommand; }
        }

        public ICommand StopCommand
        {
            get { return _stopCommand; }
        }

        #endregion
        #region event handling and updates

        private void OnTimerPropertyChanged(object source, PropertyChangedEventArgs e)
        {
            if ("Status" == e.PropertyName)
            {
                Text = GetUpdatedText();
                Background = GetUpdatedBackground();
            }
            else if ("TimeRemaining" == e.PropertyName)
            {
                Text = GetUpdatedText();
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
            switch(_timer.Status)
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

        private void StartFortyMinutes()
        {
            _timer.Start(TimeSpan.FromMinutes(40));
        }

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
