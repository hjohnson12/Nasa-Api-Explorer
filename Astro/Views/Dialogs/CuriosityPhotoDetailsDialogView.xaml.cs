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

namespace Astro.Views.Dialogs
{
    public sealed partial class CuriosityPhotoDetailsDialogView : ContentDialog
    {
        private CuriosityRover.Photo CurrentPhoto { get; set; }
        private ObservableCollection<CuriosityRover.Photo> Photos { get; set; }

        public CuriosityPhotoDetailsDialogView(CuriosityRover.Photo currentPhoto,ObservableCollection<CuriosityRover.Photo> curiosityPhotos)
        {
            this.InitializeComponent();
            CurrentPhoto = currentPhoto;
            Photos = curiosityPhotos;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
