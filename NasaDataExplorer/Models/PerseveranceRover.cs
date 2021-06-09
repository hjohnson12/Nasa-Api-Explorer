using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.Models
{
    public class PerseveranceRover : Base.Observable
    {
        private IEnumerable<Photo> _photos;

        [JsonProperty("photos")]
        public IEnumerable<Photo> Photos { get => _photos; set {_photos = value; OnPropertyChanged(); } }
        
        public class Camera
        {
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("rover_id")]
            public int rover_id { get; set; }
            [JsonProperty("full_name")]
            public string full_name { get; set; }
        }

        public class Rover
        {
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("landing_date")]
            public string landing_date { get; set; }
            [JsonProperty("launch_date")]
            public string launch_date { get; set; }
            [JsonProperty("status")]
            public string status { get; set; }
            [JsonProperty("max_sol")]
            public int max_sol { get; set; }
            [JsonProperty("max_date")]
            public string max_date { get; set; }
            [JsonProperty("total_photos")]
            public int total_photos { get; set; }
            //[JsonProperty("cameras")]
            //public IList<RoverCamera> cameras { get; set; }
        }

        public class RoverCamera
        {
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("full_name")]
            public string full_name { get; set; }
        }

        public class Photo
        {
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("sol")]
            public int sol { get; set; }
            [JsonProperty("camera")]
            public Camera camera { get; set; }
            [JsonProperty("img_src")]
            public string img_src { get; set; }
            [JsonProperty("earth_date")]
            public string earth_date { get; set; }
            [JsonProperty("rover")]
            public Rover rover { get; set; }
        }
    }
}
