using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.ViewModels
{
    public class CuriosityRoverPhotosViewModel
    {
        private INasaApiService _nasaApiService;

        public List<CuriosityRover.Photo> CuriosityPhotos { get; set; }
        public List<CuriosityRover> CuriosityRover { get; set; }

        public CuriosityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task<List<CuriosityRover.Photo>> LoadCuriosityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            CuriosityPhotos = await _nasaApiService.GetCuriosityRoverPhotosAsync(date);
            return CuriosityPhotos;
        }
    }
}
