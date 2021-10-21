using NasaApiExplorer.Models;
using NasaApiExplorer.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NasaApiExplorer.Services
{
    public class DialogService : IDialogService
    {
        public bool _isDialogOpen;

        public async Task ShowPhotoDialog(MarsRoverPhoto marsRoverPhoto, IList<MarsRoverPhoto> photos)
        {
            var roverPhotos = new List<MarsRoverPhoto>(photos);
            var dialog = new RoverPhotoDialog(
                marsRoverPhoto,
                roverPhotos);

            await ShowDialog(dialog);
        }

        private async Task ShowDialog(ContentDialog dialog)
        {
            if (_isDialogOpen)
                return;

            _isDialogOpen = true;

            await dialog.ShowAsync();

            _isDialogOpen = false;
        }
    }
}