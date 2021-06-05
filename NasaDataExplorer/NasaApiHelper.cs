using NasaDataExplorer.Models;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NasaDataExplorer
{
    public static class NasaApiHelper
    {
        private static string API_KEY = "";
        private static HttpClient _httpClient = new HttpClient();

        public static void RegisterAPI(string apiKey)
        {
            API_KEY = apiKey;
        }

        public async static Task<NasaDataExplorernomyPOD> GetNasaDataExplorernomyPODAsync()
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
                    var NasaDataExplorernomyPOD = JsonConvert.DeserializeObject<NasaDataExplorernomyPOD>(json);
                    return NasaDataExplorernomyPOD;
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
                    // Another way with http CLient: helps avoid the middle memory stream
                    var request = new HttpRequestMessage(
                        HttpMethod.Get,
                        String.Format(
                            "https://api.nasa.gov/mars-photos/api/v1/rovers/opportunity/photos?earth_date={0}&api_key={1}",
                            specifiedDate,
                            StaticKeys.API_KEY));
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var response = await httpClient.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var stream = await response.Content.ReadAsStreamAsync();


                        using (var streamReader = new StreamReader(stream))
                        {
                            using (var jsonTextReader = new JsonTextReader(streamReader))
                            {
                                var jsonSerializer = new JsonSerializer();
                                var opportunityRover = jsonSerializer.Deserialize<OpportunityRover>(jsonTextReader);
                            }
                        }

                    }

                    // One way with HTTP Client
                    string result = await httpClient.GetStringAsync(new Uri(String.Format("https://api.nasa.gov/mars-photos/api/v1/rovers/opportunity/photos?earth_date={0}&api_key={1}", specifiedDate, StaticKeys.API_KEY)));
                    var rover = JsonConvert.DeserializeObject<OpportunityRover>(result);
                    return rover.Photos;
                }
                catch
                {
                    // ...
                    return null;
                }
            }
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }
    }
}
