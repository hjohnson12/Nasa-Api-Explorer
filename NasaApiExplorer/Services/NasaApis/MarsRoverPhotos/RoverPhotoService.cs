using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NasaApiExplorer.Extensions;
using NasaApiExplorer.Models;

namespace NasaApiExplorer.Services.NasaApis.MarsRoverPhotos
{
    /// <summary>
    /// Class for interacting with Nasa's Mars Rover Photos API
    /// </summary>
    public class RoverPhotoService : IRoverPhotoService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates a new RoverPhotoService instance as a typed client.
        /// </summary>
        /// <param name="client">An instance of HttpClient</param>
        public RoverPhotoService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.nasa.gov/");
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = client;
        }

        /// <summary>
        /// Gets photos from the Mars Curiosity Rover
        /// </summary>
        /// <param name="dateOfPhotos"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MarsRoverPhoto>> GetCuriosityRoverPhotosAsync(string dateOfPhotos)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri);
        }

        /// <summary>
        /// Gets photos from the Mars Curiosity Rover
        /// </summary>
        /// <param name="dateOfPhotos"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MarsRoverPhoto>> GetCuriosityRoverPhotosAsync(
            string dateOfPhotos,
            CancellationToken cancellationToken = default)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri, cancellationToken);
        }

        /// <summary>
        /// Gets photos from the Mars Perseverance Rover
        /// </summary>
        /// <param name="dateOfPhotos"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MarsRoverPhoto>> GetSpiritRoverPhotosAsync(
            string dateOfPhotos,
            CancellationToken cancellationToken = default)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/spirit/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri, cancellationToken);
        }

        /// <summary>
        /// Gets photos from the Mars Perseverance Rover
        /// </summary>
        /// <param name="dateOfPhotos"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MarsRoverPhoto>> GetPerseveranceRoverPhotosAsync(
            string dateOfPhotos,
            CancellationToken cancellationToken = default)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/perseverance/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri, cancellationToken);
        }

        /// <summary>
        /// Gets photos from the Mars Perseverance Rover
        /// </summary>
        /// <param name="dateOfPhotos">Date of photos to retrieve</param>
        /// <param name="camera">Specific camera</param>
        /// <param name="cancellationToken">Token to process canellations</param>
        /// <returns></returns>
        public async Task<IEnumerable<MarsRoverPhoto>> GetPerseveranceRoverPhotosAsync(
            string dateOfPhotos,
            string camera,
            CancellationToken cancellationToken = default)
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/perseverance/photos?api_key={2}&earth_date={0}&camera={1}",
                    dateOfPhotos,
                    camera,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri, cancellationToken);
        }

        /// <summary>
        /// Gets photos from the Mars Opportunity Rover
        /// </summary>
        /// <param name="dateOfPhotos"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MarsRoverPhoto>> GetOpportunityRoverPhotosAsync(string dateOfPhotos)
        {
            var requestUri =
                string.Format(
                    "https://api.nasa.gov/mars-photos/api/v1/rovers/opportunity/photos?earth_date={0}&api_key={1}",
                    dateOfPhotos,
                    StaticKeys.API_KEY);

            return await GetRoverPhotosAsync(requestUri);
        }

        /// <summary>
        /// Retrieves photos for a rover according to its uriAddress
        /// </summary>
        /// <param name="uriAddress"></param>
        /// <returns></returns>
        private async Task<IEnumerable<MarsRoverPhoto>> GetRoverPhotosAsync(string uriAddress)
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

        /// <summary>
        /// Retrieves photos for a rover according to its uriAddress
        /// </summary>
        /// <param name="uriAddress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<IEnumerable<MarsRoverPhoto>> GetRoverPhotosAsync(
            string uriAddress,
            CancellationToken cancellationToken = default)
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