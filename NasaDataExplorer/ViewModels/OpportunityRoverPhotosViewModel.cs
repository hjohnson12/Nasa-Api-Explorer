using NasaDataExplorer.Services;
using NasaDataExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using NasaDataExplorer.Services.Nasa;

namespace NasaDataExplorer.ViewModels
{
    public class OpportunityRoverPhotosViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private ObservableCollection<MarsRoverPhoto> _opportunityPhotos;

        public OpportunityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public ObservableCollection<MarsRoverPhoto> OpportunityPhotos
        {
            get => _opportunityPhotos;
            set
            {
                if (_opportunityPhotos != value)
                    _opportunityPhotos = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<MarsRoverPhoto> OpportunityRover { get; set; }

        public async Task<ObservableCollection<MarsRoverPhoto>> LoadOpportunityRoverPhotos(
            string date)
        {
            OpportunityPhotos = new ObservableCollection<MarsRoverPhoto>(
                await _nasaApiService.MarsRoverPhotos.GetOpportunityRoverPhotosAsync(date));
            return OpportunityPhotos;
        }

        public async Task<ObservableCollection<MarsRoverPhoto>> LoadOpportunityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            OpportunityPhotos = new ObservableCollection<MarsRoverPhoto>(
                await _nasaApiService.MarsRoverPhotos.GetOpportunityRoverPhotosAsync(date));
            return OpportunityPhotos;
        }
    }
}
