using Newtonsoft.Json;

namespace NasaApiExplorer.Models
{
    public class MarsRoverCamera
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rover_id")]
        public int RoverId { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }
}