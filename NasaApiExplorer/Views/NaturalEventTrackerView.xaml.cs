using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using NasaApiExplorer.Models;
using NasaApiExplorer.Services.NasaApis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using NasaApiExplorer.ViewModels;

namespace NasaApiExplorer.Views
{
    public sealed partial class NaturalEventTrackerView : Page
    {
        // TODO: Move to view model, use service, use httpclient
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
