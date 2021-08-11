using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasaApiExplorer.Models;
using NasaApiExplorer.ViewModels;
using NasaApiExplorer.Views.Dialogs;
using System.Linq;

namespace NasaApiExplorer.Views
{
    public sealed partial class OpportunityRoverPhotosView : Page
    {
        public OpportunityRoverPhotosViewModel ViewModel => (OpportunityRoverPhotosViewModel)DataContext;

        public OpportunityRoverPhotosView()
        {
            this.InitializeComponent();

            this.DataContext =
                App.Current.ServiceHost.Services.GetRequiredService<OpportunityRoverPhotosViewModel>();

            var missionStartDate = new DateTimeOffset(2004, 1, 25, default, default, default, default);
            var missionEndDate = new DateTimeOffset(2018, 6, 10, default, default, default, default);
            
            RoverPhotosDatePicker.MinDate = missionStartDate;
            RoverPhotosDatePicker.MaxDate = missionEndDate;
        }
    }
}