using System;
using NasaDataExplorer.Services.Nasa.Apod;
using NasaDataExplorer.Services.Nasa.MarsRoverPhotos;

namespace NasaDataExplorer.Services.Nasa
{
    /// <summary>
    /// Class for interacting with other api services from a
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
        /// Interface instance to interact with the Mars Rover 
        /// Photos service.
        /// </summary>
        public IRoverPhotoService MarsRoverPhotos { get; set; }

        /// <summary>
        /// Interface instance to interact with the Astronomy Picture
        /// of the Day service.
        /// </summary>
        public IAstronomyPictureOfTheDayService Apod { get; set; }
    }
}