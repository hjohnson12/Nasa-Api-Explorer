using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.Models
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
        public string Img_src { get; set; }
        [JsonProperty("earth_date")]
        public string Earth_date { get; set; }
        [JsonProperty("rover")]
        public MarsRover Rover { get; set; }
    }
}
