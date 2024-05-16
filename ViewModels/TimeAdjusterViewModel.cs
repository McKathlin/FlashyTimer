using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlashyTimer.Models;
using MVVM;

namespace FlashyTimer.ViewModels
{
    public class TimeAdjusterViewModel : ObservableObject
    {
        private readonly ICommand _addCommand;
        private readonly ICommand _subtractCommand;

        private readonly CountdownTimer _timer;
        private TimeSpan _timeAmount;
        private string _shorthand;

        public TimeAdjusterViewModel(CountdownTimer timer, TimeSpan span)
        {
            _timer = timer;
            _timeAmount = span;
            _shorthand = GetShorthand(_timeAmount);

            _addCommand = new DelegateCommand(Add);
            _subtractCommand = new DelegateCommand(Subtract);
        }

        public TimeSpan TimeAmount
        {
            get 
            {
                return _timeAmount;
            }
            set
            {
                _timeAmount = value;
                _shorthand = GetShorthand(_timeAmount);
                OnPropertyChanged(nameof(TimeAmount));
                OnPropertyChanged(nameof(AddLabel));
                OnPropertyChanged(nameof(SubtractLabel));
            }
        }

        public string AddLabel
        {
            get
            {
                return "<< " + _shorthand;
            }
        }

        public string SubtractLabel
        {
            get
            {
                return _shorthand + " >>";
            }
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
        }

        public ICommand SubtractCommand
        {
            get { return _subtractCommand; }
        }

        private void Add()
        {
            _timer.AddTime(_timeAmount);
        }

        private void Subtract()
        {
            _timer.SubtractTime(_timeAmount);
        }

        private static string GetShorthand(TimeSpan span)
        {
            string str = "";
            int mins = (int)Math.Floor(span.TotalMinutes);
            if (mins > 0)
            {
                str = mins.ToString() + "m";
            }

            if (span.Seconds > 0)
            {
                if (str.Length > 0)
                {
                    str += " ";
                }
                str += span.Seconds.ToString() + "s";
            }
            return str;
        }
    }
}
