using System;
using System.IO;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasaApiExplorer.ViewModels;

namespace NasaApiExplorer.Views
{
    public sealed partial class PerseveranceRoverPhotosView : Page
    {
        public PerseveranceRoverPhotosViewModel ViewModel => (PerseveranceRoverPhotosViewModel)DataContext;

        public PerseveranceRoverPhotosView()
        {
            this.InitializeComponent();

            this.DataContext =
                    App.Current.ServiceHost.Services.GetService<PerseveranceRoverPhotosViewModel>();

            // Current day since mission is still active
            RoverPhotosDatePicker.MaxDate = DateTime.Today;
        }
    }
}