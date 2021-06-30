using NasaDataExplorer.Services;
using NasaDataExplorer.Services.Nasa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.ViewModels
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
