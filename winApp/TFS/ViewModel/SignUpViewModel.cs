using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TFS.Model;
using System.Diagnostics;
using Newtonsoft.Json;
using TFS.BussinessLogic;
namespace TFS.ViewModel
{
    class SignUpViewModel
    {
        public static Model.Request.SignUpModel SignUp { get; set; }
        private ICommand config;

        public SignUpViewModel()
        {
            SignUp = new Model.Request.SignUpModel();
        }       

        public async void mSignUp()
        {
            if (string.IsNullOrEmpty(SignUp.PhoneNumber) || string.IsNullOrEmpty(SignUp.Email) || string.IsNullOrEmpty(SignUp.Name)
                 || string.IsNullOrEmpty(SignUp.Password) || string.IsNullOrEmpty(SignUp.ConfirmPassword))
            {
                MessageBox.Show("Enter all data.", "Taxi For Sure", MessageBoxButton.OK);
            }
            else if (string.IsNullOrEmpty(SignUp.Password) != string.IsNullOrEmpty(SignUp.ConfirmPassword))
                MessageBox.Show("Password not matching", "Taxi For Sure", MessageBoxButton.OK);
            else
            {
                if (!Regex.IsMatch(SignUp.PhoneNumber, @"^(?![012])(\d{10})$"))
                {
                    MessageBox.Show("Enter a valid 10 digit number.", "Taxi For Sure", MessageBoxButton.OK);
                    return;
                }
                if (!Regex.IsMatch(SignUp.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    if (!string.IsNullOrWhiteSpace(SignUp.Email))
                    {
                        MessageBox.Show("Enter a valid email id.", "Taxi For Sure", MessageBoxButton.OK);
                        return;
                    }
                }

                //Registion method call
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("mobile", SignUp.PhoneNumber));
                postData.Add(new KeyValuePair<string, string>("email", SignUp.Email));
                postData.Add(new KeyValuePair<string, string>("appVersion", "5.2"));
                postData.Add(new KeyValuePair<string, string>("name", SignUp.Name));
                postData.Add(new KeyValuePair<string, string>("fut", "Google Search"));
                postData.Add(new KeyValuePair<string, string>("password", SignUp.Password));

                Services.ServiceAPIs srv = new Services.ServiceAPIs();
                JObject data = await srv.RegisterCustomer(postData);

                if (data != null)
                {
                    if ("success".Equals((string)data["status"]))
                    {
                        var obj = data["response_data"];
                        TFS.BussinessLogic.DataStoreCustomer.UserId = (String)obj["user_id"];
                        TFS.BussinessLogic.DataStoreCustomer.Name = (String)obj["customer_name"];
                        TFS.BussinessLogic.DataStoreCustomer.PhoneNumber = (long)obj["customer_number"];
                        TFS.BussinessLogic.DataStoreCustomer.Email = (String)obj["customer_email"];
                        TFS.BussinessLogic.DataStoreCustomer.ReferralCode = (String)obj["referral_code"];
                        TFS.BussinessLogic.DataStoreCustomer.ReferralUrl = (String)obj["referral_url"];
                    }
                    else if ((string)data["error_desc"] != "")
                    {
                        MessageBox.Show((String)data["error_desc"].ToString(), "Taxi For Sure", MessageBoxButton.OK);
                    }
                }
            }
        }

       

    }
}
