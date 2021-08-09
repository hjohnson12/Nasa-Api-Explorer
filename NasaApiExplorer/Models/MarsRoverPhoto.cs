using Newtonsoft.Json;

namespace NasaApiExplorer.Models
{
    public class MarsRoverPhoto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sol")]
        public int Sol { get; set; }

        [JsonProperty("camera")]
        public MarsRoverCamera Camera { get; set; }

        [JsonProperty("img_src")]
        public string ImageSourceUrl { get; set; }

        [JsonProperty("earth_date")]
        public string EarthDate { get; set; }

        [JsonProperty("rover")]
        public MarsRover Rover { get; set; }
    }
}