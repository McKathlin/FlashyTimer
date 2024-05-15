using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            Add(MakeStartOption(5, 1, 0.25));
            Add(MakeStartOption(10, 1, 0.25));
            Add(MakeStartOption(30, 10, 1));
            Add(MakeStartOption(45, 10, 1));
        }

        public bool LoadFromFile(string path)
        {
            this.Clear();
            try
            {
                _latestDataPath = path;
                string json;
                using (StreamReader reader = new StreamReader(path))
                {
                    json = reader.ReadToEnd();
                }

                TimeSettings[]? settingsArr = JsonSerializer.Deserialize<TimeSettings[]>(json);
                if (null == settingsArr)
                {
                    return false;
                }

                foreach (TimeSettings element in settingsArr)
                {
                    this.Add(new TimeSettingsViewModel(_timer, element));
                }
                return this.Count > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void SaveToFile(string path)
        {
            using (StreamWriter jsonWriter = new StreamWriter(path))
            {
                jsonWriter.WriteLine("[");
                for (int i = 0; i < this.Count; i++)
                {
                    jsonWriter.Write("\t");
                    TimeSettings settings = this[i].Settings;
                    string settingsJson = JsonSerializer.Serialize(settings);
                    jsonWriter.Write(settingsJson);
                    if (i < this.Count - 1)
                    {
                        jsonWriter.Write(",");
                    } 
                    jsonWriter.WriteLine();
                }
                jsonWriter.WriteLine("]");
            }
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
