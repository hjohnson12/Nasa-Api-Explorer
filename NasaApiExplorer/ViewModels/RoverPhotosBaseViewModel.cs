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
    public abstract class RoverPhotosBaseViewModel : Base.Observable
    {
        protected readonly INasaApiService _nasaApiService;
        protected readonly IFileDownloadService _fileDownloadService;
        protected readonly IDialogService _dialogService;
        protected ObservableCollection<MarsRoverPhoto> _roverPhotos;
        protected ObservableCollection<string> _roverCameras;
        protected ObservableCollection<string> _roverCameras2;
        protected bool _isLoading;
        protected DateTimeOffset? _selectedDate;
        protected string _selectedCamera;
        protected CancellationTokenSource _cancellationTokenSource;

        public ICommand DownloadPhotosCommand { get; set; }
        public ICommand SelectPhotoCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }
        public ICommand UpdateSelectedCameraCommand { get; set; }
        public ICommand CancelRequestCommand { get; set; }

        protected RoverPhotosBaseViewModel(
            INasaApiService nasaApiService,
            IFileDownloadService fileDownloadService,
            IDialogService dialogService)
        {
            _nasaApiService = nasaApiService;
            _fileDownloadService = fileDownloadService;
            _dialogService = dialogService;

            UpdateDateCommand =
               new Base.RelayCommand<DateTimeOffset?>(UpdateSelectedDate);
            SelectPhotoCommand =
                new AsyncRelayCommand<MarsRoverPhoto>(DisplayPhoto);
            UpdateSelectedCameraCommand =
                new Base.RelayCommand<string>(UpdateSelectedCamera);
            DownloadPhotosCommand =
                new AsyncRelayCommand(DownloadPhotos);
            CancelRequestCommand =
               new Base.RelayCommand(CancelRequest);
        }

        public ObservableCollection<MarsRoverPhoto> RoverPhotos
        {
            get => _roverPhotos;
            set
            {
                SetProperty(ref _roverPhotos, value);
                OnPropertyChanged("IsPhotosAvailable");
            }
        }

        public bool IsPhotosAvailable => RoverPhotos.Count == 0;

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
        public bool IsCameraSelected(string selection) => !string.IsNullOrEmpty(selection);

        /// <summary>
        /// Downloads rover photos for the selected date in a location chosen 
        /// through a folder picker.
        /// </summary>
        /// <returns></returns>
        public async Task DownloadPhotos()
        {
            string[] urls = RoverPhotos.Select(x => x.ImageSourceUrl).ToArray();

            try
            {
                await _fileDownloadService.DownloadFiles(urls);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CancelRequest()
        {
            if (_cancellationTokenSource != null)
            {
                // If instance already exists, buttons been pressed already
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
            }
        }

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
        /// Displays the selected photo in a dialog.
        /// </summary>
        /// <param name="roverPhoto"></param>
        /// <returns></returns>
        public async Task DisplayPhoto(MarsRoverPhoto roverPhoto)
        {
            await _dialogService.ShowPhotoDialog(roverPhoto, RoverPhotos.ToList());
        }
    }
}