using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashyTimer
{
    public class TimeSettings
    {
        private readonly TimeSpan _startingTime;
        private readonly TimeSpan _warningTime;
        private readonly TimeSpan _criticalTime;

        public TimeSettings(TimeSpan start, TimeSpan warn, TimeSpan crit)
        {
            _startingTime = start;
            _warningTime = warn;
            _criticalTime = crit;
        }

        public TimeSpan StartingTime
        { 
            get { return _startingTime; }
        }

        public TimeSpan WarningTime
        {
            get { return _warningTime; }
        }

        public TimeSpan CriticalTime
        {
            get { return _criticalTime; }
        }
    }
}
