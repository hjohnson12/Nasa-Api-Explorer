﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web.Http;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NasaDataExplorer.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using Microsoft.Extensions.Logging;
using NasaDataExplorer.Services;
using System.Threading;
using NasaDataExplorer.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CuriosityRoverPhotosView : Page
    {
        private ObservableCollection<CuriosityRover.Photo> curiosityPhotos = 
            new ObservableCollection<CuriosityRover.Photo>();

        private CancellationTokenSource cancellationTokenSource;

        public CuriosityRoverPhotosViewModel ViewModel { get; set; }

        public CuriosityRoverPhotosView()
        {
            this.InitializeComponent();

            ViewModel = 
                new CuriosityRoverPhotosViewModel(
                    ((App)Application.Current).ServiceHost.Services.GetRequiredService<INasaApiService>());

            // Mission hasn't ended so can just set a previous date
            RoverPhotosDatePicker.Date = DateTimeOffset.Now.AddDays(-1);
        }

        private async Task InitializePhotos_Curiosity(string date)
        {
            progressRing.IsActive = true;
            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.Token.Register(() =>
                {
                    Console.WriteLine("User requested to cancel.");
                });

                curiosityPhotos = 
                    new ObservableCollection<CuriosityRover.Photo>(
                        await ViewModel.LoadCuriosityRoverPhotos(date, cancellationTokenSource.Token));
                GridViewControl.ItemsSource = curiosityPhotos;
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
                await InitializePhotos_Curiosity(photoDate);
            }
        }

        private async void GridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Frame.Navigate(typeof(PhotoDetailsView), e.ClickedItem);
            CuriosityPhotoDetailsDialogView diag = new CuriosityPhotoDetailsDialogView(e.ClickedItem as CuriosityRover.Photo, curiosityPhotos);
            await diag.ShowAsync();
        }

        private void btnCancelRequest_Click(object sender, RoutedEventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                // If instance already exists, buttons been pressed already
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
            }
        }

        //private void BtnNext_Click(object sender, RoutedEventArgs e)
        //{
        //    string date = "";
        //   // RoverPhotosDatePicker.Date.Value.AddDays(1);
        //    date = RoverPhotosDatePicker.Date.Value.Year.ToString() + "-" + RoverPhotosDatePicker.Date.Value.Month.ToString() + "-" + RoverPhotosDatePicker.Date.Value.AddDays(1).Day.ToString();
        //    RoverPhotosDatePicker.Date.Value.AddDays(1);
        //    RoverPhotosDatePicker.Date = new DateTimeOffset(RoverPhotosDatePicker.Date.Value.Year, RoverPhotosDatePicker.Date.Value.Month, RoverPhotosDatePicker.Date.Value.AddDays(1).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, TimeSpan.Zero);
        //    InitializePhotos_Curiosity(date);
        //}
        //private void BtnPrev_Click(object sender, RoutedEventArgs e)
        //{
        //    string date = "";
        //   // RoverPhotosDatePicker.Date.Value.AddDays(-1);
        //    date = RoverPhotosDatePicker.Date.Value.Year.ToString() + "-" + RoverPhotosDatePicker.Date.Value.Month.ToString() + "-" + RoverPhotosDatePicker.Date.Value.AddDays(-1).Day.ToString();
        //    InitializePhotos_Curiosity(date);
        //}
    }
}