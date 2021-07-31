using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NasaDataExplorer.Views.Feeds;
using NasaDataExplorer.Views;
using Newtonsoft.Json;

namespace NasaDataExplorer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(AppTitleBar);
            navMenu.SelectedItem = navMenu.MenuItems.ElementAt(0);
        }

        /// <summary>
        /// Updates the page witin the frame with the selected item on the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void navMenu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = navMenu.SelectedItem as NavigationViewItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "APOD":
                        mainFrame.Navigate(typeof(AstronomyPictureView));
                        break;
                    case "Earth_EventTracker":
                        mainFrame.Navigate(typeof(NaturalEventTrackerView));
                        break;
                    case "PR_Photos":
                        mainFrame.Navigate(typeof(PerseveranceRoverPhotosView));
                        break;
                    case "CR_Photos":
                        mainFrame.Navigate(typeof(CuriosityRoverPhotosView));
                        break;
                    case "OR_Photos":
                        mainFrame.Navigate(typeof(OpportunityRoverPhotosView));
                        break;
                    case "InsightLander_Weather":
                        mainFrame.Navigate(typeof(InSightLanderWeatherView));
                        break;
                    case "NASAClimate_Feed":
                        mainFrame.Navigate(typeof(NasaClimateFeedView));
                        break;
                    case "NASAEarthObserv_Feed":
                        mainFrame.Navigate(typeof(NasaEarthObservatoryFeedView));
                        break;
                    default:
                        mainFrame.Navigate(typeof(AstronomyPictureView));
                        break;
                }
            }
        }
    }
}