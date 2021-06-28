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
        private INasaApiService _nasaApiService;
        private ObservableCollection<MarsRoverPhoto> _perseverancePhotos;
        private MarsRover _perseveranceRover;
        private ObservableCollection<string> _roverCameras;
        private ObservableCollection<string> _roverCameras2;


        private CancellationTokenSource cancellationTokenSource;
        private bool _isLoading;
        private DateTimeOffset? _selectedDate;

        public ICommand LoadPhotosCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }

        public PerseveranceRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;

            var cameraList = MarsRoverPhotoData.PerseveranceCameras;
            _roverCameras2 = new ObservableCollection<string>(
                cameraList.Select(x => x.Item2.ToString())
                .ToList());
            _roverCameras2.Insert(0, "- Choose Camera (optional) -");

            SelectedDate = DateTimeOffset.Now.AddDays(1);

            LoadPhotosCommand =
                new AsyncRelayCommand(LoadPerseveranceRoverPhotos);
            UpdateDateCommand =
                new Base.RelayCommand<DateTimeOffset?>(UpdateSelectedDate);
        }

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

        public MarsRover PerseveranceRover { get; set; }

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
            string photosDate = FormatDateString(SelectedDate);
            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.Token.Register(() =>
                {
                    Console.WriteLine("User requested to cancel.");
                });

                PerseverancePhotos = 
                    new ObservableCollection<MarsRoverPhoto>(
                        await _nasaApiService.GetPerseveranceRoverPhotosAsync(
                            photosDate, cancellationTokenSource.Token));

                PerseveranceRover = PerseverancePhotos[0].Rover;
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

        public string FormatDateString(DateTimeOffset? date)
        {
            return date.Value.Year.ToString() 
                + "-" + date.Value.Month.ToString() 
                + "-" + date.Value.Day.ToString();
        }
    }
}