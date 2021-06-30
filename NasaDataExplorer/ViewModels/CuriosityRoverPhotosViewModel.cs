using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using NasaDataExplorer.Services.Nasa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NasaDataExplorer.ViewModels
{
    public class CuriosityRoverPhotosViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private ObservableCollection<MarsRoverPhoto> _curiosityPhotos;

        public CuriosityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public ObservableCollection<MarsRoverPhoto> CuriosityPhotos
        {
            get { return _curiosityPhotos; }
            set
            {
                if (_curiosityPhotos != value)
                    _curiosityPhotos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MarsRoverPhoto> CuriosityRover { get; set; }

        public async Task<ObservableCollection<MarsRoverPhoto>> LoadCuriosityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            try
            {
                CuriosityPhotos = new ObservableCollection<MarsRoverPhoto>(
                        await _nasaApiService.MarsRoverPhotos.GetCuriosityRoverPhotosAsync(date));
                return CuriosityPhotos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CuriosityPhotos;
            }
        }
    }
}
