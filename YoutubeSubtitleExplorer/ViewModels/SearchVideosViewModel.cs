// <copyright file="SearchVideosViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drastic.Common.Interfaces;
using Drastic.Common.Tools;
using Drastic.Common.ViewModels;
using FFImageLoading;
using YoutubeExplode;
using YoutubeSubtitleExplorer.Interfaces;
using YoutubeSubtitleExplorer.Pages;

namespace YoutubeSubtitleExplorer.ViewModels
{
    /// <summary>
    /// Search Videos View Model.
    /// </summary>
    public class SearchVideosViewModel : BaseViewModel
    {
        private AsyncCommand<string> performSearchCommand;
        private AsyncCommand<YoutubeExplode.Videos.Video> selectVideoCommand;
        private YoutubeClient ytClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchVideosViewModel"/> class.
        /// </summary>
        /// <param name="ytClient">YouTube Client.</param>
        /// <param name="properties">Platform Properties.</param>
        /// <param name="resource">Resources.</param>
        /// <param name="database">Database.</param>
        /// <param name="error">Error Handler.</param>
        /// <param name="navigation">Navigation Handler.</param>
        public SearchVideosViewModel(
            YoutubeClient ytClient,
            IPlatformProperties properties,
            IResourceHelper resource,
            IDatabase database,
            IErrorHandler error,
            INavigationHandler navigation)
            : base(properties, resource, database, error, navigation)
        {
            this.ytClient = ytClient ?? throw new ArgumentNullException(nameof(ytClient));
        }

        /// <summary>
        /// Gets the searched videos.
        /// </summary>
        public ObservableCollection<YoutubeExplode.Videos.Video> Videos { get; private set; } = new ObservableCollection<YoutubeExplode.Videos.Video>();

        /// <summary>
        /// Gets the search command.
        /// </summary>
        public AsyncCommand<string> PerformSearchCommand
        {
            get
            {
                return this.performSearchCommand ??= new AsyncCommand<string>(this.PerformSearchAsync, null, this.Error);
            }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        public AsyncCommand<YoutubeExplode.Videos.Video> SelectVideoCommand
        {
            get
            {
                return this.selectVideoCommand ??= new AsyncCommand<YoutubeExplode.Videos.Video>(this.SelectVideoAsync, null, this.Error);
            }
        }

        private async Task SelectVideoAsync(YoutubeExplode.Videos.Video video)
        {
            if (video == null)
            {
                return;
            }

            var manifest = await this.ytClient.Videos.ClosedCaptions.GetManifestAsync(video.Id).ConfigureAwait(false);
            if (manifest == null || !manifest.Tracks.Any())
            {
                return;
            }

            YoutubeExplode.Videos.ClosedCaptions.ClosedCaptionTrack caption = await this.ytClient.Videos.ClosedCaptions.GetAsync(manifest.Tracks[0]).ConfigureAwait(false);
            await this.Navigation.PushPageAsync(new VideoCaptionPage(video, caption)).ConfigureAwait(false);
        }

        private async Task PerformSearchAsync(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            this.Videos.Clear();
            IReadOnlyList<YoutubeExplode.Videos.Video> searchResults = await this.ytClient.Search.GetVideosAsync(text, 1, 1);
            foreach (var video in searchResults)
            {
                this.Videos.Add(video);
            }
        }
    }
}
