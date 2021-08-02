using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using NasaDataExplorer.Services.Nasa;

namespace NasaDataExplorer.ViewModels
{
    public class PerseveranceRoverPhotosViewModel : Base.Observable
    {
        private const string DEFAULT_COMBO_OPTION = "- Choose Camera (optional) -";
        private INasaApiService _nasaApiService;
        private IFileDownloadService _fileDownloadService;
        private ObservableCollection<MarsRoverPhoto> _perseverancePhotos;
        private MarsRover _perseveranceRover;
        private ObservableCollection<string> _roverCameras;
        private ObservableCollection<string> _roverCameras2;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isLoading;
        private DateTimeOffset? _selectedDate;
        private string _selectedCamera;

        public ICommand LoadPhotosCommand { get; set; }
        public ICommand DownloadPhotosCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }
        public ICommand UpdateSelectedCameraCommand { get; set; }

        public PerseveranceRoverPhotosViewModel(INasaApiService nasaApiService, IFileDownloadService fileDownloadService)
        {
            _nasaApiService = nasaApiService;
            _fileDownloadService = fileDownloadService;

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

        public bool IsPhotosAvailable
        {
            get => PerseverancePhotos.Count == 0;
        }

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

        public async Task LoadPerseveranceRoverPhotos()
        {
            IsLoading = true;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.Token.Register(() =>
            {
                Console.WriteLine("User requested to cancel.");
            });

            string photosDate = FormatDateString(SelectedDate);
            try
            {
                if (IsCameraSelected(SelectedCamera))
                {
                    var camera = MarsRoverPhotoData.PerseveranceCameras
                        .Single(x => x.Item2.Equals(SelectedCamera))
                        .Item1;

                    PerseverancePhotos =
                        new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.MarsRoverPhotos.GetPerseveranceRoverPhotosAsync(
                                photosDate, camera, _cancellationTokenSource.Token));
                }
                else
                {
                    PerseverancePhotos =
                        new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.MarsRoverPhotos.GetPerseveranceRoverPhotosAsync(
                                photosDate, _cancellationTokenSource.Token));
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

        public void UpdateSelectedDate(DateTimeOffset? date)
        {
            SelectedDate = date;
        }

        public void UpdateSelectedCamera(string camera)
        {
            SelectedCamera = camera;
        }

        public bool IsCameraSelected(string selection)
        {
            return !string.IsNullOrEmpty(selection);
        }

        public string FormatDateString(DateTimeOffset? date)
        {
            return date.Value.Year.ToString() 
                + "-" + date.Value.Month.ToString() 
                + "-" + date.Value.Day.ToString();
        }
    }
}