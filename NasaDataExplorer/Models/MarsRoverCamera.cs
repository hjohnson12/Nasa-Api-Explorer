using Newtonsoft.Json;

namespace NasaDataExplorer.Models
{
    public class MarsRoverCamera
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("rover_id")]
        public int Rover_id { get; set; }
        [JsonProperty("full_name")]
        public string Full_name { get; set; }
    }
}
