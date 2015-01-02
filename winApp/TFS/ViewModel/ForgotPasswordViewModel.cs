using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Navigation;

namespace TFS.ViewModel
{
    class ForgotPasswordViewModel
    {
        public static Model.Request.ForgotPasswordModel ForgotPassword { get; set; }

        public ForgotPasswordViewModel()
        {
            ForgotPassword = new Model.Request.ForgotPasswordModel();
        }

        public async void mForgotPassword()
        {
            if (string.IsNullOrEmpty(ForgotPassword.PhoneNumber) && string.IsNullOrEmpty(ForgotPassword.Email))
            {
                MessageBox.Show("Enter mobile number or email.", "Taxi For Sure", MessageBoxButton.OK);
            }
            else
            {
                //forgot password method
                var postData = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrEmpty(ForgotPassword.Email))
                {
                    if (!Regex.IsMatch(ForgotPassword.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                    {
                            MessageBox.Show("Enter a valid email id.", "Taxi For Sure", MessageBoxButton.OK);
                            return;
                    }
                    postData.Add(new KeyValuePair<string, string>("email", ForgotPassword.Email));
                }
                else
                {
                    if (!Regex.IsMatch(ForgotPassword.PhoneNumber, @"^(?![012])(\d{10})$"))
                    {
                        MessageBox.Show("Enter a valid 10 digit phone number.", "Taxi For Sure", MessageBoxButton.OK);
                        return;
                    }
                    postData.Add(new KeyValuePair<string, string>("email", ForgotPassword.PhoneNumber));
                }

                Services.ServiceAPIs srv = new Services.ServiceAPIs();
                JObject data = await srv.GetPassword(postData);

                if (data != null)
                {
                    if ("success".Equals((string)data["status"]))
                    {
                        MessageBox.Show((String)data["response_data"]["message"], "Taxi For Sure", MessageBoxButton.OK);
                    }
                    else if ((string)data["error_desc"] != "")
                    {
                        MessageBox.Show((String)data["error_desc"], "Taxi For Sure", MessageBoxButton.OK);
                    }
                }
            }
        }
    }
}

    

