using Astro.Models;
using Astro.Views.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Astro.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OpportunityRoverPhotosView : Page
    {
        public ObservableCollection<OpportunityRover.Photo> OpportunityPhotos { get; set; }
        public OpportunityRoverPhotosView()
        {
            this.InitializeComponent();

            // The mission start and end date for Mars Opportunity Rover
            RoverPhotosDatePicker.MinDate = new DateTimeOffset(2004, 1, 25, default, default, default, default);
            RoverPhotosDatePicker.MaxDate = new DateTimeOffset(2018, 6, 10, default, default, default, default);
        }

        private async Task InitializePhotos_Opportunity(string date)
        {
            OpportunityPhotos = new ObservableCollection<OpportunityRover.Photo>();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    string result = await httpClient.GetStringAsync(new Uri(String.Format("https://api.nasa.gov/mars-photos/api/v1/rovers/opportunity/photos?earth_date={0}&api_key={1}", date, StaticKeys.API_KEY)));
                    var rover = JsonConvert.DeserializeObject<OpportunityRover>(result);
                    foreach (var photo in rover.Photos)
                    {
                        OpportunityPhotos.Add(photo);
                    }
                }
                catch
                {
                    // ...
                }
            }
            GridViewControl.ItemsSource = OpportunityPhotos;
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
            //CuriosityPhotoDetailsDialogView diag = new CuriosityPhotoDetailsDialogView(e.ClickedItem as OpportunityRover.Photo, OpportunityPhotos);
            //await diag.ShowAsync();
        }
    }
}
