using System;
using NasaApiExplorer.Services.Nasa.Apod;
using NasaApiExplorer.Services.Nasa.MarsRoverPhotos;

namespace NasaApiExplorer.Services.Nasa
{
    /// <summary>
    /// Class for interacting with other nasa api services from a
    /// single location
    /// </summary>
    public class NasaApiService : INasaApiService
    {
        public NasaApiService(
            IRoverPhotoService roverPhotoService,
            IAstronomyPictureOfTheDayService apodService)
        {
            MarsRoverPhotos = roverPhotoService;
            Apod = apodService;
        }

        /// <summary>
        /// Interact with the Mars Rover Photos service with an injected
        /// instance of its interface
        /// </summary>
        public IRoverPhotoService MarsRoverPhotos { get; set; }

        /// <summary>
        /// Interact with the Astronomy Picture of the Day service with 
        /// an injected instance of its interface
        /// </summary>
        public IAstronomyPictureOfTheDayService Apod { get; set; }
    }
}