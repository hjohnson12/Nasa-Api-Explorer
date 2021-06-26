using Microsoft.Toolkit.Mvvm.Input;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace NasaDataExplorer.ViewModels
{
    public class PerseveranceRoverPhotosViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private ObservableCollection<MarsRoverPhoto> _perseverancePhotos;
        private MarsRover _perseveranceRover;
        private bool _isLoading;

        public ICommand LoadPhotosCommand { get; set; }

        public PerseveranceRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;

            LoadPhotosCommand =
                new AsyncRelayCommand<CalendarDatePickerDateChangedEventArgs>(ChangeDate);
        }

        public async Task ChangeDate(CalendarDatePickerDateChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewDate.ToString()))
                return;
            else
            {
                var datePicked = args.NewDate;
                var photoDate = datePicked.Value.Year.ToString() + "-" + datePicked.Value.Month.ToString() + "-" + datePicked.Value.Day.ToString();
                await LoadPerseveranceRoverPhotos(photoDate);
            }
        }

        private CancellationTokenSource cancellationTokenSource;

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

        public async Task LoadPerseveranceRoverPhotos(string date)
        {
            IsLoading = true;
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
                            date, cancellationTokenSource.Token));

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
    }
}
