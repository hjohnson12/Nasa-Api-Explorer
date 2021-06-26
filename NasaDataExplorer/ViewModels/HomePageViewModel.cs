using Microsoft.Toolkit.Mvvm.Input;
using NasaDataExplorer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            set
            {
                if (_astronomyPictureOfTheDay != value)
                    _astronomyPictureOfTheDay = value;
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

        public async Task LoadAstronomyPictureOfTheDayAsync()
        {
            IsLoading = true;
            try
            {
                AstronomyPictureOfTheDay = await _nasaApiService.GetAstronomyPictureOfTheDayAsync();
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
