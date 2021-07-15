using System.Collections.Generic;
using Newtonsoft.Json;

namespace NasaDataExplorer.Models
{
    // Rover Types: 
    // Perseverance, Curiosity, Opportunity
    public class MarsRoverPhotoData : Base.Observable
    {
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

        public enum PerseveranceCameraNames
        {
            /// <summary>
            /// Rover Up-Look Camera
            /// </summary>
            EDL_RUCAM,
            /// <summary>
            /// Rover Down-Look Camera
            /// </summary>
            EDL_RDCAM,
            /// <summary>
            /// Descent Stage Down-Look Camera
            /// </summary>
            EDL_DDCAM,
            /// <summary>
            /// Parachute Up-Look Camera A
            /// </summary>
            EDL_PUCAM1,
            /// <summary>
            /// Parachute Up-Look Camera B
            /// </summary>
            EDL_PUCAM2,
            /// <summary>
            /// Navigation Camera - Left
            /// </summary>
            NAVCAM_LEFT,
            /// <summary>
            /// Navigation Camera - Right
            /// </summary>
            NAVCAM_RIGHT,
            /// <summary>
            /// Mast Camera Zoom - Right
            /// </summary>
            MCZ_RIGHT,
            /// <summary>
            /// Mast Camera Zoom - Left
            /// </summary>
            MCZ_LEFT,
            /// <summary>
            ///  Front Hazard Avoidance Camera - Left
            /// </summary>
            FRONT_HAZCAM_LEFT_A,
            /// <summary>
            /// Front Hazard Avoidance Camera - Right
            /// </summary>
            FRONT_HAZCAM_RIGHT_A,
            /// <summary>
            /// Rear Hazard Avoidance Camera - Left
            /// </summary>
            REAR_HAZCAM_LEFT,
            /// <summary>
            /// Rear Hazard Avoidance Camera - Right
            /// </summary>
            REAR_HAZCAM_RIGHT,
            /// <summary>
            /// MEDA Skycam
            /// </summary>
            SKYCAM,
            /// <summary>
            /// SHERLOC WATSON Camera
            /// </summary>
            SHERLOC_WATSON
        }

        private IEnumerable<MarsRoverPhoto> _photos;

        [JsonProperty("photos")]
        public IEnumerable<MarsRoverPhoto> Photos { get => _photos; set { _photos = value; OnPropertyChanged(); } }
    }
}