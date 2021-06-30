using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services.Nasa.Apod
{
    /// <summary>
    /// Interface for interacting with Nasa's Astronomy Picture of the Day API
    /// </summary>
    public interface IAstronomyPictureOfTheDayService
    {
        Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDayAsync();
    }
}
