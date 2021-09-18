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
    public class OpportunityRoverPhotosViewModel : Base.Observable
    {
        private readonly INasaApiService _nasaApiService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<MarsRoverPhoto> _opportunityPhotos;
        private bool _isLoading;
        private DateTimeOffset? _selectedDate;
        private CancellationTokenSource _cancellationTokenSource;
        private DateTimeOffset? _missionEndDate;

        public ICommand LoadPhotosCommand { get; set; }
        public ICommand SelectPhotoCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }
        public ICommand CancelRequestCommand { get; set; }

        public OpportunityRoverPhotosViewModel(
            INasaApiService nasaApiService,
            IDialogService dialogService)
        {
            _nasaApiService = nasaApiService;
            _dialogService = dialogService;

            OpportunityPhotos = new ObservableCollection<MarsRoverPhoto>();

            _missionEndDate = new DateTimeOffset(2018, 6, 10, default, default, default, default);
            SelectedDate = _missionEndDate;

            LoadPhotosCommand =
               new AsyncRelayCommand(LoadOpportunityRoverPhotos);
            SelectPhotoCommand =
                new AsyncRelayCommand<MarsRoverPhoto>(DisplayPhoto);
            UpdateDateCommand =
                new Base.RelayCommand<DateTimeOffset?>(UpdateSelectedDate);
            CancelRequestCommand =
                new Base.RelayCommand(CancelRequest);
        }

        public MarsRover OpportunityRover { get; set; }

        public ObservableCollection<MarsRoverPhoto> OpportunityPhotos
        {
            get => _opportunityPhotos;
            set
            {
                SetProperty(ref _opportunityPhotos, value);
                OnPropertyChanged("IsPhotosAvailable");
            }
        }

        public bool IsPhotosAvailable => OpportunityPhotos.Count == 0;

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

        public DateTimeOffset? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public void UpdateSelectedDate(DateTimeOffset? date) => SelectedDate = date;

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
                OpportunityPhotos = new ObservableCollection<MarsRoverPhoto>(
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
        /// Displays the selected photo in a dialog.
        /// </summary>
        /// <param name="roverPhoto"></param>
        /// <returns></returns>
        public async Task DisplayPhoto(MarsRoverPhoto roverPhoto)
        {
            await _dialogService.ShowPhotoDialog(roverPhoto, OpportunityPhotos.ToList());
        }
    }
}