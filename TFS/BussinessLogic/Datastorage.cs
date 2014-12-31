using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TFS.BussinessLogic
{
    public static class Datastorage
    {
        private static IsolatedStorageSettings localSettings = IsolatedStorageSettings.ApplicationSettings;

        public static bool IsGpsAllowed
        {
            get
            {
                if(localSettings.Contains("IsGpsAllowed"))              
                    return (bool)localSettings["IsGpsAllowed"];
                else
                    return false;                
            }
            set
            {
                localSettings["IsGpsAllowed"] = value;
            }
        }


        public static String PrevLatitude
        {
            get
            {
                if (localSettings.Contains("PrevLat"))
                    return (String)localSettings["PrevLat"];
                else
                    return "";
            }
            set { localSettings["PrevLat"] = value; }
        }

        public static String PrevLongitude
        {
            get
            {
                if (localSettings.Contains("PrevLong")) 
                    return (String)localSettings["PrevLong"];
                else
                    return "";
            }
            set { localSettings["PrevLong"] = value; }
        }


        public static bool AskForGps()
        {
            if (!IsGpsAllowed)
            {
                var result =
                    MessageBox.Show(
                        "This application requires GPS for best results. To enable gps select ok",
                        "TaxiForSure - Allow Gps", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    IsGpsAllowed = true;
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }


        }
    }
}
