using NasaApiExplorer.ViewModels;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Windows.UI.Xaml.Controls;

namespace NasaApiExplorer.Views
{
    public sealed partial class SpiritRoverPhotosView : Page
    {
        public SpiritPhotosViewModel ViewModel => (SpiritPhotosViewModel)DataContext;

        public SpiritRoverPhotosView()
        {
            this.InitializeComponent();

            this.DataContext =
                App.Current.ServiceHost.Services.GetService<SpiritPhotosViewModel>();

            var missionStartDate = new DateTimeOffset(2004, 1, 5, default, default, default, default);

            // Last contact date for Spirit Rover per Wiki
            var missionEndDate = new DateTimeOffset(2010, 3, 22, default, default, default, default);

            RoverPhotosDatePicker.MinDate = missionStartDate;
            RoverPhotosDatePicker.MaxDate = missionEndDate;
        }
    }
}
