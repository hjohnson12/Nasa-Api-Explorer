using Newtonsoft.Json;

namespace NasaApiExplorer.Models
{
    public class MarsRover
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("landing_date")]
        public string LandingDate { get; set; }

        [JsonProperty("launch_date")]
        public string LaunchDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("total_photos")]
        public int TotalPhotos { get; set; }
    }
}