using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TFS.BussinessLogic;
using TFS.Model;
using TFS.ViewModel;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Maps.Toolkit;

namespace TFS.View
{
    public partial class HomeScreen : PhoneApplicationPage
    {    
        private GeoCoordinateWatcher watcher;
        private GeoPositionAccuracy accuracy = GeoPositionAccuracy.High;
        private Popup popup;
        HomeViewModel vm; ApplicationBar app;
        public HomeScreen()
        {
            InitializeComponent();
            app = new ApplicationBar();

            if(ScreenResolution.IsWvga)
            {
                MainGrid.Height = 768 - app.DefaultSize;
                TFSMap.Height = MainGrid.Height - (grd.Height + grdCarSelect.Height);
                string ScreenWidth = Application.Current.Host.Content.ActualHeight.ToString();
                //MessageBox.Show(LayoutRoot.ActualHeight.ToString());   
            }
            else if(ScreenResolution.IsWxga)
            {
                MainGrid.Height = 768 - app.DefaultSize;
                TFSMap.Height = MainGrid.Height - (grd.Height + grdCarSelect.Height);
            }
            else
            {
                MainGrid.Height = 820 - app.DefaultSize;
                TFSMap.Height = MainGrid.Height - (grd.Height + grdCarSelect.Height);
            }


            vm = new HomeViewModel();
            this.DataContext = vm;
            //CheckLocation();
            this.popup = new Popup();
            //if (!Datastorage.IsGpsAllowed)
            //{
                //if (Datastorage.GeoLocation != null)
                //    ShowMyLocationOnTheMap();
                //else
                    GetCoordinate();
            //}
        }
       

        public void GetCoordinate()
        {
            var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High)
            {
                MovementThreshold = 1
            };
            watcher.Start();
            if (watcher.Permission.Equals(GeoPositionPermission.Denied) || watcher.Permission.Equals(GeoPositionPermission.Unknown))
            {
                var result = MessageBox.Show(
                           "This application requires GPS for best results. To enable gps select ok",
                           "TaxiForSure - Allow Gps", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        var op = Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                        //NavigationService.GoBack();
                    });
                    ShowSplash();
                }
                else
                {
                    Datastorage.IsGpsAllowed = false;
                    //popupCitySelection.IsOpen = true;
                    vm.GetCityData("All");
                }
            }
            else
            {
                ShowSplash();
                watcher.PositionChanged += this.watcher_PositionChanged;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High)
            {
                MovementThreshold = 1
            };
            watcher.Start();
            if (watcher.Permission.Equals(GeoPositionPermission.Granted))
            {
                //ShowSplash();
                //if (Datastorage.GeoLocation != null)
                //{
                //    ShowMyLocationOnTheMap();
                //}
                //else
                //{
                    watcher.PositionChanged += this.watcher_PositionChanged;
                    //ShowSplash();
                //}
            }
        }

        public void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            //bool isNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();
            //if (isNetworkAvailable == true)
            //{                    
                var pos = e.Position.Location;
                Datastorage.GeoLocation = pos;
                //Datastorage.PrevLatitude = pos.Latitude.ToString();
                //Datastorage.PrevLongitude = pos.Longitude.ToString();
                Datastorage.IsGpsAllowed = true;               
                ShowMyLocationOnTheMap();            
            //}
            //popup.IsOpen = false;
        }

        public void ShowMyLocationOnTheMap()
        {           
            this.TFSMap.Center = new GeoCoordinate(Datastorage.GeoLocation.Latitude, Datastorage.GeoLocation.Longitude);
            this.TFSMap.ZoomLevel = 13;

            Pushpin locationPushpin = new Pushpin();
            
            BitmapImage imgSource = new BitmapImage(new Uri("/Assets/Home/location@_wvga.png", UriKind.RelativeOrAbsolute));           
            Grid ImageGrid = new Grid();
            Image img = new Image();
            img.Source = imgSource;
            ImageGrid.Children.Add(img);

            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = ImageGrid;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = new GeoCoordinate(Datastorage.GeoLocation.Latitude, Datastorage.GeoLocation.Longitude);

            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            TFSMap.Layers.Add(myLocationLayer);
            this.popup.IsOpen = false;
        }

        private DependencyObject CreatePushPin()
        {
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;
            return myCircle;
        }

        public void CheckLocation()
        {
            if (!Datastorage.IsGpsAllowed)
            {
                watcher = new GeoCoordinateWatcher(accuracy);
                watcher.MovementThreshold = 50;
                watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
                //watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
                watcher.Start();              
            }           
        }

        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => myStatusChanged(e));
        }     

        void myStatusChanged(GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:                                             
                    var result =
                    MessageBox.Show(
                            "This application requires GPS for best results. To enable gps select ok",
                            "TaxiForSure - Allow Gps", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        Dispatcher.BeginInvoke(() =>
                            {
                                var op = Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                                //NavigationService.GoBack();
                            });                               
                    }
                    else
                    {
                        Datastorage.IsGpsAllowed = false;
                        //popupCitySelection.IsOpen = true;
                        vm.GetCityData("All");
                    }
                    break;                           
                case GeoPositionStatus.Initializing:
                    break;
                case GeoPositionStatus.NoData:
                    MessageBox.Show("Data Unavailable.");
                    //NavigationService.GoBack();
                    break;
                case GeoPositionStatus.Ready:                   
                    break;
            }
        }

        private void CitySearch_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.HomeViewModel.CityDeatil.txtSearch = txtSearchCity.Text;
            var emptytext = string.IsNullOrEmpty(txtSearchCity.Text);
            txtSearchBak.Opacity = emptytext ? 100 : 0;
            txtSearchCity.Opacity = emptytext ? 0 : 100;
        }

        private void CitySearch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearchBak.Opacity = 0;
            txtSearchCity.Opacity = 100;
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (popupCitySelection.IsOpen)
            {
                e.Cancel = true;
                popupCitySelection.IsOpen = false;
            }
            else if(popup.IsOpen)
            {
                popup.IsOpen = false;
            }
        }
        
        private void ShowSplash()
        {
            this.popup = new Popup();
            this.popup.Child = new Loader();
            this.popup.IsOpen = true;
            //StartLoadingData();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CityDetails city = (sender as ListBox).SelectedItem as CityDetails;
            string[] credentials = city.Credentials.Split(',');
            if (credentials.Count() > 0)
            {
                Datastorage.GeoLocation = new GeoCoordinate(Convert.ToDouble(credentials[0]), Convert.ToDouble(credentials[1]));
                ShowMyLocationOnTheMap();
            }
            popupCitySelection.IsOpen = false;
        }
     
        private void CurrentLocationClick(object sender, RoutedEventArgs e)
        {
            GetCoordinate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //listCities.ItemsSource = null;
            //vm.GetCityData(txtSearchCity.Text);
        }
        
    }
}