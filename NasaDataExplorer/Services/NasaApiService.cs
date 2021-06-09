using NasaDataExplorer.Helpers;
using NasaDataExplorer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services
{
    public class NasaApiService : INasaApiService
    {
        private static HttpClient _httpClient = new HttpClient();

        // Supports direct, named, and typed instances
        private readonly IHttpClientFactory _httpClientFactory;

        private CancellationTokenSource _cancellationTokenSource =
            new CancellationTokenSource();

        public NasaApiService()
        {
            _httpClient.BaseAddress = new Uri("https://api.nasa.gov/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Constructor for TypedClient demo
        public NasaApiService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.nasa.gov/");
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = client;
        }

        public NasaApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            // Ex use: var client = _httpClientFactory.CreateClient();
        }
  
        public async Task<IEnumerable<CuriosityRover.Photo>> GetCuriosityRoverPhotosAsync(string specifiedDate)
        {
            // Another way with http CLient: helps avoid the middle memory stream
            // Using streams to reduce memory and improve performance with reads
            // Helps avoid socket exhaustion when not having a "using" with httpclient
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                String.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                    specifiedDate,
                    StaticKeys.API_KEY));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var curiosityRover = stream.ReadAndDeserializeFromJson<CuriosityRover>();

                return curiosityRover.Photos;
            }
        }

        public async Task<IEnumerable<CuriosityRover.Photo>> GetCuriosityRoverPhotosAsync(
            string specifiedDate,
            CancellationToken cancellationToken)
        {
            // Another way with http CLient: helps avoid the middle memory stream
            // Using streams to reduce memory and improve performance with reads
            // Helps avoid socket exhaustion when not having a "using" with httpclient
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                String.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                    specifiedDate,
                    StaticKeys.API_KEY));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                using (var response = await _httpClient.SendAsync(request, cancellationToken))
                {
                    await Task.Delay(6000);
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var curiosityRover = stream.ReadAndDeserializeFromJson<CuriosityRover>();
                    return curiosityRover.Photos;
                }
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"Operation cancelled with message {ocException.Message}");
                return new CuriosityRover().Photos;
            }
        }

        public async Task<IEnumerable<PerseveranceRover.Photo>> GetPerseveranceRoverPhotosAsync(
            string specifiedDate,
            CancellationToken cancellationToken)
        {
            // Another way with http CLient: helps avoid the middle memory stream
            // Using streams to reduce memory and improve performance with reads
            // Helps avoid socket exhaustion when not having a "using" with httpclient
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                String.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/perseverance/photos?earth_date={0}&api_key={1}",
                    specifiedDate,
                    StaticKeys.API_KEY));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                using (var response = await _httpClient.SendAsync(request, cancellationToken))
                {
                    await Task.Delay(6000);
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var perseveranceRover = stream.ReadAndDeserializeFromJson<PerseveranceRover>();
                    return perseveranceRover.Photos;
                }
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"Operation cancelled with message {ocException.Message}");
                return new PerseveranceRover().Photos;
            }
        }

        public async Task<IEnumerable<OpportunityRover.Photo>> GetOpportunityRoverPhotosAsync(string specifiedDate)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                String.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/opportunity/photos?earth_date={0}&api_key={1}",
                    specifiedDate,
                    StaticKeys.API_KEY));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var opportunityRover = stream.ReadAndDeserializeFromJson<OpportunityRover>();

                return opportunityRover.Photos;
            }
        }

        public async Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDayAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                String.Format(
                    "https://api.nasa.gov/planetary/apod?api_key={0}",
                    StaticKeys.API_KEY));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var astronomyPictureOfTheDay = stream.ReadAndDeserializeFromJson<AstronomyPictureOfTheDay>();

                return astronomyPictureOfTheDay;
            }
        }

        private async Task<IEnumerable<CuriosityRover.Photo>> GetCuriosityRoverPhotosWithoutStreamAsync(string specifiedDate)
        {
            // Test 2 - Through HttpRequest Message
            var request = new HttpRequestMessage(HttpMethod.Get,
                String.Format("mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}", specifiedDate, StaticKeys.API_KEY));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var curiosityRover = JsonConvert.DeserializeObject<CuriosityRover>(content);
            return curiosityRover.Photos;
        }

        private async Task<IEnumerable<CuriosityRover.Photo>> GetCuriosityRoverPhotosBasicTest(string specifiedDate)
        {
            // Test 1 
            var response = await _httpClient.GetAsync(
                String.Format("mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}", specifiedDate, StaticKeys.API_KEY));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var rover = JsonConvert.DeserializeObject<CuriosityRover>(content);
            return rover.Photos;
        }


        //===========================================
        // Debugging functions
        //===========================================

        public async Task RunCancelTest()
        {
            _cancellationTokenSource.CancelAfter(2000);
            await GetCuriosityRoverPhotosAsync("6/04/2021", _cancellationTokenSource.Token);
        }

        private async Task DeleteResource()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                "StringHere");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
        }

        private async Task DeleteResourceShortcut()
        {
            var response = await _httpClient.DeleteAsync(
                "StringHere");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
        }

    }
}
