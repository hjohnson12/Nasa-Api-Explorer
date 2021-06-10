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

        public ObservableCollection<MarsRoverPhoto> OpportunityPhotos { get; set; }
        public ObservableCollection<MarsRoverPhoto> OpportunityRover { get; set; }

        public OpportunityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task<ObservableCollection<MarsRoverPhoto>> LoadCuriosityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            OpportunityPhotos = (ObservableCollection<MarsRoverPhoto>)await _nasaApiService.GetOpportunityRoverPhotosAsync(date);
            return OpportunityPhotos;
        }
    }
}
