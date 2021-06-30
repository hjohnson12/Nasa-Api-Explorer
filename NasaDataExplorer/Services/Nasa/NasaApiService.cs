using NasaDataExplorer.Extensions;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services.Nasa.Apod;
using NasaDataExplorer.Services.Nasa.MarsRoverPhotos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services.Nasa
{
    /// <summary>
    /// Class for interacting with some of Nasa's Open APIs
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