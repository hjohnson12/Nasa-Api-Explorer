using Microsoft.Toolkit.Mvvm.Input;
using NasaApiExplorer.Models;
using NasaApiExplorer.Services;
using NasaApiExplorer.Services.NasaApis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NasaApiExplorer.ViewModels
{
    public class SpiritPhotosViewModel : RoverPhotosBaseViewModel
    {
        public ICommand LoadPhotosCommand { get; set; }

        public SpiritPhotosViewModel(
            INasaApiService nasaApiService,
            IFileDownloadService fileDownloadService,
            IDialogService dialogService)
            : base(nasaApiService, fileDownloadService, dialogService)
        {
            // Populate the camera list for Spirit Rover
            var cameraList = MarsRoverPhotoData.SpiritCameras;
            _roverCameras = new ObservableCollection<string>(
                cameraList.Select(x => x.Item2.ToString()));

            SelectedDate = DateTimeOffset.Now.AddDays(1);
            RoverPhotos = new ObservableCollection<MarsRoverPhoto>();

            LoadPhotosCommand =
                new AsyncRelayCommand(LoadSpiritRoverPhotos);
        }

        /// <summary>
        /// Retrieves photos from curiosity rover using photos service.
        /// </summary>
        /// <returns></returns>
        public async Task LoadSpiritRoverPhotos()
        {
            IsLoading = true;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.Token.Register(() =>
            {
                Console.WriteLine("User requested to cancel.");
            });
            var cancellationToken = _cancellationTokenSource.Token;

            try
            {
                string photosDate = FormatDate(SelectedDate);

                RoverPhotos = new ObservableCollection<MarsRoverPhoto>(
                        await _nasaApiService.MarsRoverPhotos.GetSpiritRoverPhotosAsync(
                            photosDate, cancellationToken));
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"Operation cancelled with message {ocException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
                _cancellationTokenSource = null;
            }
        }
    }
}