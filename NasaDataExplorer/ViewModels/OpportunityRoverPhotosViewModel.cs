using NasaDataExplorer.Services;
using NasaDataExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NasaDataExplorer.ViewModels
{
    public class OpportunityRoverPhotosViewModel
    {
        private INasaApiService _nasaApiService;

        public ObservableCollection<OpportunityRover.Photo> OpportunityPhotos { get; set; }
        public ObservableCollection<OpportunityRover> OpportunityRover { get; set; }

        public OpportunityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task<ObservableCollection<OpportunityRover.Photo>> LoadCuriosityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            OpportunityPhotos = (ObservableCollection<OpportunityRover.Photo>)await _nasaApiService.GetOpportunityRoverPhotosAsync(date);
            return OpportunityPhotos;
        }
    }
}
