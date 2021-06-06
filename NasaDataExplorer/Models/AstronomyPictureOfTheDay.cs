using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer
{
    public class AstronomyPictureOfTheDay
    {
        [JsonProperty("copyright")]
        public string copyright { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("explanation")]
        public string explanation { get; set; }

        [JsonProperty("hdurl")]
        public string hdurl { get; set; }

        [JsonProperty("media_type")]
        public string media_type { get; set; }

        [JsonProperty("service_version")]
        public string service_version { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }
    }
}
