using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPChatterAllPlatforms.Views;

public partial class MainView : UserControl, IRecordPointer
{ 
    public MainView() 
    { 
        InitializeComponent();
        black.IsVisible = false;
        flyout.IsVisible = false;
    }

    public Point _startPoint { get ; set ; }

    public void UserControl_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _startPoint = e.GetPosition(this);
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        TCPChatterAllPlatforms.FlyoutPageHelper.ShowFlyout(SettingNavigateFlyout.Flyout);
    }

    private void Rectangle_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        FlyoutPageHelper.Exit();
    }

    public void UserControl_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        if (e.GetPosition(this).X - _startPoint.X > 0)
        {
            FlyoutPageHelper.ShowFlyout(SettingNavigateFlyout.Flyout);
        }
    }
}

