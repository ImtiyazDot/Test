using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;

namespace TFS
{
    public partial class SplashScreen : PhoneApplicationPage
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int i = 0;
        public SplashScreen()
        {
            InitializeComponent();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            this.progressBar1.IsIndeterminate = true;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            i++;
            if (i > 2)
            {
                NavigationService.Navigate(new Uri("/View/SignIn.xaml", UriKind.Relative));
            }
        }
    }
}