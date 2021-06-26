using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using NasaDataExplorer.ViewModels;
using NasaDataExplorer.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PerseveranceRoverPhotosView : Page
    {
        private ObservableCollection<MarsRoverPhoto> perseverancePhotos =
            new ObservableCollection<MarsRoverPhoto>();
        private CancellationTokenSource cancellationTokenSource;
        public PerseveranceRoverPhotosViewModel ViewModel { get; set; }

        public PerseveranceRoverPhotosView()
        {
            this.InitializeComponent();
            ViewModel =
                new PerseveranceRoverPhotosViewModel(
                    ((App)Application.Current).ServiceHost.Services.GetRequiredService<INasaApiService>());

            RoverPhotosDatePicker.Date = DateTimeOffset.Now.AddDays(-1);

            // Current day since mission is still active
            RoverPhotosDatePicker.MaxDate = DateTime.Today;
        }

        private async void GridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            RoverPhotoDialogView photoDialog = new RoverPhotoDialogView(
                e.ClickedItem as MarsRoverPhoto,
                perseverancePhotos);

            await photoDialog.ShowAsync();
        }
    }
}
