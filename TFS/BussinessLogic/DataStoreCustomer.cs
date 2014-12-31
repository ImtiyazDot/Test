using System;
using System.IO.IsolatedStorage;

namespace TFS.BussinessLogic
{
    public static class DataStoreCustomer
    {
        private static IsolatedStorageSettings localSettings = IsolatedStorageSettings.ApplicationSettings;

        public static String UserId
        {
            get
            {
                if (localSettings.Contains("CustomerUserId")) return (String)localSettings["CustomerUserId"];
                else
                    return "";
            }
            set { localSettings["CustomerUserId"] = value; }
        }

        public static String Name
        {
            get
            {
                if (localSettings.Contains("CustomerName")) return (String)localSettings["CustomerName"];
                else
                    return "";
            }
            set { localSettings["CustomerName"] = value; }
        }

        public static String Email
        {
            get
            {
                if (localSettings.Contains("CustomerEmail")) return (String)localSettings["CustomerEmail"];
                else
                    return "";
            }
            set { localSettings["CustomerEmail"] = value; }

        }

        public static long PhoneNumber
        {
            get
            {
                if (localSettings.Contains("CutomersPhoneNumber")) return (long)localSettings["CustomerPhoneNumber"];
                else
                    return 0;
            }
            set { localSettings["CustomerPhoneNumber"] = value; }
        }

        public static string City
        {
            get
            {
                if (localSettings.Contains("CustomerCity")) return (string)localSettings["CustomerCity"];
                else
                    return "";
            }
            set { localSettings["CustomerCity"] = value; }
        }

        public static string ReferralCode
        {
            get
            {
                if (localSettings.Contains("CustomerReferralCode")) return (string)localSettings["CustomerReferralCode"];
                else
                    return "";
            }
            set { localSettings["CustomerReferralCode"] = value; }
        }

        public static string ReferralUrl
        {
            get
            {
                if (localSettings.Contains("CustomerReferralUrl")) return (string)localSettings["CustomerReferralUrl"];
                else
                    return "";
            }
            set { localSettings["CustomerReferralUrl"] = value; }
        }
    }
}
