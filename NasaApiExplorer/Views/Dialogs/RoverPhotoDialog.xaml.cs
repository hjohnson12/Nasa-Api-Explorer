using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using NasaApiExplorer.Models;
using NasaApiExplorer.Services;
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
            
            DataContext = ViewModel;

            ViewModel.CurrentPhoto = currentPhoto;
            ViewModel.RoverPhotos = roverPhotos;
        }

        public RoverPhotoDialogViewModel ViewModel => (RoverPhotoDialogViewModel)DataContext;

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}