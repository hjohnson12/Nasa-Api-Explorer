using NasaDataExplorer.Models;
using NasaDataExplorer.Services.Nasa;
using NasaDataExplorer.Services.Nasa.Apod;
using NasaDataExplorer.Services.Nasa.MarsRoverPhotos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
