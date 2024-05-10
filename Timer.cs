using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Timers;
using MVVM;

namespace FlashyTimer
{
    public enum TimerStatus
    {
        Stopped,
        Normal,
        Warning,
        Critical
    }

    public class Timer : ObservableObject
    {
        public const int DEFAULT_STARTING_MINUTES = 10;
        public const int DEFAULT_WARNING_MINUTES = 1;
        public const int MILLISECONDS_PER_SECOND = 1000;

        public static readonly TimeSpan ONE_SECOND = TimeSpan.FromSeconds(1);

        private System.Timers.Timer _timer;
        private TimeSpan _startingTime;
        private TimeSpan _warningTime;
        private TimeSpan _criticalTime;
        private TimeSpan _timeRemaining;
        private TimerStatus _status;

        public Timer()
        {
            _startingTime = TimeSpan.FromMinutes(DEFAULT_STARTING_MINUTES);
            _timeRemaining = TimeSpan.FromMinutes(DEFAULT_STARTING_MINUTES);
            _warningTime = TimeSpan.FromMinutes(DEFAULT_WARNING_MINUTES);

            _timer = new System.Timers.Timer(MILLISECONDS_PER_SECOND);
            _timer.Elapsed += OnSecondElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public TimeSpan StartingTime
        {
            get
            {
                return this._startingTime;
            }
            set
            {
                this._startingTime = value;
                UpdateStatus();
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
                UpdateStatus();
                OnPropertyChanged(nameof(WarningTime));
            }
        }

        public TimeSpan CriticalTime
        {
            get
            {
                return this._criticalTime;
            }
            set
            {
                this._criticalTime = value;
                UpdateStatus();
                OnPropertyChanged(nameof(CriticalTime));
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
                UpdateStatus();
                OnPropertyChanged(nameof(TimeRemaining));
            }
        }

        public TimerStatus Status
        {
            get {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
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
            _timer.Stop();
            TimeRemaining = TimeSpan.Zero;
        }

        public void Pause()
        {
            _timer.Stop();
            UpdateStatus();
        }

        public void Resume()
        {
            _timer.Start();
            UpdateStatus();
        }

        public void AddTime(TimeSpan timeToAdd)
        {
            TimeRemaining += timeToAdd;
        }

        private void UpdateStatus()
        {
            if (_timer.Enabled)
            {
                if (TimeRemaining > WarningTime)
                {
                    Status = TimerStatus.Normal;
                }
                else if (TimeRemaining > CriticalTime)
                {
                    Status = TimerStatus.Warning;
                }
                else
                {
                    Status = TimerStatus.Critical;
                }
            }
            else
            {
                Status = TimerStatus.Stopped;
            }
        }

        private void OnSecondElapsed(Object source, ElapsedEventArgs e)
        {
            TimeRemaining -= ONE_SECOND;
        }
    } // class Timer
} // namespace FlashyTimer
