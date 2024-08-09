using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPChatterAllPlatforms.DataSchemas;
using TCPChatterAllPlatforms.ViewModels;
using YamlDotNet.Core.Tokens;

namespace TCPChatterAllPlatforms.Views;

public partial class SettingView : UserControl

{
    public static SettingView View = new SettingView();
    public SettingView()
    {
        InitializeComponent();
        this.DataContext = SettingsModel.Instance;
    }

    internal void Scan() => Task.Factory.StartNew(AddToModel);
    public void AddToModel()
    {
        var ips = NetworkScanner.ScanNetCards();
        foreach (var ip in ips)
        {
            var ipR = new IpRegion(ip);
            SettingsModel.Instance._oRecordedThisTime.Add(ipR);
                SettingsModel.Instance.RecordedThisTime.Add(ipR);
        }
        foreach (var item in SettingsModel._ipBlackList)
        {
            var tarCl = SettingsModel.Instance.RecordedThisTime.Where(x => x.IPBase.StartsWith(item));
            if (tarCl.Count() > 0)
            {
                var len = tarCl.Count();
                for (int i = 0; i < len; i++)
                {
                    SettingsModel.Instance.IPBlackCollection.Add(tarCl.ToArray()[i]);
                    SettingsModel.Instance.RecordedThisTime.Remove(tarCl.ToArray()[i]);
                }
            }
        }
    }
}