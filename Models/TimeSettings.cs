using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashyTimer.Models
{
    public struct TimeSettings
    {
        public TimeSpan StartingTime { get; set; }
        public TimeSpan WarningTime { get; set; }
        public TimeSpan CriticalTime { get; set; }
    }
}
