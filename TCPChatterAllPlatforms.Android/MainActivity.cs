﻿using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;

namespace TCPChatterAllPlatforms.Android;

[Activity(
    Label = "TCPChatterAllPlatforms.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        App.IPProvdider = new AndroidIPProvider(this);
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}
