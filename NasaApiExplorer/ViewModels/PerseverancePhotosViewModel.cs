using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaApiExplorer.Models;
using NasaApiExplorer.Services;
using NasaApiExplorer.Services.NasaApis;

namespace NasaApiExplorer.ViewModels
{
    public class PerseverancePhotosViewModel : RoverPhotosBaseViewModel
    {
        private const string DEFAULT_COMBO_OPTION = "- Choose Camera (optional) -";
        private MarsRover _perseveranceRover;

        public ICommand LoadPhotosCommand { get; set; }

        public PerseverancePhotosViewModel(
            INasaApiService nasaApiService,
            IFileDownloadService fileDownloadService,
            IDialogService dialogService)
            : base(nasaApiService, fileDownloadService, dialogService)
        {
            // Populate the camera list for Perseverance Rover
            var cameraList = MarsRoverPhotoData.PerseveranceCameras;
            _roverCameras = new ObservableCollection<string>(
                cameraList.Select(x => x.Item2.ToString())
                .ToList());
            //_roverCameras.Insert(0, DEFAULT_COMBO_OPTION);

            SelectedDate = DateTimeOffset.Now.AddDays(1);
            RoverPhotos = new ObservableCollection<MarsRoverPhoto>();

            LoadPhotosCommand =
                new AsyncRelayCommand(LoadPerseveranceRoverPhotos);
        }

        public MarsRover PerseveranceRover { get; set; }

        /// <summary>
        /// Retrieves photos from perseverance rover using photos service.
        /// </summary>
        /// <returns></returns>
        public async Task LoadPerseveranceRoverPhotos()
        {
            IsLoading = true;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.Token.Register(() =>
            {
                Console.WriteLine("User requested to cancel.");
            });
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            try
            {
                string photosDate = FormatDate(SelectedDate);

                // Pass camera parameter to api call if a camera is selected
                if (IsCameraSelected(SelectedCamera))
                {
                    var camera = MarsRoverPhotoData.PerseveranceCameras
                        .Single(x => x.Item2.Equals(SelectedCamera))
                        .Item1;

                    RoverPhotos = new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.MarsRoverPhotos.GetPerseveranceRoverPhotosAsync(
                                photosDate, camera, cancellationToken));
                }
                else // No camera selected
                {
                    RoverPhotos = new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.MarsRoverPhotos.GetPerseveranceRoverPhotosAsync(
                                photosDate, cancellationToken));
                }
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