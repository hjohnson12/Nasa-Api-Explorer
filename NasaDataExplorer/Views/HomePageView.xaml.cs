﻿using Microsoft.Extensions.DependencyInjection;
using NasaDataExplorer.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePageView : Page
    {
        private INasaApiService _nasaApiService;

        public AstronomyPictureOfTheDay PictureOfDay { get; set; }

        public HomePageView()
        {
            this.InitializeComponent();
            _nasaApiService = ((App)Application.Current).NasaApiServiceHost.Services.GetRequiredService<INasaApiService>();

        }

        /// <summary>
        /// Initializes the "Astronomy Picture of the Day" data
        /// </summary>
        public async Task InitializePictureOfDay()
        {
            PictureOfDay = await _nasaApiService.GetAstronomyPictureOfTheDayAsync();
            imgPictureOfDay.Source = new BitmapImage(
                new Uri("https://apod.nasa.gov/apod/image/2106/PIA24622-Curiosity_Clouds_Mont_Mercou1100.jpg"));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializePictureOfDay();
        }
    }
}
