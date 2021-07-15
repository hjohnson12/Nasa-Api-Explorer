using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services.Nasa;

namespace NasaDataExplorer.ViewModels
{
    public class CuriosityRoverPhotosViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private ObservableCollection<MarsRoverPhoto> _curiosityPhotos;
        private bool _isLoading;
        private DateTimeOffset? _selectedDate;
        private CancellationTokenSource _cancellationTokenSource;

        public ICommand LoadPhotosCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }
        public ICommand CancelRequestCommand { get; set; }

        public CuriosityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;

            SelectedDate = DateTimeOffset.Now.AddDays(1);
            CuriosityPhotos = new ObservableCollection<MarsRoverPhoto>();

            LoadPhotosCommand =
               new AsyncRelayCommand(LoadCuriosityRoverPhotos);
            UpdateDateCommand =
                new Base.RelayCommand<DateTimeOffset?>(UpdateSelectedDate);
            CancelRequestCommand =
                new Base.RelayCommand(CancelRequest);
        }

        public ObservableCollection<MarsRoverPhoto> CuriosityPhotos
        {
            get { return _curiosityPhotos; }
            set
            {
                SetProperty(ref _curiosityPhotos, value);
                OnPropertyChanged("IsPhotosAvailable");
            }
        }

        public bool IsPhotosAvailable
        {
            get => CuriosityPhotos.Count == 0;
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
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

        public ObservableCollection<MarsRoverPhoto> CuriosityRover { get; set; }

        public async Task LoadCuriosityRoverPhotos()
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
                CuriosityPhotos = new ObservableCollection<MarsRoverPhoto>(
                        await _nasaApiService.MarsRoverPhotos.GetCuriosityRoverPhotosAsync(photosDate));
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

        public void CancelRequest()
        {
            if (_cancellationTokenSource != null)
            {
                // If instance already exists, buttons been pressed already
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
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