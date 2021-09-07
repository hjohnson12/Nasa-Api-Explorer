using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using NasaApiExplorer.Models;
using NasaApiExplorer.Services;
using NasaApiExplorer.Services.Nasa;

namespace NasaApiExplorer.ViewModels
{
    public class CuriosityRoverPhotosViewModel : Base.Observable
    {
        private readonly INasaApiService _nasaApiService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<MarsRoverPhoto> _curiosityPhotos;
        private bool _isLoading;
        private DateTimeOffset? _selectedDate;
        private CancellationTokenSource _cancellationTokenSource;

        public ICommand LoadPhotosCommand { get; set; }
        public ICommand SelectPhotoCommand { get; set; }
        public ICommand UpdateDateCommand { get; set; }
        public ICommand CancelRequestCommand { get; set; }

        public CuriosityRoverPhotosViewModel(
            INasaApiService nasaApiService,
            IDialogService dialogService)
        {
            _nasaApiService = nasaApiService;
            _dialogService = dialogService;

            SelectedDate = DateTimeOffset.Now.AddDays(1);
            CuriosityPhotos = new ObservableCollection<MarsRoverPhoto>();

            LoadPhotosCommand =
               new AsyncRelayCommand(LoadCuriosityRoverPhotos);
            SelectPhotoCommand =
                new AsyncRelayCommand<MarsRoverPhoto>(DisplayPhoto);
            UpdateDateCommand =
                new Base.RelayCommand<DateTimeOffset?>(UpdateSelectedDate);
            CancelRequestCommand =
                new Base.RelayCommand(CancelRequest);
        }

        public MarsRover CuriosityRover { get; set; }

        public ObservableCollection<MarsRoverPhoto> CuriosityPhotos
        {
            get { return _curiosityPhotos; }
            set
            {
                SetProperty(ref _curiosityPhotos, value);
                OnPropertyChanged("IsPhotosAvailable");
            }
        }

        /// <summary>
        /// Determines if there are photos loaded.
        /// </summary>
        public bool IsPhotosAvailable => CuriosityPhotos.Count == 0;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
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
        /// Retrieves photos from curiosity rover using photos service.
        /// </summary>
        /// <returns></returns>
        public async Task LoadCuriosityRoverPhotos()
        {
            IsLoading = true;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.Token.Register(() =>
            {
                Console.WriteLine("User requested to cancel.");
            });
            var cancellationToken = _cancellationTokenSource.Token;

            try
            {
                string photosDate = FormatDate(SelectedDate);

                CuriosityPhotos = new ObservableCollection<MarsRoverPhoto>(
                        await _nasaApiService.MarsRoverPhotos.GetCuriosityRoverPhotosAsync(photosDate));
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
            await _dialogService.ShowPhotoDialog(roverPhoto, CuriosityPhotos.ToList());
        }
    }
}