using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TCPChatterAllPlatforms.DataSchemas;
using TCPChatterAllPlatforms.Views;

namespace TCPChatterAllPlatforms.ViewModels
{
    public class SettingsViewModel
    {
        internal ObservableCollection<IpRegion> _oRecordedThisTime = new();

        internal ObservableCollection<IpRegion> RecordedThisTime { get; set; } = new();
        public SettingsViewModel()
        {
            RecordedThisTime.CollectionChanged += RecordedThisTime_CollectionChanged;
            IPBlackCollection.CollectionChanged += RecordedThisTime_CollectionChanged;
        }

        private void RecordedThisTime_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SettingsModel.SaveToDisk();
        }
        internal ObservableCollection<IpRegion> IPBlackCollection { get; set; } = new();
        public IList<string> IPBlackList { get; set; } = new List<string>();
    }

    internal static class SettingsModel
    {
        static SettingsModel()
        {
            LoadFromDisk();
        }
        public static IList<string> _ipBlackList;
        private static string _appD;

        public static SettingsViewModel Instance { get; set; } = new SettingsViewModel();
        public static void LoadFromDisk()
        {
            _appD = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _appD = Path.Combine(_appD, ".tcpchat");
            Directory.CreateDirectory(_appD);
            _appD = Path.Combine(_appD, ".config");
            if(!File.Exists(_appD)) 
            { 
                File.Create(_appD).Close(); 
                Instance = new();
                _ipBlackList = new List<string>();
                //SettingView.View.DataContext = Instance;
                SaveToDisk();
            }
            else
            {
                var content = File.ReadAllText(_appD);
                try
                {
                    var ins = JsonSerializer.Deserialize<SettingsViewModel>(content);
                    Instance = ins;
                    _ipBlackList = ins.IPBlackList;
                    //SettingView.View.DataContext = Instance;
                }
                catch (Exception ex)
                {
                    File.Create(_appD).Close();
                    Instance = new();
                    _ipBlackList = new List<string>();
                    //SettingView.View.DataContext = Instance;
                    SaveToDisk();
                }
            }
        }
        public static void SaveToDisk()
        {
            File.WriteAllText(_appD,JsonSerializer.Serialize(Instance));
        }
    }
}
