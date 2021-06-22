using Microsoft.Extensions.DependencyInjection;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
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
        private MarsRoverPhoto CurrentPhoto { get; set; }
        private ObservableCollection<MarsRoverPhoto> Photos { get; set; }

        private IDownloaderService downloaderService;

        public RoverPhotoDialogView(MarsRoverPhoto currentPhoto,ObservableCollection<MarsRoverPhoto> curiosityPhotos)
        {
            this.InitializeComponent();
            CurrentPhoto = currentPhoto;
            Photos = curiosityPhotos;

            downloaderService = ((App)Application.Current).ServiceHost.Services.GetRequiredService<IDownloaderService>();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            downloaderService.DownloadFile(CurrentPhoto.Img_src, @"C:\users\hlj51\desktop\");
        }
    }
}
