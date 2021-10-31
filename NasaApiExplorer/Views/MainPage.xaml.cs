﻿using System;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace NasaApiExplorer.Views
{
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(AppTitleBar);
            navMenu.SelectedItem = navMenu.MenuItems.ElementAt(0);
        }

        /// <summary>
        /// Updates the frame with the selected view.
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
                        navMenu.Header = "Perseverance Rover Photos";
                        break;
                    case "CR_Photos":
                        mainFrame.Navigate(typeof(CuriosityRoverPhotosView));
                        navMenu.Header = "Curiosity Rover Photos";
                        break;
                    case "OR_Photos":
                        mainFrame.Navigate(typeof(OpportunityRoverPhotosView));
                        navMenu.Header = "Opportunity Rover Photos"; 
                        break;
                    case "InsightLander_Weather":
                        mainFrame.Navigate(typeof(InSightLanderWeatherView));
                        break;
                    default:
                        mainFrame.Navigate(typeof(AstronomyPictureView));
                        break;
                }
            }
        }
    }
}