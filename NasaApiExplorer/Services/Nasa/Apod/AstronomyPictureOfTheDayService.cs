using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NasaApiExplorer.Extensions;

namespace NasaApiExplorer.Services.Nasa.Apod
{
    /// <summary>
    /// Class for interacting with the Astronomy Picture of the Day API
    /// </summary>
    public class AstronomyPictureOfTheDayService : IAstronomyPictureOfTheDayService
    {
        private readonly HttpClient _httpClient;

        public AstronomyPictureOfTheDayService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.nasa.gov/");
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = client;
        }

        /// <summary>
        /// Retrieves the Nasa Astronomy Picture of the Day
        /// <para>See the <see href="https://apod.nasa.gov/apod/astropix.html">website</see>.</para>
        /// </summary>
        /// <returns></returns>
        public async Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDayAsync()
        {
            var requestUri = string.Format(
                    "https://api.nasa.gov/planetary/apod?api_key={0}",
                    StaticKeys.API_KEY);

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var astronomyPictureOfTheDay = stream.ReadAndDeserializeFromJson<AstronomyPictureOfTheDay>();
                return astronomyPictureOfTheDay;
            }
        }
    }
}