using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasaDataExplorer.Models;
using NasaDataExplorer.Services;
using NasaDataExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PerseveranceRoverPhotosView : Page
    {
        private ObservableCollection<MarsRoverPhoto> perseverancePhotos =
            new ObservableCollection<MarsRoverPhoto>();
        private CancellationTokenSource cancellationTokenSource;
        public PerseveranceRoverPhotosViewModel ViewModel { get; set; }

        public PerseveranceRoverPhotosView()
        {
            this.InitializeComponent();
            ViewModel =
                new PerseveranceRoverPhotosViewModel(
                    ((App)Application.Current).ServiceHost.Services.GetRequiredService<INasaApiService>());
            RoverPhotosDatePicker.Date = DateTimeOffset.Now.AddDays(-1);
        }

        private async Task InitializePhotos_Perseverance(string date)
        {
            progressRing.IsActive = true;
            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.Token.Register(() =>
                {
                    Console.WriteLine("User requested to cancel.");
                });

                perseverancePhotos =
                   new ObservableCollection<MarsRoverPhoto>(
                        await ViewModel.LoadPerseveranceRoverPhotos(date, cancellationTokenSource.Token));
            }
            catch (Exception ex)
            {
                var logger = ((App)Application.Current).ServiceHost.Services.GetRequiredService<ILogger<App>>();
                logger.LogError(ex, "An error occurred.");
            }
            finally
            {
                progressRing.IsActive = false;
                cancellationTokenSource = null;
            }
        }

        private async void RoverPhotosDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var calendarName = sender.Name.ToString();
            if (string.IsNullOrEmpty(args.NewDate.ToString()))
                return;
            else
            {
                var datePicked = args.NewDate;
                var photoDate = datePicked.Value.Year.ToString() + "-" + datePicked.Value.Month.ToString() + "-" + datePicked.Value.Day.ToString();
                await InitializePhotos_Perseverance(photoDate);
            }
        }
    }
}
