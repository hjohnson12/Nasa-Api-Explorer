using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaDataExplorer.Services;
using NasaDataExplorer.Services.Nasa;

namespace NasaDataExplorer.ViewModels
{
    public class HomePageViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private AstronomyPictureOfTheDay _astronomyPictureOfTheDay;
        private bool _isLoading;

        public ICommand LoadApodCommand { get; set; }

        public HomePageViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;

            LoadApodCommand =
                new AsyncRelayCommand(LoadAstronomyPictureOfTheDayAsync, () => true);
        }

        public AstronomyPictureOfTheDay AstronomyPictureOfTheDay
        {
            get => _astronomyPictureOfTheDay;
            set => SetProperty(ref _astronomyPictureOfTheDay, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public async Task LoadAstronomyPictureOfTheDayAsync()
        {
            IsLoading = true;
            try
            {
                AstronomyPictureOfTheDay = await _nasaApiService.Apod.GetAstronomyPictureOfTheDayAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}