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
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using NasaDataExplorer.ViewModels;

namespace NasaDataExplorer.Views.Dialogs
{
    public sealed partial class RoverPhotoDialogView : ContentDialog
    {
        public RoverPhotoDialogView(MarsRoverPhoto currentPhoto, ObservableCollection<MarsRoverPhoto> roverPhotos)
        {
            this.InitializeComponent();

            ViewModel =
                new RoverPhotoDialogViewModel(
                    roverPhotos,
                    currentPhoto,
                    ((App)Application.Current).ServiceHost.Services.GetRequiredService<IFileDownloadService>());

            DataContext = ViewModel;
        }

        public RoverPhotoDialogViewModel ViewModel { get; set; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
