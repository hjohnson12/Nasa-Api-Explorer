﻿using Astro.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Astro.Views.Feeds;

namespace Astro.Views
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

        private void navMenu_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = navMenu.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "HomePage":
                        mainFrame.Navigate(typeof(HomePageView));
                        break;
                    case "Earth_EventTracker":
                        mainFrame.Navigate(typeof(NaturalEventTrackerView));
                        break;
                    case "Earth_EarthNow":
                        mainFrame.Navigate(typeof(EarthClimateView));
                        break;
                    case "CR_Photos":
                        mainFrame.Navigate(typeof(CuriosityRoverPhotosView));
                        break;
                    case "InsightLander_Weather":
                        mainFrame.Navigate(typeof(InSightLanderWeatherView));
                        break;
                    case "NASAClimate_Feed":
                        mainFrame.Navigate(typeof(NasaClimateFeedView));
                        break;
                }
            }
        }
    }
}
