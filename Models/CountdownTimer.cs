using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Timers;
using MVVM;
using System.Net.NetworkInformation;

namespace FlashyTimer.Models
{
    public enum TimerStatus
    {
        Stopped,
        Normal,
        Warning,
        Critical
    }

    public class CountdownTimer : ObservableObject
    {
        public const int MILLISECONDS_PER_SECOND = 1000;

        public static readonly TimeSpan ONE_SECOND = TimeSpan.FromSeconds(1);

        public static readonly TimeSpan DEFAULT_STARTING_TIME =
            TimeSpan.FromMinutes(10);
        public static readonly TimeSpan DEFAULT_WARNING_TIME =
            TimeSpan.FromMinutes(1);
        public static readonly TimeSpan DEFAULT_CRITICAL_TIME =
            TimeSpan.FromSeconds(30);

        private System.Timers.Timer _timer;
        private TimeSpan _startingTime;
        private TimeSpan _warningTime;
        private TimeSpan _criticalTime;
        private TimeSpan _timeRemaining;
        private TimerStatus _status;

        public CountdownTimer()
        {
            _startingTime = DEFAULT_STARTING_TIME;
            _warningTime = DEFAULT_WARNING_TIME;
            _criticalTime = DEFAULT_CRITICAL_TIME;
            _timeRemaining = TimeSpan.Zero;

            _timer = new System.Timers.Timer(MILLISECONDS_PER_SECOND);
            _timer.Elapsed += OnSecondElapsed;
            _timer.AutoReset = true;
        }

        public TimeSpan StartingTime
        {
            get
            {
                return _startingTime;
            }
            set
            {
                _startingTime = value;
                UpdateStatus();
                OnPropertyChanged(nameof(StartingTime));
            }
        }

        public TimeSpan WarningTime
        {
            get
            {
                return _warningTime;
            }
            set
            {
                _warningTime = value;
                UpdateStatus();
                OnPropertyChanged(nameof(WarningTime));
            }
        }

        public TimeSpan CriticalTime
        {
            get
            {
                return _criticalTime;
            }
            set
            {
                _criticalTime = value;
                UpdateStatus();
                OnPropertyChanged(nameof(CriticalTime));
            }
        }

        public TimeSpan TimeRemaining
        {
            get
            {
                return _timeRemaining;
            }
            set
            {
                if (value != _timeRemaining)
                {
                    _timeRemaining = value;
                    UpdateStatus();
                    OnPropertyChanged(nameof(TimeRemaining));
                }
            }
        }

        public TimerStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public bool IsRunning()
        {
            return _timer.Enabled;
        }

        public bool IsStopped()
        {
            return !IsRunning();
        }

        public void Start()
        {
            TimeRemaining = StartingTime;
            _timer.Start();
            UpdateStatus();
        }

        public void Stop()
        {
            _timer.Stop();
            TimeRemaining = TimeSpan.Zero;
            UpdateStatus();
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
            UpdateStatus();
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

        private void OnSecondElapsed(object? source, ElapsedEventArgs e)
        {
            TimeRemaining -= ONE_SECOND;
        }
    } // class Timer
} // namespace FlashyTimer
