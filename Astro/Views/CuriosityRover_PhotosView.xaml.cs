using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Testing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CuriosityRover_PhotosView : Page
    {
        ObservableCollection<CuriosityRover.Photo> curiosityPhotos = new ObservableCollection<CuriosityRover.Photo>();

        public CuriosityRover_PhotosView()
        {
            this.InitializeComponent();

            // Initialize here
            RoverPhotosDatePicker.Date = DateTimeOffset.Now.AddDays(-1);
        }

        public void InitializePhotos_Curiosity(string date)
        {
            curiosityPhotos = new ObservableCollection<CuriosityRover.Photo>();

            var webRequest = WebRequest.Create(String.Format("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key=QAPd6iShWw0Qgx3Cd1t08wgXtKoCybGTCVLzxbgM", date)) as HttpWebRequest;
            if (webRequest == null)
            {
                return;
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var json = sr.ReadToEnd();
                    var response = JsonConvert.DeserializeObject<CuriosityRover>(json);
                    foreach (var photo in response.Photos)
                    {
                        curiosityPhotos.Add(photo);
                    }
                }
            }
            GridViewControl.ItemsSource = curiosityPhotos;
        }

        private void RoverPhotosDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var calendarName = sender.Name.ToString();
            if (string.IsNullOrEmpty(args.NewDate.ToString()))
                return;
            else
            {
                var datePicked = args.NewDate;
                var photoDate = datePicked.Value.Year.ToString() + "-" + datePicked.Value.Month.ToString() + "-" + datePicked.Value.Day.ToString();
                InitializePhotos_Curiosity(photoDate);
            }
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
