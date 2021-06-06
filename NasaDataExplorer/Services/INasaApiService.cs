using NasaDataExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services
{
    public interface INasaApiService
    {
        Task<List<OpportunityRover.Photo>> GetOpportunityRoverPhotosAsync(string specifiedDate);

        Task<List<CuriosityRover.Photo>> GetCuriosityRoverPhotosAsync(string specifiedDate);

        Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDayAsync();
    }
}
