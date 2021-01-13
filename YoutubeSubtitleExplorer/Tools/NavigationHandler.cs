﻿// <copyright file="NavigationHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Drastic.Common.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Drastic.Common.Forms.Tools
{
    /// <summary>
    /// Navigation Handler.
    /// </summary>
    public class NavigationHandler : INavigationHandler
    {
        /// <inheritdoc/>
        public Task DisplayAlertAsync(string title, string message, string closeButton)
        {
            MainThread.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(title, message, closeButton).ConfigureAwait(false));
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task PushModalPageAsync(object page)
        {
            MainThread.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.Navigation.PushModalAsync((Page)page).ConfigureAwait(false));
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task PushPageAsync(object page)
        {
            var navigationPage = this.GetCurrentNavigationPage(Application.Current.MainPage);
            if (navigationPage != null && page is Page formsPage)
            {
                MainThread.BeginInvokeOnMainThread(async () => await navigationPage.Navigation.PushAsync(formsPage).ConfigureAwait(false));
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void SetMainAppPage(object page)
        {
            MainThread.BeginInvokeOnMainThread(() => Application.Current.MainPage = (Page)page);
        }

        private NavigationPage GetCurrentNavigationPage(Page page)
        {
            if (page is NavigationPage navPage)
            {
                return navPage;
            }

            if (page is TabbedPage tabbedPage)
            {
                return this.GetCurrentNavigationPage(tabbedPage.CurrentPage);
            }

            if (page is FlyoutPage flyoutPage)
            {
                return this.GetCurrentNavigationPage(flyoutPage.Detail);
            }

            return null;
        }
    }
}
