using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPChatterAllPlatforms.MyNet;

namespace TCPChatterAllPlatforms.Views;

public partial class MainPageView : UserControl

{
    internal static MainPageView View = new();

    public MainPageView()
    {
        InitializeComponent();
    }

}