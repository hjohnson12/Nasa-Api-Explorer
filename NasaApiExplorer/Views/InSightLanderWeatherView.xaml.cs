﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NasaApiExplorer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NasaApiExplorer.Views
{
    // Update 07/2021: Insight Weather API currently is down
    // Info here https://mars.nasa.gov/insight/weather/

    public sealed partial class InSightLanderWeatherView : Page
    {
        public ObservableCollection<SolDay> SolWeek = new ObservableCollection<SolDay>();

        public InSightLanderWeatherView()
        {
            this.InitializeComponent();

            InitializeWeather();
        }

        // Update 07/2021: Insight Weather API currently is down
        // Info here https://mars.nasa.gov/insight/weather/
        public void InitializeWeather()
        {
            var webRequest = WebRequest.Create(String.Format("https://api.nasa.gov/insight_weather/?api_key={0}&feedtype=json&ver=1.0", StaticKeys.API_KEY)) as HttpWebRequest;
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