﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaApiExplorer.Services.NasaApis.Apod
{
    /// <summary>
    /// Interface for interacting with Nasa's Astronomy Picture of the Day API
    /// </summary>
    public interface IAstronomyPictureOfTheDayService
    {
        /// <summary>
        /// Retrieves the Nasa Astronomy Picture of the Day
        /// </summary>
        /// <returns></returns>
        Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDayAsync();
    }
}