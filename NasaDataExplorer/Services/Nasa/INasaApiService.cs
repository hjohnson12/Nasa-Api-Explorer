using NasaDataExplorer.Services.Nasa.Apod;
using NasaDataExplorer.Services.Nasa.MarsRoverPhotos;

namespace NasaDataExplorer.Services.Nasa
{
    /// <summary>
    /// Interface for a service interacting with some of Nasa's Open APIs.
    /// </summary>
    public interface INasaApiService
    {
        /// <summary>
        /// Property to interact with the Mars Rover 
        /// Photos service.
        /// </summary>
        IRoverPhotoService MarsRoverPhotos { get; set; }

        /// <summary>
        /// Property to interact with the Astronomy Picture
        /// of the Day service.
        /// </summary>
        IAstronomyPictureOfTheDayService Apod { get; set; }
    }
}