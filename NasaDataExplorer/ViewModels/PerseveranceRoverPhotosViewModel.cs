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

namespace NasaDataExplorer.ViewModels
{
    public class PerseveranceRoverPhotosViewModel : Base.Observable
    {
        private const string DEFAULT_COMBO_OPTION = "- Choose Camera (optional) -";
        private INasaApiService _nasaApiService;
        private ObservableCollection<MarsRoverPhoto> _perseverancePhotos;
        private MarsRover _perseveranceRover;
        private ObservableCollection<string> _roverCameras;
        private ObservableCollection<string> _roverCameras2;
        private CancellationTokenSource cancellationTokenSource;
        private bool _isLoading;
        private DateTimeOffset? _selectedDate;
        private string _selectedCamera;

        public ICommand LoadPhotosCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }
        public ICommand UpdateSelectedCameraCommand { get; set; }

        public PerseveranceRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;

            var cameraList = MarsRoverPhotoData.PerseveranceCameras;
            _roverCameras2 = new ObservableCollection<string>(
                cameraList.Select(x => x.Item2.ToString())
                .ToList());
            _roverCameras2.Insert(0, DEFAULT_COMBO_OPTION);

            SelectedDate = DateTimeOffset.Now.AddDays(1);

            LoadPhotosCommand =
                new AsyncRelayCommand(LoadPerseveranceRoverPhotos);
            UpdateDateCommand =
                new Base.RelayCommand<DateTimeOffset?>(UpdateSelectedDate);
            UpdateSelectedCameraCommand =
                new Base.RelayCommand<string>(UpdateSelectedCamera);
        }

        public MarsRover PerseveranceRover { get; set; }

        public ObservableCollection<MarsRoverPhoto> PerseverancePhotos
        {
            get => _perseverancePhotos;
            set
            {
                if (_perseverancePhotos != value)
                    _perseverancePhotos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> RoverCameras
        {
            get => _roverCameras;
            set
            {
                if (_roverCameras != value)
                    _roverCameras = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> RoverCameras2
        {
            get => _roverCameras2;
            set
            {
                if (_roverCameras2 != value)
                    _roverCameras2 = value;
                OnPropertyChanged();
            }
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

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                    _isLoading = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadPerseveranceRoverPhotos()
        {
            IsLoading = true;
            cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Token.Register(() =>
            {
                Console.WriteLine("User requested to cancel.");
            });

            string photosDate = FormatDateString(SelectedDate);
            try
            {
                if (isCameraSelected(SelectedCamera))
                {
                    var camera = MarsRoverPhotoData.PerseveranceCameras
                        .Single(x => x.Item2.Equals(SelectedCamera))
                        .Item1;

                    PerseverancePhotos =
                        new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.GetPerseveranceRoverPhotosAsync(
                                photosDate, camera, cancellationTokenSource.Token));
                }
                else
                {
                    PerseverancePhotos =
                        new ObservableCollection<MarsRoverPhoto>(
                            await _nasaApiService.GetPerseveranceRoverPhotosAsync(
                                photosDate, cancellationTokenSource.Token));
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
                cancellationTokenSource = null;
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

        public bool isCameraSelected(string selection)
        {
            return !selection.Equals(DEFAULT_COMBO_OPTION);
        }

        public string FormatDateString(DateTimeOffset? date)
        {
            return date.Value.Year.ToString() 
                + "-" + date.Value.Month.ToString() 
                + "-" + date.Value.Day.ToString();
        }
    }
}