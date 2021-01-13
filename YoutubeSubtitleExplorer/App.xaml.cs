using Autofac;
using Drastic.Common.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeSubtitleExplorer.Entities.Settings;
using YoutubeSubtitleExplorer.Interfaces;

namespace YoutubeSubtitleExplorer
{
    public partial class App : Application
    {
        /// <summary>
        /// Autofac Container.
        /// </summary>
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1401 // Fields should be private
        public static IContainer Container;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA2211 // Non-constant fields should not be visible

        public App(ContainerBuilder builder)
        {
            Device.SetFlags(new string[] { "MediaElement_Experimental", "Shell_UWP_Experimental", "AppTheme_Experimental", "CollectionView_Experimental", "Shapes_Experimental" });
            this.InitializeComponent();
            Container = AppContainerBuilder.BuildContainer(builder);
            var database = Container.Resolve<IDatabase>();
            var platform = Container.Resolve<IPlatformProperties>();
            var navigation = Container.Resolve<INavigationHandler>();
            var resourceHelper = Container.Resolve<IResourceHelper>();
            var settings = database.GetAppSettings();

            // If we're using the default system settings.
            if (settings.UseSystemThemeSettings)
            {
                if (platform.IsDarkTheme)
                {
                    resourceHelper.SetDarkMode();
                }
                else
                {
                    resourceHelper.SetLightMode();
                }
            }
            else
            {
                if (settings.CustomTheme != AppCustomTheme.None)
                {
                    resourceHelper.SetCustomTheme(settings.CustomTheme);
                }
                else
                {
                    if (settings.UseDarkMode)
                    {
                        resourceHelper.SetDarkMode();
                    }
                    else
                    {
                        resourceHelper.SetLightMode();
                    }
                }
            }

            this.MainPage = new MainPage();

            //if (Device.Idiom == TargetIdiom.Desktop || Device.Idiom == TargetIdiom.Tablet)
            //{
            //    this.MainPage = new MainFlyoutPage(GenerateMenuItems(), GenerateSettingsItem());
            //}
            //else
            //{
            //    this.MainPage = new MainPage();
            //}
        }

        /// <summary>
        /// Gets the current pages background color.
        /// </summary>
        /// <returns>Xamarin Forms Color.</returns>
        public static Xamarin.Forms.Color GetCurrentBackgroundColor()
        {
            return App.Current.MainPage.BackgroundColor;
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
        }

        /// <inheritdoc/>
        protected override void OnSleep()
        {
        }

        /// <inheritdoc/>
        protected override void OnResume()
        {
        }
    }
}
