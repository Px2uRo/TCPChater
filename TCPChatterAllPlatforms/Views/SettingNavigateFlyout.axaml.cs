using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPChatterAllPlatforms.Views;

public partial class SettingNavigateFlyout : UserControl,IRecordPointer

{
    public static TcpClient _client;
    private NetworkStream _stream;
    private string _host = null;

    public static SettingNavigateFlyout Flyout { get;set; } = new SettingNavigateFlyout();
    public Point _startPoint { get; set ; }

    public SettingNavigateFlyout()
    {
        InitializeComponent();
    }

    private async Task TryConnectToServerAsync()
    {

    }

    private async void StartReadingMessages()
    {

    }

    private void SendButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    public void UserControl_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        _startPoint = e.GetPosition(this);
    }

    public void UserControl_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        if(e.GetPosition(this).X - _startPoint.X < 0)
        {
            FlyoutPageHelper.Exit();
        }
    }

    private void Grid_PointerPressed_1(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        FlyoutPageHelper.Exit();
        FlyoutPageHelper.AddToBorder(SettingView.View);
    }
}

public interface IRecordPointer
{
    public Point _startPoint { get; set; }
    public void UserControl_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e);
    public void UserControl_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e);
}