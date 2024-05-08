using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlashyTimer
{
    public class Timer : INotifyPropertyChanged
    {
        #region Constants

        public const int DEFAULT_STARTING_MINUTES = 10;
        public const int DEFAULT_WARNING_MINUTES = 1;

        #endregion
        #region Private Members

        private TimeSpan _startingTime;
        private TimeSpan _warningTime;
        private TimeSpan _timeRemaining;

        #endregion
        #region Properties

        public TimeSpan StartingTime
        {
            get
            {
                return this._startingTime;
            }
            set
            {
                this._startingTime = value;
                OnPropertyChanged(nameof(StartingTime));
            }
        }

        public TimeSpan WarningTime
        {
            get
            {
                return this._warningTime;
            }
            set
            {
                this._warningTime = value;
                OnPropertyChanged(nameof(WarningTime));
            }
        }

        public TimeSpan TimeRemaining {
            get
            {
                return _timeRemaining;
            }
            set
            {
                _timeRemaining = value;
                OnPropertyChanged(nameof(TimeRemaining));
            }
        }

        #endregion
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
