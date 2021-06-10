using NasaDataExplorer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.ViewModels
{
    public class HomePageViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private AstronomyPictureOfTheDay _astronomyPictureOfTheDay;

        public HomePageViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
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

        public async Task<AstronomyPictureOfTheDay> LoadAstronomyPictureOfTheDay()
        {
            try
            {
                AstronomyPictureOfTheDay = await _nasaApiService.GetAstronomyPictureOfTheDayAsync();
                return AstronomyPictureOfTheDay;
            }
            catch (Exception ex)
            {
                return AstronomyPictureOfTheDay;
            }
        }
    }
}
