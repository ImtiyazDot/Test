using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TFS.View
{
    public partial class SignIn : PhoneApplicationPage
    {
        public SignIn()
        {
            this.DataContext = new TFS.ViewModel.SignInViewModel();
            InitializeComponent();
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.SignInViewModel.SignIn.Email = txtEmail.Text;
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.SignInViewModel.SignIn.Password = txtpassword.Text;
        }
     }
}