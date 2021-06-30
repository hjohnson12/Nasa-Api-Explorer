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
    /// Class for interacting with Nasa's Open APIs.
    /// </summary>
    public class NasaApiService : INasaApiService
    {
        public NasaApiService()
        {

        }
        public IRoverPhotoService MarsRoverPhotos { get; set; }
        public IAstronomyPictureOfTheDayService Apod { get; set; }
    }
}