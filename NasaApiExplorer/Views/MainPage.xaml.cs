using System;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NasaApiExplorer.Views.Dialogs;

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
                        navMenu.Header = "Astronomy Picture of the Day";
                        mainFrame.Navigate(typeof(AstronomyPictureView));
                        break;
                    case "Earth_EventTracker":
                        mainFrame.Navigate(typeof(NaturalEventTrackerView));
                        break;
                    case "PR_Photos":
                        navMenu.Header = "Perseverance Rover Photos";
                        mainFrame.Navigate(typeof(PerseveranceRoverPhotosView));
                        break;
                    case "CR_Photos":
                        navMenu.Header = "Curiosity Rover Photos";
                        mainFrame.Navigate(typeof(CuriosityRoverPhotosView));
                        break;
                    case "OR_Photos":
                        navMenu.Header = "Opportunity Rover Photos";
                        mainFrame.Navigate(typeof(OpportunityRoverPhotosView));
                        break;
                    case "SR_Photos":
                        navMenu.Header = "Spirit Rover Photos";
                        mainFrame.Navigate(typeof(SpiritRoverPhotosView));
                        break;
                    case "About":
                        navMenu.Header = "About";
                        mainFrame.Navigate(typeof(AboutPageView));
                        break;
                    default:
                        mainFrame.Navigate(typeof(AstronomyPictureView));
                        break;
                }
            }
        }
    }
}