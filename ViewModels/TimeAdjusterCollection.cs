using FlashyTimer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashyTimer.ViewModels
{
    public class TimeAdjusterCollection : ObservableCollection<TimeAdjusterViewModel>
    {
        CountdownTimer _timer;
        
        public TimeAdjusterCollection(CountdownTimer timer)
        {
            _timer = timer;
            LoadElements();
        }

        public void LoadElements()
        {
            Clear();
            Add(MakeAdjuster(TimeSpan.FromMinutes(5)));
            Add(MakeAdjuster(TimeSpan.FromMinutes(1)));
            Add(MakeAdjuster(TimeSpan.FromSeconds(10)));
        }

        public TimeAdjusterViewModel MakeAdjuster(TimeSpan span)
        {
            return new TimeAdjusterViewModel(_timer, span);
        }
    }
}
