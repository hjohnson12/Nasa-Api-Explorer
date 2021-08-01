using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaDataExplorer.Base;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;

namespace NasaDataExplorer.ViewModels
{
    public class RoverPhotoDialogViewModel : Base.Observable
    {
        private IFileDownloadService _fileDownloadService;
        private MarsRoverPhoto _currentPhoto;
        private ObservableCollection<MarsRoverPhoto> _roverPhotos;

        public ICommand ChangeSelectionCommand { get; set; }
        public ICommand DownloadImageCommand { get; set; }
        
        public RoverPhotoDialogViewModel(
            IFileDownloadService downloaderService,
            ObservableCollection<MarsRoverPhoto> roverPhotos,
            MarsRoverPhoto selectedPhoto)
        {
            _fileDownloadService = downloaderService;
            _roverPhotos = roverPhotos;
            _currentPhoto = selectedPhoto;

            ChangeSelectionCommand = 
                new Base.RelayCommand<MarsRoverPhoto>(ChangeSelection, () => true);
            DownloadImageCommand =
                new AsyncRelayCommand(DownloadImage, () => !CurrentPhoto.ImageSourceUrl.Equals(""));
        }

        public ObservableCollection<MarsRoverPhoto> RoverPhotos
        {
            get => _roverPhotos;
            set => SetProperty(ref _roverPhotos, value);
        }

        public MarsRoverPhoto CurrentPhoto
        {
            get => _currentPhoto;
            set => SetProperty(ref _currentPhoto, value);
        }

        public void ChangeSelection(MarsRoverPhoto updatedSelection)
        {
            CurrentPhoto = updatedSelection;
        }

        public async Task DownloadImage()
        {
            try
            {
                await _fileDownloadService.DownloadFileAsync(CurrentPhoto.ImageSourceUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}