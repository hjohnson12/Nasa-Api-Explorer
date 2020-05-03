using Astro.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace Astro.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InSightLander_WeatherView : Page
    {
        public ObservableCollection<SolDay> SolWeek = new ObservableCollection<SolDay>();

        public InSightLander_WeatherView()
        {
            this.InitializeComponent();

            InitializeWeather();

        }

        public void InitializeWeather()
        {
            //curiosityPhotos = new ObservableCollection<CuriosityRover.Photo>();

            var webRequest = WebRequest.Create("https://api.nasa.gov/insight_weather/?api_key=QAPd6iShWw0Qgx3Cd1t08wgXtKoCybGTCVLzxbgM&feedtype=json&ver=1.0") as HttpWebRequest;
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
                    var sol_keys = JObject.Parse(json)["sol_keys"].ToList();
                    foreach (var sol in sol_keys)
                    {
                        var sol_data = JObject.Parse(json)[sol.ToString()].ToString();
                        var solObj = JsonConvert.DeserializeObject<SolDay>(sol_data);

                        // Manually get Wind Direction data since JSON format is different
                        var wdData = JObject.Parse(sol_data)["WD"].ToString();
                        //foreach(var compassPoint in wdData)
                        //{
                        //    var cp = JsonConvert.DeserializeObject<SolDay.CompassPoint>(wdData);
                        //    //solObj.WD.CompassPoints.Add();
                        //}

                        // Set Sol date
                        solObj.Sol = (int)sol;
                        SolWeek.Add(solObj);
                    }
                }
            }
            string output = "";
            output = "On Sol " + SolWeek[0].Sol.ToString() + " the temps ranged from " + SolWeek[0].AtmostphericTemp.mx.ToString() + " degree's Celcius to " +
                SolWeek[0].AtmostphericTemp.mn.ToString() + " degrees celcius.";
            txtBlock.Text = output;
            //txtBlock.Text = CelciusToFarenheit(SolWeek[0].AtmostphericTemp.mn).ToString(); // DEBUG DATA
        }

        public double CelciusToFarenheit(double degC)
        {
            return (degC * (9.0 / 5.0)) + 32.0;
        }
    }
}
