using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TFS.Resources;
using TFS.View;
using TFS.ViewModel;
using TFS.Model;
using TFS.BussinessLogic;
using System.Diagnostics;

namespace TFS
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new SignInViewModel();
           

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/View/SignIn.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //List<CityDetails> cityStringList = new List<CityDetails>();
            //foreach(var city in DatastoreCity.CityList)
            //{
            //    //Debug.WriteLine("----------------------- \n {0}", city.Key);
            //}

            NavigationService.Navigate(new Uri("/View/HomeScreen.xaml", UriKind.RelativeOrAbsolute));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}