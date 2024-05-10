using System;
using System.Collections.Generic;
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

        private ICommand _startFortyMinutesCommand = null;
        private ICommand _stopCommand = null;

        private FlashyTimer.Timer _timer = new FlashyTimer.Timer();

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
            get
            {
                if (null == _startFortyMinutesCommand)
                {
                    _startFortyMinutesCommand = new DelegateCommand(StartFortyMinutes);
                }
                return _startFortyMinutesCommand;
            }
        }

        public ICommand StopCommand
        {
            get
            {
                if (null == _stopCommand)
                {
                    _stopCommand = new DelegateCommand(Stop);
                }
                return _stopCommand;
            }
        }

        private void StartFortyMinutes()
        {
            _timer.Start(TimeSpan.FromMinutes(40));
            // TODO: Automate text and background values based on Timer state
            Text = "40:00";
            Background = NORMAL_BACKGROUND;
        }

        private void Stop()
        {
            _timer.Stop();
            // TODO: Automate based on Timer state
            Text = TIME_STOPPED_TEXT;
            Background = DISABLED_BACKGROUND;
        }

    } // end class TimerViewModel
}
