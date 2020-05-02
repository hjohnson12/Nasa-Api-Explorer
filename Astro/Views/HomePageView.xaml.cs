using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace Astro.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePageView : Page
    {
        public AstronomyPOD PictureOfDay { get; set; }

        public HomePageView()
        {
            this.InitializeComponent();
     

            InitializePictureOfDay();
        }

        public void InitializePictureOfDay()
        {
            var webRequest = WebRequest.Create("https://api.nasa.gov/planetary/apod?api_key=QAPd6iShWw0Qgx3Cd1t08wgXtKoCybGTCVLzxbgM") as HttpWebRequest;
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
                    var response = JsonConvert.DeserializeObject<AstronomyPOD>(json);
                    PictureOfDay = response;
                }
            }
        }
    }
}
