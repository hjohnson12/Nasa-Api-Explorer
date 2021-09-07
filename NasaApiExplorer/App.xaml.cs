﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NasaApiExplorer.Services;
using NasaApiExplorer.Views;
using NasaApiExplorer.Views.Dialogs;
using NasaApiExplorer.Services.Nasa;
using NasaApiExplorer.Services.Nasa.MarsRoverPhotos;
using NasaApiExplorer.Services.Nasa.Apod;
using NasaApiExplorer.ViewModels;
using NasaApiExplorer.Helpers;

namespace NasaApiExplorer
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

        public IHost ServiceHost { get; set; }

        /// <summary>
        /// Gets the current application instance.
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the IServiceProvider instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures and builds services with view models for dependency injection.
        /// </summary>
        private void ConfigureAndBuildServices()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Services
                    services.AddHttpClient<IRoverPhotoService, RoverPhotoService>();
                    services.AddHttpClient<IAstronomyPictureOfTheDayService, AstronomyPictureOfTheDayService>();
                    services.AddSingleton<IFolderService, FolderService>();
                    services.AddHttpClient<IFileDownloadService, FileDownloadService>();
                    services.AddSingleton<INasaApiService, NasaApiService>();
                    services.AddSingleton<IDialogService, DialogService>();

                    // View Models
                    services.AddTransient<AstronomyPictureViewModel>();
                    services.AddTransient<PerseveranceRoverPhotosViewModel>();
                    services.AddTransient<CuriosityRoverPhotosViewModel>();
                    services.AddTransient<OpportunityRoverPhotosViewModel>();
                    services.AddTransient<RoverPhotoDialogViewModel>();
                }).UseConsoleLifetime();

            ServiceHost = builder.Build();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services
            services.AddHttpClient<IRoverPhotoService, RoverPhotoService>();
            services.AddHttpClient<IAstronomyPictureOfTheDayService, AstronomyPictureOfTheDayService>();
            services.AddSingleton<IFolderService, FolderService>();
            services.AddHttpClient<IFileDownloadService, FileDownloadService>();
            services.AddSingleton<INasaApiService, NasaApiService>();
            services.AddSingleton<IDialogService, DialogService>();

            // View Models
            services.AddTransient<AstronomyPictureViewModel>();
            services.AddTransient<PerseveranceRoverPhotosViewModel>();
            services.AddTransient<CuriosityRoverPhotosViewModel>();
            services.AddTransient<OpportunityRoverPhotosViewModel>();
            services.AddTransient<RoverPhotoDialogViewModel>();

            return services.BuildServiceProvider();
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

                // Expands into view and sets theme
                ConfigureTitleBar();
            }
        }

        /// <summary>
        /// Configure application title bar to application theme.
        /// </summary>
        void ConfigureTitleBar()
        {
            TitleBarHelper.ExpandViewIntoTitleBar();
            TitleBarHelper.SetupTitleBar();
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