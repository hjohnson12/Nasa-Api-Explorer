using NasaApiExplorer.Models;
using NasaApiExplorer.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaApiExplorer.Services
{
    public class DialogService : IDialogService
    {
        public bool _isDialogOpen;

        public async Task ShowPhotoDialog(MarsRoverPhoto marsRoverPhoto, IList<MarsRoverPhoto> photos)
        {
            if (_isDialogOpen)
                return;

            _isDialogOpen = true;

            var roverPhotos = new List<MarsRoverPhoto>(photos);

            var dialog = new RoverPhotoDialogView(
                marsRoverPhoto,
                roverPhotos);

            await dialog.ShowAsync();

            _isDialogOpen = false;
        }
    }
}