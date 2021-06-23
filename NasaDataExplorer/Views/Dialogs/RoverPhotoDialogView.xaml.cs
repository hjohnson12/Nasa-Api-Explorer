using Microsoft.Extensions.DependencyInjection;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using NasaDataExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NasaDataExplorer.Views.Dialogs
{
    public sealed partial class RoverPhotoDialogView : ContentDialog
    {
        public RoverPhotoDialogView(MarsRoverPhoto currentPhoto,ObservableCollection<MarsRoverPhoto> roverPhotos)
        {
            this.InitializeComponent();

            ViewModel =
                new RoverPhotoDialogViewModel(
                    ((App)Application.Current).ServiceHost.Services.GetRequiredService<IDownloaderService>(),
                    roverPhotos,
                    currentPhoto);

            flipView.SelectedItem = currentPhoto;
            DataContext = ViewModel;
        }

        public RoverPhotoDialogViewModel ViewModel { get; set; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ViewModel.DownloadImageAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                ViewModel.ChangeSelection(e.AddedItems[0] as MarsRoverPhoto);
        }
    }
}
