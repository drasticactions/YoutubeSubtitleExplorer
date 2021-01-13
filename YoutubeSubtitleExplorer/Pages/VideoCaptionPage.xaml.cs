// <copyright file="VideoCaptionPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeSubtitleExplorer.ViewModels;

namespace YoutubeSubtitleExplorer.Pages
{
    /// <summary>
    /// Video Caption Page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCaptionPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoCaptionPage"/> class.
        /// </summary>
        /// <param name="video">YouTube video.</param>
        /// <param name="captionTrack">Captions.</param>
        public VideoCaptionPage(Video video, ClosedCaptionTrack captionTrack)
        {
            this.InitializeComponent();
            var vm = App.Container.Resolve<VideoCaptionViewModel>(new NamedParameter("video", video), new NamedParameter("captionTrack", captionTrack));
            this.BindingContext = vm;
        }
    }
}