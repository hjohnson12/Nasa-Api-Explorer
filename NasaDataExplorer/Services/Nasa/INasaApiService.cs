using System;
using NasaDataExplorer.Services.Nasa.Apod;
using NasaDataExplorer.Services.Nasa.MarsRoverPhotos;

namespace NasaDataExplorer.Services.Nasa
{
    /// <summary>
    /// Interface for a service interacting with some of Nasa's Open APIs.
    /// </summary>
    public interface INasaApiService
    {
        IRoverPhotoService MarsRoverPhotos { get; set; }
        IAstronomyPictureOfTheDayService Apod { get; set; }
    }
}