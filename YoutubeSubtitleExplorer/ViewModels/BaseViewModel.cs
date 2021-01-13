﻿// <copyright file="BaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Drastic.Common.Interfaces;
using Xamarin.Essentials;
using YoutubeSubtitleExplorer.Interfaces;

namespace Drastic.Common.ViewModels
{
    /// <summary>
    /// Base View Model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string title = string.Empty;
        private bool isBusy = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="properties">Platform Properties.</param>
        /// <param name="resource">Resources.</param>
        /// <param name="database">Database.</param>
        /// <param name="error">Error Handler.</param>
        /// <param name="navigation">Navigation Handler.</param>
        public BaseViewModel(IPlatformProperties properties, IResourceHelper resource, IDatabase database, IErrorHandler error, INavigationHandler navigation)
        {
            this.Error = error;
            this.Navigation = navigation;
            this.Database = database;
            this.PlatformProperties = properties;
            this.Resources = resource;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raised when CanExecute is changed.
        /// </summary>
        public event EventHandler RaiseCanExecuteChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the view is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.SetProperty(ref this.isBusy, value);
                this.RaiseCanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets the resource handler.
        /// </summary>
        protected IResourceHelper Resources { get; private set; }

        /// <summary>
        /// Gets the platform properties handler.
        /// </summary>
        protected IPlatformProperties PlatformProperties { get; private set; }

        /// <summary>
        /// Gets the database handler.
        /// </summary>
        protected IDatabase Database { get; private set; }

        /// <summary>
        /// Gets the navigation handler.
        /// </summary>
        protected INavigationHandler Navigation { get; private set; }

        /// <summary>
        /// Gets the error handler.
        /// </summary>
        protected IErrorHandler Error { get; private set; }

        /// <summary>
        /// Called when the page is appearing.
        /// </summary>
        /// <returns>Task.</returns>
        public virtual Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }

#pragma warning disable SA1600 // Elements should be documented
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
#pragma warning restore SA1600 // Elements should be documented
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// On Property Changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var changed = this.PropertyChanged;
                if (changed == null)
                {
                    return;
                }

                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }
    }
}
