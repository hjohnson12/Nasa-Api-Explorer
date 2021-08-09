using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NasaApiExplorer.Models
{
    public class NaturalEventTracker
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("categories")]
        public IList<Category> Categories { get; set; }

        [JsonProperty("events")]
        public IList<Event> Events { get; set; }

        public class Category
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("title")]
            public string title { get; set; }

            [JsonProperty("link")]
            public string link { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("layers")]
            public string layers { get; set; }
        }

        public class Source
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("url")]
            public string url { get; set; }
        }

        public class Geometry
        {
            [JsonProperty("magnitudeValue")]
            public double? magnitudeValue { get; set; }

            [JsonProperty("magnitudeUnit")]
            public string magnitudeUnit { get; set; }

            [JsonProperty("date")]
            public DateTime date { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("coordinates")]
            public IList<object> coordinates { get; set; }
        }

        public class Event
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("title")]
            public string title { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("link")]
            public string link { get; set; }

            [JsonProperty("closed")]
            public object closed { get; set; }

            [JsonProperty("categories")]
            public IList<Category> categories { get; set; }

            [JsonProperty("sources")]
            public IList<Source> sources { get; set; }

            [JsonProperty("geometry")]
            public IList<Geometry> geometry { get; set; }
        }
    }

    
}
