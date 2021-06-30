using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services.Nasa;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using NasaDataExplorer.Services;
using NasaDataExplorer.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NaturalEventTrackerView : Page
    {
        // Debug
        ObservableCollection<NaturalEventTracker.Event> events;
        ObservableCollection<NaturalEventTracker.Category> categories;

        public NaturalEventTrackerViewModel ViewModel => (NaturalEventTrackerViewModel)DataContext;

        public NaturalEventTrackerView()
        {
            this.InitializeComponent();

            this.DataContext =
                new NaturalEventTrackerViewModel(
                    ((App)Application.Current).ServiceHost.Services.GetRequiredService<INasaApiService>());

            InitializeEventTrackerCategories();
            InitializeEventTrackerEvents();
        }

        public void InitializeEventTrackerCategories()
        {
            var webRequest = WebRequest.Create("https://eonet.sci.gsfc.nasa.gov/api/v3/categories") as HttpWebRequest;
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
                    var eventTracker = JsonConvert.DeserializeObject<NaturalEventTracker>(json);
                    categories = new ObservableCollection<NaturalEventTracker.Category>(eventTracker.Categories);
                }
            }
        }

        public void InitializeEventTrackerEvents()
        {
            var webRequest = WebRequest.Create("https://eonet.sci.gsfc.nasa.gov/api/v3/events?limit=5&days=500&source=InciWeb,EO&status=closed") as HttpWebRequest;
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
                    var eventTracker = JsonConvert.DeserializeObject<NaturalEventTracker>(json);
                    events = new ObservableCollection<NaturalEventTracker.Event>(eventTracker.Events);
                    txtBlock.Text = "Returned Event from API Below:\n" + events[0].title + ": " +
                        events[0].id;
                }
            }
        }
    }
}
