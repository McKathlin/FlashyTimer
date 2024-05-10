using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MVVM;

namespace FlashyTimer
{
    public class Timer : ObservableObject
    {
        public const int DEFAULT_STARTING_MINUTES = 10;
        public const int DEFAULT_WARNING_MINUTES = 1;
        public const int MILLISECONDS_PER_SECOND = 1000;

        private System.Timers.Timer _timer =
            new System.Timers.Timer(MILLISECONDS_PER_SECOND);
        private TimeSpan _startingTime;
        private TimeSpan _warningTime;
        private TimeSpan _timeRemaining;

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

        public void Start(TimeSpan startTime)
        {
            StartingTime = startTime;
            TimeRemaining = startTime;
            _timer.Start();
        }

        public void Stop()
        {
            TimeRemaining = TimeSpan.Zero;
            _timer.Stop();
        }

        public void Pause()
        {
            _timer.Stop();
        }

        public void Resume()
        {
            _timer.Start();
        }

        public void AddTime(TimeSpan timeToAdd)
        {
            TimeRemaining += timeToAdd;
        }
    } // class Timer
} // namespace FlashyTimer
