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

        public static readonly Brush REGULAR_BRUSH =
            new SolidColorBrush(Colors.White);
        public static readonly Brush WARNING_BRUSH =
            new SolidColorBrush(Colors.Yellow);
        public static readonly Brush CRITICAL_BRUSH =
            new SolidColorBrush(Colors.Red);
        public static readonly Brush DISABLED_BRUSH =
            new SolidColorBrush(Colors.LightGray);

        private string _text = TIME_STOPPED_TEXT;
        private Brush _background = DISABLED_BRUSH;

        private ICommand _startFortyMinutesCommand = null;

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
                    return new DelegateCommand(StartFortyMinutes);
                }
                return _startFortyMinutesCommand;
            }
        }

        private void StartFortyMinutes()
        {
            _timer.Start(TimeSpan.FromMinutes(40));
        }


    } // end class TimerViewModel
}
