using NasaDataExplorer.Services;
using NasaDataExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.ViewModels
{
    public class OpportunityRoverPhotosViewModel
    {
        private INasaApiService _nasaApiService;

        public List<OpportunityRover.Photo> OpportunityPhotos { get; set; }
        public List<OpportunityRover> OpportunityRover { get; set; }

        public OpportunityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task<List<OpportunityRover.Photo>> LoadCuriosityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            OpportunityPhotos = await _nasaApiService.GetOpportunityRoverPhotosAsync(date);
            return OpportunityPhotos;
        }
    }
}
