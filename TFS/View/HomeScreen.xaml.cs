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

namespace TFS.View
{
    public partial class HomeScreen : PhoneApplicationPage
    {    
        private GeoCoordinateWatcher watcher;
        private GeoPositionAccuracy accuracy = GeoPositionAccuracy.High;

        HomeViewModel vm;
        public HomeScreen()
        {
            InitializeComponent();
            
            if(ScreenResolution.IsWvga)
            {

            }
            else if(ScreenResolution.IsWxga)
            {
                MainGrid.Height = 768;
            }
            else
            {
                MainGrid.Height = 820;
            }

            vm = new HomeViewModel();
            this.DataContext = vm;
            //CheckLocation();

            if (!Datastorage.IsGpsAllowed)
            {
                GetCoordinate();
            }
        }

        private async void ShowMyLocationOnTheMap()
        {
            //Geolocator myGeolocator = new Geolocator();
            //myGeolocator.DesiredAccuracyInMeters = 50;
            //Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            //Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            //myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

            //this.TFSMap.Center = ;
            //this.TFSMap.ZoomLevel = 13;

            //var Pushpin = CreatePushPin();
            //MapOverlay myLocationOverlay = new MapOverlay();
            //myLocationOverlay.Content = Pushpin;
            //myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            //myLocationOverlay.GeoCoordinate = myGeoCoordinate;

            //MapLayer myLocationLayer = new MapLayer();
            //myLocationLayer.Add(myLocationOverlay);

            //TFSMap.Layers.Add(myLocationLayer);
        }

        //private DependencyObject CreatePushPin()
        //{
        //    Ellipse myCircle = new Ellipse();
        //    myCircle.Fill = new SolidColorBrush(Colors.Blue);
        //    myCircle.Height = 20;
        //    myCircle.Width = 20;
        //    myCircle.Opacity = 50;
        //    return myCircle;
        //}

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
                watcher.PositionChanged += this.watcher_PositionChanged;
            }

            ShowMyLocationOnTheMap();
        }

        public void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            //bool isNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();
            //if (isNetworkAvailable == true)
            //{
                var pos = e.Position.Location;
                Datastorage.PrevLatitude = pos.Latitude.ToString();
                Datastorage.PrevLongitude = pos.Longitude.ToString();
                Datastorage.IsGpsAllowed = true;
            //}
            //popup.IsOpen = false;
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
            //MessageBox.Show(e.Status.ToString());

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
                    //Datastorage.IsGpsAllowed = true;
                    //watcher.PositionChanged += this.watcher_PositionChanged;
                    //MessageBox.Show(e.Status.ToString());
                    break;
            }
        }

        private void CitySearch_LostFocus(object sender, RoutedEventArgs e)
        {
            var emptytext = string.IsNullOrEmpty(txtSearchCity.Text);
            txtSearchBak.Opacity = emptytext ? 100 : 0;
            txtSearchCity.Opacity = emptytext ? 0 : 100;

            ViewModel.HomeViewModel.CityDeatil.txtSearch = txtSearchCity.Text;
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
        }

        private void popupCitySelection_TextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }
    }
}