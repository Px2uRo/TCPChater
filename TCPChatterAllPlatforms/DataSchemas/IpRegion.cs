using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TCPChatterAllPlatforms.ViewModels;

namespace TCPChatterAllPlatforms.DataSchemas
{
    internal class IpRegion
    {
        public ICommand AddToBlacklist => AddToBlacklistCommand.Instance;
        public ICommand RemoveFromBlacklist => RemoveFromBlacklistCommand.Instance;

        private string _ipbase;
		public string IPBase
		=> _ipbase;

		public IpRegion(string ipbase) 
		{ 
			_ipbase = ipbase;
		}
    }

    internal class AddToBlacklistCommand : ICommand
    {
        public static AddToBlacklistCommand Instance = new();

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var ipRe = parameter as IpRegion;
            SettingsModel.Instance.IPBlackList.Add(ipRe.IPBase);
            SettingsModel.Instance.IPBlackCollection.Add(ipRe);
            SettingsModel.Instance.RecordedThisTime.Remove(ipRe);
        }
    }
    internal class RemoveFromBlacklistCommand : ICommand
    {
        public static RemoveFromBlacklistCommand Instance = new();

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var ipRe = parameter as IpRegion;
            SettingsModel.Instance.IPBlackList.Remove(ipRe.IPBase);
            SettingsModel.Instance.IPBlackCollection.Remove(ipRe);
            SettingsModel.Instance.RecordedThisTime.Add(ipRe);
        }
    }
}
