using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Windows;

namespace TFS.Services
{
    class ServiceGateway
    {
        HttpClient client;
        

        public ServiceGateway()
        {
            client = new HttpClient();
        }

        //API response for the customer transactions.
        //url will be the API methods and the input parameters postdata will be list of keyvaluepair List<KeyValuePair<string, string>>
        public async Task<JObject> GetCustomerResponse(string url, List<KeyValuePair<string, string>> postData)
        {
            client.BaseAddress = new Uri(BussinessLogic.GlobalVariables.CustomerURL);  //Assign the base url for the API
            JObject data = null;
            using (var content = new FormUrlEncodedContent(postData))
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        data = JObject.Parse(await response.Content.ReadAsStringAsync());
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return data;
                }
            }
        }

        //API response for the OTP for phone Number
        //url will be the API methods and the input parameters postdata will be list of keyvaluepair List<KeyValuePair<string, string>>
        public async Task<JObject> ProcessOTP(string url, List<KeyValuePair<string, string>> postData)
        {
            client.BaseAddress = new Uri(BussinessLogic.GlobalVariables.OTPURL);  //Assign the base url for the API
            JObject data = null;
            using (var content = new FormUrlEncodedContent(postData))
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        data = JObject.Parse(await response.Content.ReadAsStringAsync());
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return data;
                }
            }
        }
        
        //API response for the booking transactions.
        //url will be the API methods and the input parameters postdata will be list of keyvaluepair List<KeyValuePair<string, string>>
        public async Task<JObject> GetBookingResponse(string url, List<KeyValuePair<string, string>> postData)
        {
            client.BaseAddress = new Uri(BussinessLogic.GlobalVariables.BookingURL);
            JObject data = null;
            using (var content = new FormUrlEncodedContent(postData))
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        data = JObject.Parse(await response.Content.ReadAsStringAsync());
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return data;
                }
            }
        }
        
        //API response for the configurations.
        //url will be the API methods and the input parameters postdata will be list of keyvaluepair List<KeyValuePair<string, string>>
        public async Task<JObject> GetApplicationConfig(string url, List<KeyValuePair<string, string>> postData)
        {
            string QueryString = "/?";
            for (int i = 0; i < postData.Count; i++)
            {
                QueryString += postData[i].ToString().Replace("[", "").Replace("]", "").Replace(",", "=") + "&";
            }

            client.BaseAddress = new Uri(BussinessLogic.GlobalVariables.ApplicationConfig);
            JObject data = null;
            using (var content = new FormUrlEncodedContent(postData))
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url + QueryString);
                    if (response.IsSuccessStatusCode)
                    {
                        data = JObject.Parse(await response.Content.ReadAsStringAsync());
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return data;
                }
            }
        }

        //url for City details(City Selection Data)
        public async Task<JObject> GetCityDetails(string url)
        {
            string QueryString = "/?";            

            client.BaseAddress = new Uri(BussinessLogic.GlobalVariables.ApplicationConfig);
            JObject data = null;
            //using (var content = new FormUrlEncodedContent(postData))
            //{
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        data = JObject.Parse(await response.Content.ReadAsStringAsync());
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return data;
                }
            //}
        }

        public void CancelPending()
        {
            client.CancelPendingRequests();
        }
    }
}
