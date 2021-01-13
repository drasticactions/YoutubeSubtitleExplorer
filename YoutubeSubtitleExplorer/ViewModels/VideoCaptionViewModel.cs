// <copyright file="VideoCaptionViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drastic.Common.Interfaces;
using Drastic.Common.Tools;
using Drastic.Common.ViewModels;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeSubtitleExplorer.Interfaces;

namespace YoutubeSubtitleExplorer.ViewModels
{
    /// <summary>
    /// Video Caption View Model.
    /// </summary>
    public class VideoCaptionViewModel : BaseViewModel
    {
        private AsyncCommand<string> performSearchCommand;
        private Video video;
        private ClosedCaptionTrack captionTrack;
        private List<ClosedCaption> captions;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoCaptionViewModel"/> class.
        /// </summary>
        /// <param name="video">YouTube video.</param>
        /// <param name="captionTrack">Captions.</param>
        /// <param name="properties">Platform Properties.</param>
        /// <param name="resource">Resources.</param>
        /// <param name="database">Database.</param>
        /// <param name="error">Error Handler.</param>
        /// <param name="navigation">Navigation Handler.</param>
        public VideoCaptionViewModel(
            Video video,
            ClosedCaptionTrack captionTrack,
            IPlatformProperties properties,
            IResourceHelper resource,
            IDatabase database,
            IErrorHandler error,
            INavigationHandler navigation)
                : base(properties, resource, database, error, navigation)
        {
            if (captionTrack == null)
            {
                throw new ArgumentNullException(nameof(captionTrack));
            }

            this.Video = video;
            this.CaptionTrack = captionTrack;
            this.Captions = captionTrack.Captions.ToList();
        }

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        public Video Video
        {
            get
            {
                return this.video;
            }

            set
            {
                this.SetProperty(ref this.video, value);
            }
        }

        /// <summary>
        /// Gets or sets the captions.
        /// </summary>
        public ClosedCaptionTrack CaptionTrack
        {
            get
            {
                return this.captionTrack;
            }

            set
            {
                this.SetProperty(ref this.captionTrack, value);
            }
        }

        /// <summary>
        /// Gets or sets the captions.
        /// </summary>
        public List<ClosedCaption> Captions
        {
            get
            {
                return this.captions;
            }

            set
            {
                this.SetProperty(ref this.captions, value);
            }
        }

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

        private async Task PerformSearchAsync(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                this.Captions = this.captionTrack.Captions.ToList();
                return;
            }

            this.Captions = this.captionTrack.Captions.Where(n => n.Text.ToUpperInvariant().Contains(text.ToUpperInvariant())).ToList();
        }
    }
}
