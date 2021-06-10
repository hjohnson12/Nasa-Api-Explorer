using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.ViewModels
{
    public class PerseveranceRoverPhotosViewModel
    {
        private INasaApiService _nasaApiService;

        public PerseveranceRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public ObservableCollection<MarsRoverPhoto> PerseverancePhotos { get; set; }
        public MarsRover PerseveranceRover { get; set; }

        public async Task<ObservableCollection<MarsRoverPhoto>> LoadCuriosityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            PerseverancePhotos = (ObservableCollection<MarsRoverPhoto>)await _nasaApiService.GetOpportunityRoverPhotosAsync(date);
            PerseveranceRover = PerseverancePhotos[0].Rover;
            return PerseverancePhotos;
        }
    }
}
