using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using NasaDataExplorer.ViewModels;
using NasaDataExplorer.Models;
using NasaDataExplorer.Views.Dialogs;

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CuriosityRoverPhotosView : Page
    {
        public CuriosityRoverPhotosViewModel ViewModel => (CuriosityRoverPhotosViewModel)DataContext;

        public CuriosityRoverPhotosView()
        {
            this.InitializeComponent();

            this.DataContext =
                App.Current.ServiceHost.Services.GetRequiredService<CuriosityRoverPhotosViewModel>();
            
            // Mission hasn't ended so can just set a previous date
            RoverPhotosDatePicker.MaxDate = DateTime.Today;
        }

        private async void GridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Display photo in dialog on selection
            RoverPhotoDialogView photoDialog = new RoverPhotoDialogView(
                e.ClickedItem as MarsRoverPhoto,
                ViewModel.CuriosityPhotos);

            await photoDialog.ShowAsync();
        }
    }
}