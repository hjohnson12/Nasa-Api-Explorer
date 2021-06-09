using NasaDataExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services
{
    public interface INasaApiService
    {
        Task<IEnumerable<OpportunityRover.Photo>> GetOpportunityRoverPhotosAsync(string specifiedDate);

        Task<IEnumerable<CuriosityRover.Photo>> GetCuriosityRoverPhotosAsync(string specifiedDate);

        Task<IEnumerable<CuriosityRover.Photo>> GetCuriosityRoverPhotosAsync(string specifiedDate, CancellationToken cancellationToken);
        Task<IEnumerable<PerseveranceRover.Photo>> GetPerseveranceRoverPhotosAsync(string specifiedDate, CancellationToken cancellationToken);

        Task<AstronomyPictureOfTheDay> GetAstronomyPictureOfTheDayAsync();
    }
}
