using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaApiExplorer.Base;
using NasaApiExplorer.Models;
using NasaApiExplorer.Services;

namespace NasaApiExplorer.ViewModels
{
    public class RoverPhotoDialogViewModel : Base.Observable
    {
        private IFileDownloadService _fileDownloadService;
        private MarsRoverPhoto _currentPhoto;
        private List<MarsRoverPhoto> _roverPhotos;

        public ICommand ChangeSelectionCommand { get; set; }
        public ICommand DownloadImageCommand { get; set; }
        
        public RoverPhotoDialogViewModel(IFileDownloadService downloaderService)
        {
            _fileDownloadService = downloaderService;

            ChangeSelectionCommand = 
                new Base.RelayCommand<MarsRoverPhoto>(ChangeSelection, () => true);
            DownloadImageCommand =
                new AsyncRelayCommand(DownloadImage, () => !CurrentPhoto.ImageSourceUrl.Equals(""));
        }

        public List<MarsRoverPhoto> RoverPhotos
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

        /// <summary>
        /// Downloads the currently selected image from its url in a location
        /// chosen through a folder picker.
        /// </summary>
        /// <returns></returns>
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