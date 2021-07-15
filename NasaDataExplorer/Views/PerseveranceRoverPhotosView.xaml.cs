using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasaDataExplorer.Models;
using NasaDataExplorer.ViewModels;
using NasaDataExplorer.Views.Dialogs;

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PerseveranceRoverPhotosView : Page
    {
        private ObservableCollection<MarsRoverPhoto> perseverancePhotos =
            new ObservableCollection<MarsRoverPhoto>();
        public PerseveranceRoverPhotosViewModel ViewModel => (PerseveranceRoverPhotosViewModel)DataContext;

        public PerseveranceRoverPhotosView()
        {
            this.InitializeComponent();

            this.DataContext =
                    App.Current.ServiceHost.Services.GetService<PerseveranceRoverPhotosViewModel>();

            // Current day since mission is still active
            RoverPhotosDatePicker.MaxDate = DateTime.Today;
        }

        private async void GridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            RoverPhotoDialogView photoDialog = new RoverPhotoDialogView(
                e.ClickedItem as MarsRoverPhoto,
                ViewModel.PerseverancePhotos);

            await photoDialog.ShowAsync();
        }
    }
}