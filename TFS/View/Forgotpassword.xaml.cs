using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace TFS.View
{
    public partial class Forgotpassword : PhoneApplicationPage
    {
        public Forgotpassword()
        {
            this.DataContext = new TFS.ViewModel.ForgotPasswordViewModel();
            InitializeComponent();
        }

        private void Mobile_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.ForgotPasswordViewModel.ForgotPassword.PhoneNumber = txtMobile.Text;
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.ForgotPasswordViewModel.ForgotPassword.Email = txtEmail.Text;
           
        }


    }
}