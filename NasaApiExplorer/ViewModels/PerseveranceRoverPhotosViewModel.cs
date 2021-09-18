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
    public class PerseveranceRoverPhotosViewModel : Base.Observable
    {
        private readonly INasaApiService _nasaApiService;
        private readonly IFileDownloadService _fileDownloadService;
        private readonly IDialogService _dialogService;
        private const string DEFAULT_COMBO_OPTION = "- Choose Camera (optional) -";
        private MarsRover _perseveranceRover;
        private ObservableCollection<MarsRoverPhoto> _perseverancePhotos;
        private ObservableCollection<string> _roverCameras;
        private ObservableCollection<string> _roverCameras2;
        private bool _isLoading;
        private DateTimeOffset? _selectedDate;
        private string _selectedCamera;
        private CancellationTokenSource _cancellationTokenSource;

        public ICommand LoadPhotosCommand { get; set; }
        public ICommand DownloadPhotosCommand { get; set; }
        public ICommand SelectPhotoCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }
        public ICommand UpdateSelectedCameraCommand { get; set; }

        public PerseveranceRoverPhotosViewModel(
            INasaApiService nasaApiService,
            IFileDownloadService fileDownloadService,
            IDialogService dialogService)
        {
            _nasaApiService = nasaApiService;
            _fileDownloadService = fileDownloadService;
            _dialogService = dialogService;

            var cameraList = MarsRoverPhotoData.PerseveranceCameras;
            _roverCameras2 = new ObservableCollection<string>(
                cameraList.Select(x => x.Item2.ToString())
                .ToList());
            //_roverCameras2.Insert(0, DEFAULT_COMBO_OPTION);

            SelectedDate = DateTimeOffset.Now.AddDays(1);
            PerseverancePhotos = new ObservableCollection<MarsRoverPhoto>();

            LoadPhotosCommand =
                new AsyncRelayCommand(LoadPerseveranceRoverPhotos);
            UpdateDateCommand =
                new Base.RelayCommand<DateTimeOffset?>(UpdateSelectedDate);
            SelectPhotoCommand =
                new AsyncRelayCommand<MarsRoverPhoto>(DisplayPhoto);
            UpdateSelectedCameraCommand =
                new Base.RelayCommand<string>(UpdateSelectedCamera);
            DownloadPhotosCommand =
                new AsyncRelayCommand(DownloadPhotos);
        }

        public MarsRover PerseveranceRover { get; set; }

        public ObservableCollection<MarsRoverPhoto> PerseverancePhotos
        {
            get => _perseverancePhotos;
            set
            {
                SetProperty(ref _perseverancePhotos, value);
                OnPropertyChanged("IsPhotosAvailable");
            }
        }

        /// <summary>
        /// Determines if there are photos loaded.
        /// </summary>
        public bool IsPhotosAvailable => PerseverancePhotos.Count == 0;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ObservableCollection<string> RoverCameras
        {
            get => _roverCameras;
            set => SetProperty(ref _roverCameras, value);
        }

        public ObservableCollection<string> RoverCameras2
        {
            get => _roverCameras2;
            set => SetProperty(ref _roverCameras2, value);
        }

        public DateTimeOffset? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCamera
        {
            get => _selectedCamera;
            set
            {
                _selectedCamera = value;
                OnPropertyChanged();
            }
        }

        public void UpdateSelectedDate(DateTimeOffset? date) => SelectedDate = date;

        public void UpdateSelectedCamera(string camera) => SelectedCamera = camera;

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

                    PerseverancePhotos =
                        new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.MarsRoverPhotos.GetPerseveranceRoverPhotosAsync(
                                photosDate, camera, cancellationToken));
                }
                else
                {
                    PerseverancePhotos =
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

        public bool IsCameraSelected(string selection) => !string.IsNullOrEmpty(selection);

        /// <summary>
        /// Formats the given date accordingly to match api request pattern.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string FormatDate(DateTimeOffset? date)
        {
            return date.Value.Year.ToString()
                + "-" + date.Value.Month.ToString()
                + "-" + date.Value.Day.ToString();
        }

        /// <summary>
        /// Downloads rover photos for the selected date in a location chosen 
        /// through a folder picker.
        /// </summary>
        /// <returns></returns>
        public async Task DownloadPhotos()
        {
            string[] urls = PerseverancePhotos.Select(x => x.ImageSourceUrl).ToArray();

            try
            {
                await _fileDownloadService.DownloadFiles(urls);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Displays the selected photo in a dialog.
        /// </summary>
        /// <param name="roverPhoto"></param>
        /// <returns></returns>
        public async Task DisplayPhoto(MarsRoverPhoto roverPhoto)
        {
            await _dialogService.ShowPhotoDialog(roverPhoto, PerseverancePhotos.ToList());
        }
    }
}