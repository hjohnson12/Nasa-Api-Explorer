﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Hosting;
using NasaDataExplorer.Services;
using NasaDataExplorer.Views;
using NasaDataExplorer.Views.Dialogs;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NasaDataExplorer.Services.Nasa;
using NasaDataExplorer.Services.Nasa.MarsRoverPhotos;
using NasaDataExplorer.Services.Nasa.Apod;

namespace NasaDataExplorer
{
    public static class StaticKeys
    {
        public static string API_KEY { get; set; }
    }

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public IHost ServiceHost { get; set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            ConfigureAndBuildServices();
        }

        /// <summary>
        /// Configures and builds the Nasa API service
        /// </summary>
        public void ConfigureAndBuildServices()
        {
            var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient<IRoverPhotoService, RoverPhotoService>();
                services.AddHttpClient<IAstronomyPictureOfTheDayService, AstronomyPictureOfTheDayService>();
                services.AddTransient<INasaApiService, NasaApiService>();
                services.AddTransient<IDownloaderService, DownloaderService>();
            }).UseConsoleLifetime();
            ServiceHost = builder.Build();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Register NASA API Value
            // Check to see if API KEY is in your Environment Variables with name NASA_API_KEY
            if (Environment.GetEnvironmentVariable("NASA_API_KEY") == null)
            {
                // Key not available, halt application?
                InvalidAPIKeyDialogView dialog = new InvalidAPIKeyDialogView();
                await dialog.ShowAsync();
            }
            else
            {
                StaticKeys.API_KEY = Environment.GetEnvironmentVariable("NASA_API_KEY").ToString();
            }

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();

                // Hide default title bar and extend your content into the title bar area
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

                var titleBar = ApplicationView.GetForCurrentView().TitleBar;

                // Set active window colors
                titleBar.ForegroundColor = Windows.UI.Colors.White;
                titleBar.BackgroundColor = Windows.UI.Colors.Transparent;
                titleBar.ButtonForegroundColor = Windows.UI.Colors.White;
                titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
                titleBar.ButtonHoverForegroundColor = Windows.UI.Colors.White;
                titleBar.ButtonHoverBackgroundColor = Windows.UI.Colors.SlateGray;
                titleBar.ButtonPressedForegroundColor = Windows.UI.Colors.White;
                titleBar.ButtonPressedBackgroundColor = Windows.UI.Colors.DimGray;

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Windows.UI.Colors.White;
                titleBar.InactiveBackgroundColor = Windows.UI.Colors.Transparent;
                titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.White;
                titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
