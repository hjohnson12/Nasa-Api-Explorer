﻿using System;
using System.Collections.ObjectModel;
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
        private CancellationTokenSource cancellationTokenSource;
        private bool _isLoading;

        public ICommand LoadPhotosCommand { get; set; }

        public PerseveranceRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;

            LoadPhotosCommand =
                new AsyncRelayCommand<DateTimeOffset?>(LoadPerseveranceRoverPhotos);
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

        public async Task LoadPerseveranceRoverPhotos(DateTimeOffset? date)
        {
            IsLoading = true;
            string photosDate = FormatDateString(date);
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

        public string FormatDateString(DateTimeOffset? date)
        {
            return date.Value.Year.ToString() 
                + "-" + date.Value.Month.ToString() 
                + "-" + date.Value.Day.ToString();
        }
    }
}
