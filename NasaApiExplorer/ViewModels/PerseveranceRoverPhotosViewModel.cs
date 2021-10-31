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
    public class PerseveranceRoverPhotosViewModel : RoverPhotosBaseViewModel
    {
        private const string DEFAULT_COMBO_OPTION = "- Choose Camera (optional) -";
        private MarsRover _perseveranceRover;

        public ICommand LoadPhotosCommand { get; set; }

        public PerseveranceRoverPhotosViewModel(
            INasaApiService nasaApiService,
            IFileDownloadService fileDownloadService,
            IDialogService dialogService)
            : base(nasaApiService, fileDownloadService, dialogService)
        {
            var cameraList = MarsRoverPhotoData.PerseveranceCameras;
            _roverCameras2 = new ObservableCollection<string>(
                cameraList.Select(x => x.Item2.ToString())
                .ToList());
            //_roverCameras2.Insert(0, DEFAULT_COMBO_OPTION);

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
            var cancellationToken = _cancellationTokenSource.Token;

            try
            {
                string photosDate = FormatDate(SelectedDate);

                if (IsCameraSelected(SelectedCamera))
                {
                    var camera = MarsRoverPhotoData.PerseveranceCameras
                        .Single(x => x.Item2.Equals(SelectedCamera))
                        .Item1;

                    RoverPhotos =
                        new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.MarsRoverPhotos.GetPerseveranceRoverPhotosAsync(
                                photosDate, camera, cancellationToken));
                }
                else
                {
                    RoverPhotos =
                        new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.MarsRoverPhotos.GetPerseveranceRoverPhotosAsync(
                                photosDate, cancellationToken));
                }

                //PerseveranceRover = PerseverancePhotos[0].Rover;
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"Operation cancelled with message {ocException.Message}");
            }
            catch (Exception ex)
            {
                //var logger = ((App)Application.Current).ServiceHost.Services.GetRequiredService<ILogger<App>>();
                //logger.LogError(ex, "An error occurred.");
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