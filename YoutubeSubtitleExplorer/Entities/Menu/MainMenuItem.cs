﻿// <copyright file="MainMenuItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace YoutubeSubtitleExplorer.Entities.Menu
{
    /// <summary>
    /// Main Menu Item.
    /// </summary>
    public class MainMenuItem
    {
        /// <summary>
        /// Gets or sets the title for the main menu.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the icon image source.
        /// </summary>
        public ImageSource IconImageSource { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        public Page Page { get; set; }
    }
}
