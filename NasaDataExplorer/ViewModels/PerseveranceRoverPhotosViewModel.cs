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
    public class PerseveranceRoverPhotosViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private ObservableCollection<MarsRoverPhoto> _perseverancePhotos;

        public PerseveranceRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public ObservableCollection<MarsRoverPhoto> PerseverancePhotos
        {
            get => _perseverancePhotos;
            set
            {
                if (_perseverancePhotos != value)
                    _perseverancePhotos = value;
                OnPropertyChanged();
            }
        }

        public MarsRover PerseveranceRover { get; set; }

        public async Task<ObservableCollection<MarsRoverPhoto>> LoadPerseveranceRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            PerseverancePhotos = new ObservableCollection<MarsRoverPhoto>(await _nasaApiService.GetPerseveranceRoverPhotosAsync(date,
                cancellationToken));
            PerseveranceRover = PerseverancePhotos[0].Rover;
            return PerseverancePhotos;
        }
    }
}
