using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using NasaApiExplorer.Models;
using NasaApiExplorer.ViewModels;

namespace NasaApiExplorer.Views.Dialogs
{
    public sealed partial class RoverPhotoDialog : ContentDialog
    {
        public RoverPhotoDialog(MarsRoverPhoto currentPhoto, List<MarsRoverPhoto> roverPhotos)
        {
            this.InitializeComponent();

            this.DataContext =
                App.Current.ServiceHost.Services.GetService<RoverPhotoDialogViewModel>();

            ViewModel.CurrentPhoto = currentPhoto;
            ViewModel.RoverPhotos = roverPhotos;
        }

        public RoverPhotoDialogViewModel ViewModel => (RoverPhotoDialogViewModel)DataContext;

        private void btnCloseDialog_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}