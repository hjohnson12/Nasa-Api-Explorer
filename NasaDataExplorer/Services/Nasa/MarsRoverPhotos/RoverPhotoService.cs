using NasaDataExplorer.Extensions;
using NasaDataExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services.Nasa.MarsRoverPhotos
{
    public class RoverPhotoService : IRoverPhotoService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates a new NasaApiService instance as a typed client.
        /// </summary>
        /// <param name="client"></param>
        public RoverPhotoService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.nasa.gov/");
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = client;
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetCuriosityRoverPhotosAsync(string dateOfPhotos)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetCuriosityRoverPhotosAsync(
            string dateOfPhotos,
            CancellationToken cancellationToken)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri, cancellationToken);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetPerseveranceRoverPhotosAsync(
            string dateOfPhotos,
            CancellationToken cancellationToken)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/perseverance/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri, cancellationToken);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetPerseveranceRoverPhotosAsync(
            string dateOfPhotos,
            string camera,
            CancellationToken cancellationToken)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/perseverance/photos?api_key={2}&earth_date={0}&camera={1}",
                    dateOfPhotos,
                    camera,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri, cancellationToken);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetOpportunityRoverPhotosAsync(string dateOfPhotos)
        {
            var requestUri =
                string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/opportunity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri);
        }

        public async Task<IEnumerable<MarsRoverPhoto>> GetRoverPhotosAsync(string uriAddress)
        {
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
    }
}
