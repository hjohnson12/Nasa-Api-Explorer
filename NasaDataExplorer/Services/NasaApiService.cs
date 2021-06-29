using NasaDataExplorer.Extensions;
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
        private readonly HttpClient _httpClient;

        // Constructor for a TypedClient 
        public NasaApiService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.nasa.gov/");
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = client;
        }

        public async Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDayAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                string.Format(
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

        public async Task<IEnumerable<MarsRoverPhoto>> GetCuriosityRoverPhotosAsync(string dateOfPhotos)
        {
            var apiString = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(apiString);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetCuriosityRoverPhotosAsync(
            string dateOfPhotos,
            CancellationToken cancellationToken)
        {
            var apiString =string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(apiString, cancellationToken);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetPerseveranceRoverPhotosAsync(
            string dateOfPhotos,
            CancellationToken cancellationToken)
        {
            var apiString = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/perseverance/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(apiString, cancellationToken);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetPerseveranceRoverPhotosAsync(
            string dateOfPhotos,
            string camera,
            CancellationToken cancellationToken)
        {
            var apiString = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/perseverance/photos?api_key={2}&earth_date={0}&camera={1}",
                    dateOfPhotos,
                    camera,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(apiString, cancellationToken);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetOpportunityRoverPhotosAsync(string dateOfPhotos)
        {
            var apiString =
                string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/opportunity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(apiString);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetRoverPhotosAsync(string uriAddress)
        {
            // Another way with http CLient: helps avoid the middle memory stream
            // Using streams to reduce memory and improve performance with reads
            // Helps avoid socket exhaustion when not having a "using" with httpclient
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                uriAddress);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var roverData = stream.ReadAndDeserializeFromJson<MarsRoverPhotoData>();
                return roverData.Photos;
            }
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetRoverPhotosAsync(
            string uriAddress,
            CancellationToken cancellationToken)
        {
            // Another way with http CLient: helps avoid the middle memory stream
            // Using streams to reduce memory and improve performance with reads
            // Helps avoid socket exhaustion when not having a "using" with httpclient
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                uriAddress);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var roverData = stream.ReadAndDeserializeFromJson<MarsRoverPhotoData>();
                return roverData.Photos;
            }
        }

        //===========================================
        // Debugging functions
        //===========================================

        public async Task RunCancelTest()
        {
            var _cancellationTokenSource = new CancellationTokenSource();
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