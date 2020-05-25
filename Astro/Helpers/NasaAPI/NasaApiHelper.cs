using Astro.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Astro.Helpers.NasaAPI
{
     public static class NasaApiHelper
    {
        private static string API_KEY = "";

        public static void RegisterAPI(string apiKey)
        {
            API_KEY = apiKey;
        }

        public async static Task<AstronomyPOD> GetAstronomyPODAsync()
        {
            var webRequest = System.Net.WebRequest.Create(String.Format("https://api.nasa.gov/planetary/apod?api_key={0}", StaticKeys.API_KEY)) as System.Net.HttpWebRequest;
            if (webRequest == null)
            {
                return null;
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var json = sr.ReadToEnd();
                    var astronomyPOD = JsonConvert.DeserializeObject<AstronomyPOD>(json);
                    return astronomyPOD;
                }
            }
        }

        public async static Task<List<CuriosityRover.Photo>> GetCuriosityRoverPhotosAsync(string specifiedDate)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string result = await httpClient.GetStringAsync(new Uri(String.Format("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}", specifiedDate, StaticKeys.API_KEY)));
                    var rover = JsonConvert.DeserializeObject<CuriosityRover>(result);
                    return rover.Photos;
                }
                catch (Exception ex)
                {
                    // ...
                    return null;
                }
            }
        }

        public async static Task<List<OpportunityRover.Photo>> GetOpportunityRoverPhotosAsync(string specifiedDate)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string result = await httpClient.GetStringAsync(new Uri(String.Format("https://api.nasa.gov/mars-photos/api/v1/rovers/opportunity/photos?earth_date={0}&api_key={1}", specifiedDate, StaticKeys.API_KEY)));
                    return JsonConvert.DeserializeObject<OpportunityRover>(result).Photos;
                }
                catch (Exception ex)
                {
                    // ...
                    return null;
                }
            }
        }
    }
}
