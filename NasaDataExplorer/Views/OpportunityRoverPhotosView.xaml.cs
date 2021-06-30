using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using NasaDataExplorer.Services.Nasa;
using NasaDataExplorer.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OpportunityRoverPhotosView : Page
    {
        public ObservableCollection<MarsRoverPhoto> OpportunityPhotos =
            new ObservableCollection<MarsRoverPhoto>();

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

        private async Task InitializePhotos_Opportunity(string date)
        {
            try
            {
                OpportunityPhotos = new ObservableCollection<MarsRoverPhoto>(
                    await ViewModel.LoadOpportunityRoverPhotos(date));
            }
            catch (Exception ex)
            {
                var logger = ((App)Application.Current).ServiceHost.Services.GetRequiredService<ILogger<App>>();
                logger.LogError(ex, "An error occurred.");
            }
        }

        private async void RoverPhotosDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var calendarName = sender.Name.ToString();
            if (string.IsNullOrEmpty(args.NewDate.ToString()))
                return;
            else
            {
                var datePicked = args.NewDate;
                var photoDate = datePicked.Value.Year.ToString() + "-" + datePicked.Value.Month.ToString() + "-" + datePicked.Value.Day.ToString();
                await InitializePhotos_Opportunity(photoDate);
            }
        }

        private async void GridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Frame.Navigate(typeof(PhotoDetailsView), e.ClickedItem);
            //CuriosityPhotoDetailsDialogView diag = new CuriosityPhotoDetailsDialogView(e.ClickedItem as MarsRoverPhoto, OpportunityPhotos);
            //await diag.ShowAsync();
        }
    }
}
