using Microsoft.Toolkit.Mvvm.Input;
using NasaDataExplorer.Base;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NasaDataExplorer.ViewModels
{
    public class RoverPhotoDialogViewModel : Base.Observable
    {
        private IDownloaderService _downloaderService;
        private MarsRoverPhoto _currentPhoto;
        private ObservableCollection<MarsRoverPhoto> _roverPhotos;

        public ICommand ChangeSelectionCommand { get; set; }
        public ICommand DownloadImageCommand { get; set; }

        public RoverPhotoDialogViewModel(IDownloaderService downloaderService,
            ObservableCollection<MarsRoverPhoto> roverPhotos,
            MarsRoverPhoto selectedPhoto)
        {
            _downloaderService = downloaderService;
            _roverPhotos = roverPhotos;
            _currentPhoto = selectedPhoto;

            ChangeSelectionCommand = 
                new Base.RelayCommand<MarsRoverPhoto>(ChangeSelection, () => true);
            DownloadImageCommand =
                new AsyncRelayCommand(DownloadImage, () => !CurrentPhoto.Img_src.Equals(""));
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

        public MarsRoverPhoto CurrentPhoto
        {
            get => _currentPhoto;
            set
            {
                if (_currentPhoto != value)
                    _currentPhoto = value;
                OnPropertyChanged();
            }
        }

        public void ChangeSelection(MarsRoverPhoto updatedSelection)
        {
            CurrentPhoto = updatedSelection;
        }

        public async Task DownloadImage()
        {
            try
            {
                await _downloaderService.DownloadFileAsync(CurrentPhoto.Img_src, @"C:\users\hlj51\desktop\");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}