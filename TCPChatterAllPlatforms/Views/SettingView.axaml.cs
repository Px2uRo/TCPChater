using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPChatterAllPlatforms.Views;

public partial class SettingView : UserControl

{
    public static SettingView View = new SettingView();
    public SettingView()
    {
        InitializeComponent();
    }
}