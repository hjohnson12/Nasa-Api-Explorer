using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using NasaApiExplorer.Services.NasaApis;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaApiExplorer.Models;
using NasaApiExplorer.Services;

namespace NasaApiExplorer.ViewModels
{
    public class OpportunityRoverPhotosViewModel : RoverPhotosBaseViewModel
    {
        private CancellationTokenSource _cancellationTokenSource;
        private DateTimeOffset? _missionEndDate;

        public ICommand LoadPhotosCommand { get; set; }

        public OpportunityRoverPhotosViewModel(
            INasaApiService nasaApiService,
            IFileDownloadService fileDownloadService,
            IDialogService dialogService)
            : base(nasaApiService, fileDownloadService, dialogService)
        {
            RoverPhotos = new ObservableCollection<MarsRoverPhoto>();

            _missionEndDate = new DateTimeOffset(2018, 6, 10, default, default, default, default);
            SelectedDate = _missionEndDate;

            LoadPhotosCommand =
               new AsyncRelayCommand(LoadOpportunityRoverPhotos);
        }

        public MarsRover OpportunityRover { get; set; }

        /// <summary>
        /// Retrieves photos from opportunity rover using photos service.
        /// </summary>
        /// <returns></returns>
        public async Task LoadOpportunityRoverPhotos()
        {
            IsLoading = true;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.Token.Register(() =>
            {
                Console.WriteLine("User requested to cancel.");
            });

            string photosDate = FormatDate(SelectedDate);
            try
            {
                RoverPhotos = new ObservableCollection<MarsRoverPhoto>(
                    await _nasaApiService.MarsRoverPhotos.GetOpportunityRoverPhotosAsync(photosDate));
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
    }
}