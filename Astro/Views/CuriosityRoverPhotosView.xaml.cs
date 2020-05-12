using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web.Http;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Astro.Views.Dialogs;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Astro.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CuriosityRoverPhotosView : Page
    {
        ObservableCollection<CuriosityRover.Photo> curiosityPhotos = new ObservableCollection<CuriosityRover.Photo>();


        public CuriosityRoverPhotosView()
        {
            this.InitializeComponent();
            // Initialize here
            RoverPhotosDatePicker.Date = DateTimeOffset.Now.AddDays(-1);
        }

        private async Task InitializePhotos_Curiosity(string date)
        {
            
            curiosityPhotos = new ObservableCollection<CuriosityRover.Photo>();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    string result = await httpClient.GetStringAsync(new Uri(String.Format("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}", date, StaticKeys.API_KEY)));
                    var test = JsonConvert.DeserializeObject<CuriosityRover>(result);
                    foreach (var photo in test.Photos)
                    {
                        curiosityPhotos.Add(photo);
                    }
                }
                catch
                {
                    // ...
                }
            }
            GridViewControl.ItemsSource = curiosityPhotos;
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
                await InitializePhotos_Curiosity(photoDate);
            }
        }

        private async void GridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Frame.Navigate(typeof(PhotoDetailsView), e.ClickedItem);
            CuriosityPhotoDetailsDialogView diag = new CuriosityPhotoDetailsDialogView(e.ClickedItem as CuriosityRover.Photo, curiosityPhotos);
            await diag.ShowAsync();
        }

        //private void BtnNext_Click(object sender, RoutedEventArgs e)
        //{
        //    string date = "";
        //   // RoverPhotosDatePicker.Date.Value.AddDays(1);
        //    date = RoverPhotosDatePicker.Date.Value.Year.ToString() + "-" + RoverPhotosDatePicker.Date.Value.Month.ToString() + "-" + RoverPhotosDatePicker.Date.Value.AddDays(1).Day.ToString();
        //    RoverPhotosDatePicker.Date.Value.AddDays(1);
        //    RoverPhotosDatePicker.Date = new DateTimeOffset(RoverPhotosDatePicker.Date.Value.Year, RoverPhotosDatePicker.Date.Value.Month, RoverPhotosDatePicker.Date.Value.AddDays(1).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, TimeSpan.Zero);
        //    InitializePhotos_Curiosity(date);
        //}
        //private void BtnPrev_Click(object sender, RoutedEventArgs e)
        //{
        //    string date = "";
        //   // RoverPhotosDatePicker.Date.Value.AddDays(-1);
        //    date = RoverPhotosDatePicker.Date.Value.Year.ToString() + "-" + RoverPhotosDatePicker.Date.Value.Month.ToString() + "-" + RoverPhotosDatePicker.Date.Value.AddDays(-1).Day.ToString();
        //    InitializePhotos_Curiosity(date);
        //}

    }
}
