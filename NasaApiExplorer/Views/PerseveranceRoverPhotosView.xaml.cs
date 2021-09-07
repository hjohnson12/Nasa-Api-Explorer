﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasaApiExplorer.Models;
using NasaApiExplorer.ViewModels;
using NasaApiExplorer.Views.Dialogs;

namespace NasaApiExplorer.Views
{
    public sealed partial class PerseveranceRoverPhotosView : Page
    {
        public PerseveranceRoverPhotosViewModel ViewModel => (PerseveranceRoverPhotosViewModel)DataContext;

        public PerseveranceRoverPhotosView()
        {
            this.InitializeComponent();

            this.DataContext =
                    App.Current.ServiceHost.Services.GetService<PerseveranceRoverPhotosViewModel>();

            // Current day since mission is still active
            RoverPhotosDatePicker.MaxDate = DateTime.Today;
        }
    }
}