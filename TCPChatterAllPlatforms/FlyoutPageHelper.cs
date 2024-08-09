using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPChatterAllPlatforms.Views;

namespace TCPChatterAllPlatforms
{
    public static class FlyoutPageHelper
    {

        public static MainView MainView { get; set; }

        public static void ShowFlyout(UserControl view)
        {
            MainView.flyout.IsVisible = true;
            var db = 0 - MainView.Bounds.Width * (2d / 3d);
            if (double.IsNaN(view.Width))
            {
                view.Width = 0 - db;
            }
            view.Height = MainView.Bounds.Height;
            if (MainView.flyout.Children.Count > 0)
            {
                MainView.flyout.Children.RemoveAll(MainView.flyout.Children);
            }
            MainView.flyout.Children.Add(view);
            view.SetValue(Canvas.LeftProperty,db);
            _showBgrdAnim.RunAsync(MainView.black);
            _showFlyoutAnim(db).RunAsync(view);
        }

        static FlyoutPageHelper()
        {
            if (App.Current?.ApplicationLifetime is ISingleViewApplicationLifetime app)
            {
                FlyoutPageHelper.MainView = app.MainView as MainView;
            }
            else if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime app2)
            {
                FlyoutPageHelper.MainView = (app2.MainWindow as MainWindow).mv;
            }
        }
        static Easing ease = new CircularEaseInOut();
        static Animation _showBgrdAnim = new Animation()
        {
            Easing=ease,
            FillMode = FillMode.Forward,
            Duration = TimeSpan.FromSeconds(0.3d),
            Children =
                {
                    new KeyFrame
                    {
                        Cue = new Cue(0d),
                        Setters =
                        {
                            new Setter(Rectangle.OpacityProperty, 0d),
                            new Setter(Rectangle.IsVisibleProperty, true)
                        }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(0.5d),
                        Setters =
                        {
                            new Setter(Rectangle.OpacityProperty, 1d),
                            new Setter(Rectangle.IsVisibleProperty, true)
                        }
                    }
                }
        };
        private static Animation _hideBgrdAnim = new Animation()
        {
            Easing = ease,
            FillMode = FillMode.Forward,
            Duration = TimeSpan.FromSeconds(0.3d),
            Children =
                {
                    new KeyFrame
                    {
                        Cue = new Cue(0d),
                        Setters =
                        {
                            new Setter(Rectangle.OpacityProperty, 1d),
                        }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(0.3d),
                        Setters =
                        {
                            new Setter(Rectangle.OpacityProperty, 0d),
                            new Setter(Rectangle.IsVisibleProperty, false)
                        }
                    }
                }
        };

        static Animation _showFlyoutAnim(double db) => new Animation()
        {
            Easing=ease,
            FillMode = FillMode.Forward, 
            Duration = TimeSpan.FromSeconds(0.3d),
            Children =
                {
                    new KeyFrame
                    {
                        Cue = new Cue(0d),
                        Setters =
                        {
                            new Setter(Canvas.LeftProperty, db),
                        }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(1d),
                        Setters =
                        {
                            new Setter(Canvas.LeftProperty, 0d),
                        }
                    }
                }
        };


        internal static void AddToBorder(Control view)
        {
            MainView.MainBorder.Child = null;
            MainView.MainBorder.Child = view;
        }
        internal static void Exit()
        {
            var view = MainView.flyout.Children[0];
            var db = 0 - MainView.Bounds.Width * (2d / 3d);
            _hideBgrdAnim.RunAsync(MainView.black);
            var ani = _hideFlyoutAnim(db).RunAsync(view);
            new Thread(() =>
            {
                Thread.Sleep(320);
                Dispatcher.UIThread.Invoke(new Action(() =>
                {
                    MainView.flyout.IsVisible = false;
                }));
            }).Start();
        }

        private static Animation _hideFlyoutAnim(double db) => new Animation()
        {
            Easing = ease,
            FillMode = FillMode.Forward,
            Duration = TimeSpan.FromSeconds(0.3d),
            Children =
                {
                    new KeyFrame
                    {
                        Cue = new Cue(0d),
                        Setters =
                        {
                            new Setter(Canvas.LeftProperty, 0d),
                        }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(1d),
                        Setters =
                        {
                            new Setter(Canvas.LeftProperty, db),
                        }
                    }
                }
        };

    }
}
