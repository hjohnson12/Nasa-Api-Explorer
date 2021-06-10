using Microsoft.Extensions.DependencyInjection;
using NasaDataExplorer.Services;
using NasaDataExplorer.ViewModels;
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
        public AstronomyPictureOfTheDay PictureOfDay { get; set; }
        public HomePageViewModel ViewModel { get; set;  }

        public HomePageView()
        {
            this.InitializeComponent();

            ViewModel = 
                new HomePageViewModel(
                    ((App)Application.Current).ServiceHost.Services.GetRequiredService<INasaApiService>());
            
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Initializes the "Astronomy Picture of the Day" photo and data
        /// </summary>
        public async Task InitializePictureOfDay()
        {
            PictureOfDay = await ViewModel.LoadAstronomyPictureOfTheDay();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializePictureOfDay();
        }
    }
}
