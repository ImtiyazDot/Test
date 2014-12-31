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
    public partial class SignUp : PhoneApplicationPage
    {
        public SignUp()
        {
            this.DataContext = new TFS.ViewModel.SignUpViewModel();
            InitializeComponent();
        }

        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.SignUpViewModel.SignUp.Name = tbname.Text;
        }

        private void Mobile_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.SignUpViewModel.SignUp.PhoneNumber = tbMobile.Text;
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.SignUpViewModel.SignUp.Email = tbEmail.Text;
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.SignUpViewModel.SignUp.Password = tbPassword.Text;
        }

        private void ConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.SignUpViewModel.SignUp.ConfirmPassword = tbconfirmpassword.Text;
        }
    }
}