//using Microsoft.Toolkit.Parsers.Rss;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.System;
//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Data;
//using Windows.UI.Xaml.Input;
//using Windows.UI.Xaml.Media;
//using Windows.UI.Xaml.Navigation;
//using Windows.Web.Http;

//// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

//namespace NasaDataExplorer.Views.Feeds
//{
//    /// <summary>
//    /// An empty page that can be used on its own or navigated to within a Frame.
//    /// </summary>
//    public sealed partial class NasaEarthObservatoryFeedView : Page
//    {
//        public ObservableCollection<RssSchema> RSSFeed { get; } = new ObservableCollection<RssSchema>();

//        public NasaEarthObservatoryFeedView()
//        {
//            this.InitializeComponent();
//        }

//        public string Url { get; set; } = "https://earthobservatory.nasa.gov/feeds/earth-observatory.rss";

//        public async void ParseRSS()
//        {
//            string feed = null;
//            RSSFeed.Clear();

//            using (var client = new HttpClient())
//            {
//                try
//                {
//                    feed = await client.GetStringAsync(new Uri(Url));
//                }
//                catch
//                {
//                }
//            }

//            if (feed != null)
//            {
//                var parser = new RssParser();
//                var rss = parser.Parse(feed);

//                foreach (var element in rss)
//                {
//                    RSSFeed.Add(element);
//                }
//            }
//        }

//        private async void RSSList_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            if (RSSList.SelectedItem is RssSchema rssItem)
//            {
//                try
//                {
//                    await Launcher.LaunchUriAsync(new Uri(rssItem.FeedUrl));
//                }
//                catch
//                {
//                }
//            }

//            RSSList.SelectedItem = null;
//        }

//        private void imgNews_Loaded(object sender, RoutedEventArgs e)
//        {
//            var test = sender as Image;
//            if (test.Source == null)
//            {
//                //(sender as Image).Visibility = Visibility.Collapsed;
//            }
//        }

//        private void imgNews_ImageFailed(object sender, ExceptionRoutedEventArgs e)
//        {
//            var test = e.OriginalSource;
//        }
//    }
//}
