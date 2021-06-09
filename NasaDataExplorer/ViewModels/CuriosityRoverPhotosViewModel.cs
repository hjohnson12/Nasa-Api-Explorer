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
    public class CuriosityRoverPhotosViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private ObservableCollection<CuriosityRover.Photo> _curiosityPhotos;

        public CuriosityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public ObservableCollection<CuriosityRover.Photo> CuriosityPhotos
        {
            get { return _curiosityPhotos; }
            set
            {
                if (_curiosityPhotos != value)
                    _curiosityPhotos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CuriosityRover> CuriosityRover { get; set; }

        public async Task<ObservableCollection<CuriosityRover.Photo>> LoadCuriosityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            try
            {
                CuriosityPhotos =
                    new ObservableCollection<CuriosityRover.Photo>(
                        await _nasaApiService.GetCuriosityRoverPhotosAsync(date));

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
