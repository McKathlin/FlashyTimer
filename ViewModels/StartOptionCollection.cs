using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FlashyTimer.Models;

namespace FlashyTimer.ViewModels
{
    public class StartOptionCollection : ObservableCollection<TimeSettingsViewModel>
    {
        CountdownTimer _timer;
        string _latestDataPath;

        public StartOptionCollection(CountdownTimer timer, string dataFilePath) {
            _timer = timer;
            _latestDataPath = dataFilePath;
            bool loaded = LoadFromFile(dataFilePath);
            if (!loaded)
            {
                LoadDefaults();
                SaveToFile(_latestDataPath);
            }
        }

        public void LoadDefaults()
        {
            Add(MakeStartOption(1, 0.5, 0.1));
            Add(MakeStartOption(10, 1, 0.333));
            Add(MakeStartOption(40, 10, 1));
            Add(MakeStartOption(45, 10, 1));
        }

        public bool LoadFromFile(string path)
        {
            _latestDataPath = path;
            // TODO: Load JSON from file; return true if successful
            return false;
        }

        public void SaveToFile(string path)
        {
            // TOOD: Implement saving JSON to file
        }

        private TimeSettingsViewModel MakeStartOption(double startMins, double warnMins, double critMins)
        {
            return new TimeSettingsViewModel(
                _timer,
                TimeSpan.FromMinutes(startMins),
                TimeSpan.FromMinutes(warnMins),
                TimeSpan.FromMinutes(critMins)
            );
        }
    }
}
