using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Autofac;
using Drastic.Common.Interfaces;
using Drastic.Forms.UWP;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace YoutubeSubtitleExplorer.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            var builder = new ContainerBuilder();
            builder.RegisterType<WindowsPlatformProperties>().As<IPlatformProperties>();
            var app = new YoutubeSubtitleExplorer.App(builder);
            app.RequestedThemeChanged += this.App_RequestedThemeChanged;
            this.LoadApplication(app);
            this.SetTheme(app.RequestedTheme);
        }

        private void App_RequestedThemeChanged(object sender, Xamarin.Forms.AppThemeChangedEventArgs e)
        {
            this.SetTheme(e.RequestedTheme);
        }

        private void SetTheme(Xamarin.Forms.OSAppTheme theme)
        {
            switch (theme)
            {
                case Xamarin.Forms.OSAppTheme.Dark: this.RequestedTheme = ElementTheme.Dark; break;
                case Xamarin.Forms.OSAppTheme.Light: this.RequestedTheme = ElementTheme.Light; break;
                case Xamarin.Forms.OSAppTheme.Unspecified: this.RequestedTheme = ElementTheme.Default; break;
            }
        }
    }
}
