using Newtonsoft.Json;

namespace NasaDataExplorer.Models
{
    public class MarsRover
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("landing_date")]
        public string Landing_date { get; set; }
        [JsonProperty("launch_date")]
        public string Launch_date { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("total_photos")]
        public int Total_photos { get; set; }
    }
}
