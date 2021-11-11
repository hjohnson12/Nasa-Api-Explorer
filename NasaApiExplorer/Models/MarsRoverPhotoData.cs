using System.Collections.Generic;
using Newtonsoft.Json;

namespace NasaApiExplorer.Models
{
    /// <summary>
    /// The root object that contains the Photos and extra data fields
    /// </summary>
    public class MarsRoverPhotoData : Base.Observable
    {
        private IEnumerable<MarsRoverPhoto> _photos;

        /// <summary>
        /// Property populated with Photos for a rover
        /// </summary>
        [JsonProperty("photos")]
        public IEnumerable<MarsRoverPhoto> Photos { get => _photos; set { _photos = value; OnPropertyChanged(); } }

        /// <summary>
        /// Cameras pertaining to Perseverance Rover
        /// </summary>
        public static List<(string, string)> PerseveranceCameras =>
            new List<(string, string)>
            {
                ("EDL_RUCAM", "Rover Up-Look Camera"),
                ("EDL_RDCAM", "Rover Down-Look Camera"),
                ("EDL_DDCAM", "Descent Stage Down-Look Camera"),
                ("EDL_PUCAM1", "Parachute Up-Look Camera A"),
                ("EDL_PUCAM2", "Parachute Up-Look Camera B"),
                ("NAVCAM_LEFT", "Navigation Camera - Left"),
                ("NAVCAM_RIGHT", "Navigation Camera - Right"),
                ("MCZ_RIGHT", "Mast Camera Zoom - Right"),
                ("MCZ_LEFT",  "Mast Camera Zoom - Left"),
                ("FRONT_HAZCAM_LEFT_A", "Front Hazard Avoidance Camera - Left"),
                ("FRONT_HAZCAM_RIGHT_A", "Front Hazard Avoidance Camera - Right"),
                ("REAR_HAZCAM_LEFT", "Rear Hazard Avoidance Camera - Left"),
                ("REAR_HAZCAM_RIGHT", "Rear Hazard Avoidance Camera - Right"),
                ("SKYCAM", "MEDA Skycam"),
                ("SHERLOC_WATSON", "SHERLOC WATSON Camera")
            };

        /// <summary>
        /// Cameras pertaining to Curiosity Rover
        /// </summary>
        public static List<(string, string)> CuriosityCameras =>
            new List<(string, string)>
            {
                ("FHAZ", "Front Hazard Avoidance Camera"),
                ("RHAZ", "Rear Hazard Avoidance Camera"),
                ("MAST", "Mast Camrea"),
                ("CHEMCAM", "Chemistry and Camera Complex"),
                ("MAHLI", "Mars Hand Lens Imager"),
                ("MARDI", "Mars Descent Imager"),
                ("NAVCAM", "Navigation Camera"),
                ("PANCAM", "Panoramic Camera")
            };

        /// <summary>
        /// Cameras pertaining to Opportunity Rover
        /// </summary>
        public static List<(string, string)> OpportunityCameras =>
            new List<(string, string)>
            {
                ("FHAZ", "Front Hazard Avoidance Camera"),
                ("RHAZ", "Rear Hazard Avoidance Camera"),
                ("NAVCAM", "Navigation Camera"),
                ("PANCAM", "Panoramic Camera"),
                ("MINITES", "Miniature Thermal Emission Spectrometer (Mini-TES)")
            };

        /// <summary>
        /// Cameras pertaining to Spirit Rover
        /// </summary>
        public static List<(string, string)> SpiritCameras =>
            new List<(string, string)>
            {
                ("FHAZ", "Front Hazard Avoidance Camera"),
                ("RHAZ", "Rear Hazard Avoidance Camera"),
                ("NAVCAM", "Navigation Camera"),
                ("PANCAM", "Panoramic Camera"),
                ("MINITES", "Miniature Thermal Emission Spectrometer (Mini-TES)")
            };
    }
}