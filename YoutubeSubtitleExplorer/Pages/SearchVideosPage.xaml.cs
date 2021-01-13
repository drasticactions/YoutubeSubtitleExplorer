// <copyright file="SearchVideosPage.xaml.cs" company="Drastic Actions">
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
using YoutubeSubtitleExplorer.ViewModels;

namespace YoutubeSubtitleExplorer.Pages
{
    /// <summary>
    /// Search Videos Page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchVideosPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchVideosPage"/> class.
        /// </summary>
        public SearchVideosPage()
        {
            this.InitializeComponent();
            this.BindingContext = App.Container.Resolve<SearchVideosViewModel>();
        }

        /// <inheritdoc/>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.videoSearchCollection.SelectedItem = null;
        }
    }
}