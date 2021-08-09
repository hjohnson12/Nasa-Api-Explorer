using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using NasaApiExplorer.Services.Nasa;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaApiExplorer.Models;

namespace NasaApiExplorer.ViewModels
{
    public class OpportunityRoverPhotosViewModel : Base.Observable
    {
        private INasaApiService _nasaApiService;
        private ObservableCollection<MarsRoverPhoto> _opportunityPhotos;
        private bool _isLoading;
        private DateTimeOffset? _selectedDate;
        private CancellationTokenSource _cancellationTokenSource;
        private DateTimeOffset? _missionEndDate;

        public ICommand LoadPhotosCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }
        public ICommand CancelRequestCommand { get; set; }

        public OpportunityRoverPhotosViewModel(INasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;

            OpportunityPhotos = new ObservableCollection<MarsRoverPhoto>();

            _missionEndDate = new DateTimeOffset(2018, 6, 10, default, default, default, default);
            SelectedDate = _missionEndDate;

            LoadPhotosCommand =
               new AsyncRelayCommand(LoadOpportunityRoverPhotos);
            UpdateDateCommand =
                new Base.RelayCommand<DateTimeOffset?>(UpdateSelectedDate);
            CancelRequestCommand =
                new Base.RelayCommand(CancelRequest);
        }

        public ObservableCollection<MarsRoverPhoto> OpportunityPhotos
        {
            get => _opportunityPhotos;
            set
            {
                SetProperty(ref _opportunityPhotos, value);
                OnPropertyChanged("IsPhotosAvailable");
            }
        }

        public bool IsPhotosAvailable
        {
            get => OpportunityPhotos.Count == 0;
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

        public DateTimeOffset? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MarsRoverPhoto> OpportunityRover { get; set; }

        public async Task LoadOpportunityRoverPhotos()
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

        public async Task<ObservableCollection<MarsRoverPhoto>> LoadOpportunityRoverPhotos(
            string date,
            CancellationToken cancellationToken)
        {
            OpportunityPhotos = new ObservableCollection<MarsRoverPhoto>(
                await _nasaApiService.MarsRoverPhotos.GetOpportunityRoverPhotosAsync(date));
            return OpportunityPhotos;
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