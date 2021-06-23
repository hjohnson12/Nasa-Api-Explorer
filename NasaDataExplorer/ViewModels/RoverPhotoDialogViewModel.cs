using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.ViewModels
{
    public class RoverPhotoDialogViewModel : Base.Observable
    {
        private IDownloaderService _downloaderService;
        private MarsRoverPhoto _selectedPhoto;
        private ObservableCollection<MarsRoverPhoto> _roverPhotos;

        public RoverPhotoDialogViewModel(IDownloaderService downloaderService,
            ObservableCollection<MarsRoverPhoto> roverPhotos,
            MarsRoverPhoto selectedPhoto)
        {
            _downloaderService = downloaderService;
            _roverPhotos = roverPhotos;
            _selectedPhoto = selectedPhoto;
        }

        public ObservableCollection<MarsRoverPhoto> RoverPhotos
        {
            get => _roverPhotos;
            set
            {
                if (_roverPhotos != value)
                    _roverPhotos = value;
                OnPropertyChanged();
            }
        }

        public MarsRoverPhoto SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                if (_selectedPhoto != value)
                    _selectedPhoto = value;
                OnPropertyChanged();
            }
        }

        public void ChangeSelection(MarsRoverPhoto updatedSelection)
        {
            SelectedPhoto = updatedSelection;
        }

        public async Task DownloadImageAsync()
        {
            try
            {
                await _downloaderService.DownloadFileAsync(SelectedPhoto.Img_src, @"C:\users\hlj51\desktop\");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
