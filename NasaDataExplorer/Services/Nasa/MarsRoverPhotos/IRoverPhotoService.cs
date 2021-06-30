using NasaDataExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services.Nasa.MarsRoverPhotos
{
    public interface IRoverPhotoService
    {
        Task<IEnumerable<MarsRoverPhoto>> GetOpportunityRoverPhotosAsync(string dateOfPhotos);
        Task<IEnumerable<MarsRoverPhoto>> GetCuriosityRoverPhotosAsync(string dateOfPhotos);
        Task<IEnumerable<MarsRoverPhoto>> GetCuriosityRoverPhotosAsync(string dateOfPhotos, CancellationToken cancellationToken);
        Task<IEnumerable<MarsRoverPhoto>> GetPerseveranceRoverPhotosAsync(string dateOfPhotos, CancellationToken cancellationToken);
        Task<IEnumerable<MarsRoverPhoto>> GetPerseveranceRoverPhotosAsync(string dateOfPhotos, string camera, CancellationToken cancellationToken);
    }
}
