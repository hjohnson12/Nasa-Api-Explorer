using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.Models
{
    // Rover Types: 
    // Perseverance, Curiosity, Opportunity
    public class MarsRoverPhotoData : Base.Observable
    {
        public enum CameraNames
        {
            /// <summary>
            /// Front Hazard Avoidance Camera
            /// </summary>
            FHAZ,      
            /// <summary>
            /// Rear Hazard Avoidance Camera
            /// </summary>
            RHAZ,
            /// <summary>
            /// Mast Camera
            /// </summary>
            MAST,
            /// <summary>
            /// Chemistry and Camera Complex
            /// </summary>
            CHEMCAM,
            /// <summary>
            /// Mars Hand Lens Imager
            /// </summary>
            MAHLI,
            /// <summary>
            /// Mars Descent Imager
            /// </summary>
            MARDI,
            /// <summary>
            /// Navigation Camera
            /// </summary>
            NAVCAM,
            /// <summary>
            /// Panoramic Camera
            /// </summary>
            PANCAM,
            /// <summary>
            /// Miniature Thermal Emission Spectrometer (Mini-TES)
            /// </summary>
            MINITES   
        }

        private IEnumerable<MarsRoverPhoto> _photos;

        [JsonProperty("photos")]
        public IEnumerable<MarsRoverPhoto> Photos { get => _photos; set { _photos = value; OnPropertyChanged(); } }
    }
}
