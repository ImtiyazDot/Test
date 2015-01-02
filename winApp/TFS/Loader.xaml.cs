using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TFS
{
    public partial class Loader : PhoneApplicationPage
    {
        public Loader()
        {
            InitializeComponent();
            if (ScreenResolution.IsWvga)
            {

            }
            else if (ScreenResolution.IsWxga)
            {
                MainGrid.Height = 768;
            }
            else
            {
                MainGrid.Height = 820;
            }

            this.progressBar1.IsIndeterminate = true;
        }
    }
}