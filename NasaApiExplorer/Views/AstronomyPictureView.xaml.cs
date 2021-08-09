using System;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using NasaApiExplorer.ViewModels;

namespace NasaApiExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AstronomyPictureView : Page
    {
        public AstronomyPictureViewModel ViewModel => (AstronomyPictureViewModel)DataContext;

        public AstronomyPictureView()
        {
            this.InitializeComponent();

            this.DataContext =
                App.Current.ServiceHost.Services.GetRequiredService<AstronomyPictureViewModel>();
        }
    }
}