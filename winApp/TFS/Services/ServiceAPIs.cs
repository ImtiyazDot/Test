using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows;

namespace TFS.Services
{
    //call the service APIs and get the response as jason format
    class ServiceAPIs
    {
        //method to call customer login
        public async Task<JObject> CustomerLogin(List<KeyValuePair<string, string>> postData)
        {
            ServiceGateway IniSer = new ServiceGateway();
            JObject data = await IniSer.GetCustomerResponse("user/login/", postData);
            return data;
        }

        //method to register a customer
        public async Task<JObject> RegisterCustomer(List<KeyValuePair<string, string>> postData)
        {
            ServiceGateway IniSer = new ServiceGateway();
            JObject data = await IniSer.GetCustomerResponse("user/register/", postData);

            return data;
        }

        //method to get the password for forgot password
        public async Task<JObject> GetPassword(List<KeyValuePair<string, string>> postData)
        {
            ServiceGateway IniSer = new ServiceGateway();
            JObject data = await IniSer.GetCustomerResponse("user/forgot-password/", postData);

            return data;
        }

        //method to generate the OTP number
        public async Task<JObject> GenerateOTP(List<KeyValuePair<string, string>> postData)
        {
            ServiceGateway IniSer = new ServiceGateway();
            JObject data = await IniSer.ProcessOTP("api/consumer-app/generate-otp/", postData);

            return data;
        }

        //method to varify an OTP number
        public async Task<JObject> VerifyOTP(List<KeyValuePair<string, string>> postData)
        {
            ServiceGateway IniSer = new ServiceGateway();
            JObject data = await IniSer.ProcessOTP("api/verify-one-time-password/", postData);

            return data;
        }

        //method to get the configuration details for a guest user
        public async Task<JObject> GuestUserConfig(List<KeyValuePair<string, string>> postData)
        {
            ServiceGateway IniSer = new ServiceGateway();
            JObject data = await IniSer.GetApplicationConfig("api/consumer-app/config/", postData);

            return data;
        }

        //method to get the configuration details for a logged-in user
        public async Task<JObject> LoggedInUserConfig(List<KeyValuePair<string, string>> postData)
        {
            ServiceGateway IniSer = new ServiceGateway();
            JObject data = await IniSer.GetApplicationConfig("api/consumer-app/config/", postData);

            return data;
        }

        //method to get the configuration details for a logged-in user
        public async Task<JObject> CityDetails()
        {
            ServiceGateway IniSer = new ServiceGateway();
            JObject data = await IniSer.GetCityDetails("/api/consumer-app/config/?user_id=&customer_number=&latitude=&longitude=&polygon_hash=&string_hash=&appVersion=4.1.1");
            return data;
        }
    }
}
