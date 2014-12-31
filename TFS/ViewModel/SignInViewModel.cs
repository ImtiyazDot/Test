using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TFS.BussinessLogic;
using System.Windows.Input;
using TFS.Model;
using System.Net.NetworkInformation;
using System.Windows.Controls.Primitives;

namespace TFS.ViewModel
{
    class SignInViewModel
    {
        public static Model.Request.SignInModel SignIn { get; set; }
        private ICommand config;
        private Popup popup;

        public SignInViewModel()
        {
            SignIn = new Model.Request.SignInModel();
            this.config = new DelegateCommand(this.ConfigAction);
        }

        public async void mSignIn()
        {
            if (string.IsNullOrEmpty(SignIn.Email) || string.IsNullOrEmpty(SignIn.Password))
            {
                MessageBox.Show("Enter email and password.", "Taxi For Sure", MessageBoxButton.OK);
            }
            else
            {
                if (!Regex.IsMatch(SignIn.Email, @"^(?![012])(\d{10})$"))
                {
                    MessageBox.Show("Enter a valid 10 digit phone number.", "Taxi For Sure", MessageBoxButton.OK);
                    return;
                }

                //Login method
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("username", SignIn.Email));
                postData.Add(new KeyValuePair<string, string>("password", SignIn.Password));

                Services.ServiceAPIs srv = new Services.ServiceAPIs();
                JObject data = await srv.CustomerLogin(postData);

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
                        MessageBox.Show((String)data["error_desc"], "Taxi For Sure", MessageBoxButton.OK);
                    }
                }
            }
        }

        private async void ConfigAction(object p)
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                ShowSplash();
                Services.ServiceAPIs srv = new Services.ServiceAPIs();
                var res = await srv.CityDetails();
                JObject data = JObject.Parse(res.ToString());

                if (data != null)
                {
                    if ("success".Equals((string)data["status"]))
                    {
                        var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ToString());
                        Dictionary<string, object> dict = (Dictionary<string, object>)values;

                        string responseData = (string)dict["response_data"].ToString();
                        Dictionary<string, object> responseDataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                        string centerString = (string)responseDataDict["CENTER"].ToString();

                        Dictionary<string, Object> centerDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(centerString);
                        DatastoreCity.CityList = centerDict;

                        this.popup.IsOpen = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please check your network connection.");
            }
        }

        private void ShowSplash()
        {
            this.popup = new Popup();
            this.popup.Child = new Loader();
            this.popup.IsOpen = true;
            //StartLoadingData();
        }

        public ICommand Config
        {
            get
            {
                return this.config;
            }
        }        
    }
}


