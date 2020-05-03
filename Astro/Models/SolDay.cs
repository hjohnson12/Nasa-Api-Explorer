using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astro.Models
{
    public class SolDay
    {
        /// <summary>
        /// The solar day on Mars
        /// </summary>
        public int Sol { get; set; }

        /// <summary>
        /// Per-sol atmospheric temperature sensor summary data
        /// </summary>
        [JsonProperty("AT")]
        public AT AtmostphericTemp { get; set; }

        /// <summary>
        /// TIme of first datum, of any sensor, for the Sol (UTC; YYYY-MM-DDTHH:MM:SSZ)
        /// </summary>
        [JsonProperty("First_UTC")]
        public DateTime First_UTC { get; set; }

        /// <summary>
        /// Per-sol atmospheric pressure sensor summary data
        /// </summary>
        [JsonProperty("HWS")]
        public HWS HorizontalWindSpeed { get; set; }

        /// <summary>
        /// Time of last datum, of any sensor, for the Sol (UTC; YYYY-MM-DDTHH:MM:SSZ)
        /// </summary>
        [JsonProperty("Last_UTC")]
        public DateTime Last_UTC { get; set; }

        /// <summary>
        ///  Per-sol atmospheric pressure sensor summary data
        /// </summary>
        [JsonProperty("PRE")]
        public PRE AtmosphericPressure { get; set; }

        /// <summary>
        /// Per-sol season on Mars; one of ["winter, "spring", "summer", "fall"]
        /// </summary>
        [JsonProperty("Season")]
        public string Season { get; set; }

        /// <summary>
        /// Per-sol wind direction sensor summary data
        /// </summary>
        [JsonProperty("WD")]
        public WD WindDirection { get; set; }

        public class CompassPoint
        {
            /// <summary>
            /// The compass direction of the center of the compass point
            /// </summary>
            [JsonProperty("compass_degrees")]
            public double compass_degrees { get; set; }

            /// <summary>
            /// Name of the compass point, i.e., "N" for North, "ESE" or East-SouthEast
            /// </summary>
            [JsonProperty("compass_point")]
            public string compass_point { get; set; }

            /// <summary>
            /// The positive-right (positive-east), horizontal compontent of a unit vector indicating the direction of the compass point
            /// </summary>
            [JsonProperty("compass_right")]
            public double compass_right { get; set; }

            /// <summary>
            /// The positive-up (positive-north), vertical component of a unit vector indicating the direction of the compass point
            /// </summary>
            [JsonProperty("compass_up")]
            public double compass_up { get; set; }

            /// <summary>
            /// The number of samples for the Sol in the 11.25 degree around this compass point
            /// </summary>
            [JsonProperty("ct")]
            public int ct { get; set; }
        }

        public class WD
        {
            // BUG with this class parsing 
            public int CompassId { get; set; }

            // Compass something
            public IList<CompassPoint> CompassPoints { get; set; }

            [JsonProperty("most_common")]
            public CompassPoint most_common { get; set; }
        }

        public class AT
        {
            /// <summary>
            /// Average of samples over the Sol (Farenheit for AT; m/s for HWS; Pa for PRE)
            /// </summary>
            [JsonProperty("av")]
            public double av { get; set; }

            /// <summary>
            /// TOtal number of recorded samples over the Sol
            /// </summary>
            [JsonProperty("ct")]
            public int ct { get; set; }

            /// <summary>
            /// Minimum data sample over the sol (same units as av)
            /// </summary>
            [JsonProperty("mn")]
            public double mn { get; set; }

            /// <summary>
            /// Maximum data sample over the sol (same units as av)
            /// </summary>
            [JsonProperty("mx")]
            public double mx { get; set; }
        }

        public class HWS
        {
            /// <summary>
            /// Average of samples over the Sol (Farenheit for AT; m/s for HWS; Pa for PRE)
            /// </summary>
            [JsonProperty("av")]
            public double av { get; set; }

            /// <summary>
            /// TOtal number of recorded samples over the Sol
            /// </summary>
            [JsonProperty("ct")]
            public int ct { get; set; }

            /// <summary>
            /// Minimum data sample over the sol (same units as av)
            /// </summary>
            [JsonProperty("mn")]
            public double mn { get; set; }

            /// <summary>
            /// Maximum data sample over the sol (same units as av)
            /// </summary>
            [JsonProperty("mx")]
            public double mx { get; set; }
        }

        public class PRE
        { 
            /// <summary>
            /// Average of samples over the Sol (Farenheit for AT; m/s for HWS; Pa for PRE)
            /// </summary>
            [JsonProperty("av")]
            public double av { get; set; }

            /// <summary>
            /// TOtal number of recorded samples over the Sol
            /// </summary>
            [JsonProperty("ct")]
            public int ct { get; set; }

            /// <summary>
            /// Minimum data sample over the sol (same units as av)
            /// </summary>
            [JsonProperty("mn")]
            public double mn { get; set; }

            /// <summary>
            /// Maximum data sample over the sol (same units as av)
            /// </summary>
            [JsonProperty("mx")]
            public double mx { get; set; }
        }
    }
}
