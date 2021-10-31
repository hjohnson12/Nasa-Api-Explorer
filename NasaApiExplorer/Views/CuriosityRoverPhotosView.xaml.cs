using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using NasaApiExplorer.ViewModels;

namespace NasaApiExplorer.Views
{
    public sealed partial class CuriosityRoverPhotosView : Page
    {
        public CuriosityRoverPhotosViewModel ViewModel => (CuriosityRoverPhotosViewModel)DataContext;

        public CuriosityRoverPhotosView()
        {
            this.InitializeComponent();

            this.DataContext =
                App.Current.ServiceHost.Services.GetRequiredService<CuriosityRoverPhotosViewModel>();
            
            // Mission hasn't ended so can just set a previous date
            RoverPhotosDatePicker.MaxDate = DateTime.Today;
        }
    }
}