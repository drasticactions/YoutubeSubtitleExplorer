// <copyright file="AppContainerBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Drastic.Common.Forms.Tools;
using Drastic.Common.Interfaces;
using YoutubeSubtitleExplorer.Controls;
using YoutubeSubtitleExplorer.Database;
using YoutubeSubtitleExplorer.Interfaces;
using YoutubeSubtitleExplorer.ViewModels;

namespace YoutubeSubtitleExplorer
{
    /// <summary>
    /// App Container Builder.
    /// </summary>
    public static class AppContainerBuilder
    {
        /// <summary>
        /// Builds SocialMediaApp Container.
        /// </summary>
        /// <param name="builder">Platform Specific Container.</param>
        /// <returns>IContainer.</returns>
        public static IContainer BuildContainer(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.RegisterType<AppDatabase>().As<IDatabase>().SingleInstance();
            builder.RegisterType<NavigationHandler>().As<INavigationHandler>().SingleInstance();
            builder.RegisterType<ErrorHandler>().As<IErrorHandler>().SingleInstance();
            builder.RegisterType<ResourceHelper>().As<IResourceHelper>().SingleInstance();
            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<SearchVideosViewModel>();
            builder.RegisterType<VideoCaptionViewModel>();
            builder.RegisterInstance(new YoutubeExplode.YoutubeClient()).SingleInstance();
            builder.RegisterType<AppPopup>().As<YoutubeSubtitleExplorer.Interfaces.IPopup>().SingleInstance();
            return builder.Build();
        }
    }
}
