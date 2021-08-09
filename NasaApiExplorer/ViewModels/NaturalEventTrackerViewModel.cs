using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NasaApiExplorer.Services.Nasa;

namespace NasaApiExplorer.ViewModels
{
    public class NaturalEventTrackerViewModel : Base.Observable
    {
        private readonly INasaApiService _nasaApiService;

        public NaturalEventTrackerViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }
    }
}