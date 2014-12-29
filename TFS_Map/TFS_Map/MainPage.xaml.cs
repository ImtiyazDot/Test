using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TFS_Map.Resources;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Windows.Input;

namespace TFS_Map
{
    public partial class MainPage : PhoneApplicationPage
    {
        //string textSearch = "";
        MapOverlay myLocationOverlay = new MapOverlay();
        MapLayer myLocationLayer = new MapLayer();
        GeoCoordinate myGeoCoordinate;

        HttpClient htttpClient;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            htttpClient = new HttpClient();
            ShowMyLocationOnTheMap();

            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            Touch.FrameReported += Touch_FrameReported;
            
            //MessageBox.Show(Application.Current.Host.Content.ActualHeight.ToString());          
        }

        private void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            if (TouchPanel.IsGestureAvailable)
            {
                TouchPoint mainTouch = e.GetPrimaryTouchPoint(TFSMap);                           
                if (mainTouch.Action == TouchAction.Up)
                {
                    MessageBox.Show("Here -- " + e.GetTouchPoints(TFSMap).Count);
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
       
        private async void ShowMyLocationOnTheMap()
        {
            Geolocator myGeolocator = new Geolocator();
            myGeolocator.DesiredAccuracyInMeters = 50;
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

            this.TFSMap.Center = myGeoCoordinate;
            this.TFSMap.ZoomLevel = 13;

            var Pushpin = CreatePushPin();
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = Pushpin;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;

            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            TFSMap.Layers.Add(myLocationLayer);
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

        private void TFSMap_CenterChanged(object sender, MapCenterChangedEventArgs e)
        {
            //MessageBox.Show("center");

            myLocationLayer.Remove(myLocationOverlay);
            myLocationOverlay = new MapOverlay();
            myLocationLayer = new MapLayer();
            //MessageBox.Show(myLocationLayer.Count.ToString());
           
            var Pushpin = CreatePushPin();
            myLocationOverlay.Content = Pushpin;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = TFSMap.Center;

            myLocationLayer.Add(myLocationOverlay);
            TFSMap.Layers.Add(myLocationLayer);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
       
        public void CancelPending()
        {
            htttpClient.CancelPendingRequests();
        }

        private void btnPresentLocation_Click(object sender, RoutedEventArgs e)
        {
            ShowMyLocationOnTheMap();
        }

        private void TFSMap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           // MessageBox.Show("up");
        }

        private void TFSMap_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

        }
      
    }   
}