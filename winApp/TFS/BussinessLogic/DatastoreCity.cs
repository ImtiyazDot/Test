using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFS.Model;

namespace TFS.BussinessLogic
{
    public static class DatastoreCity
    {
        private static IsolatedStorageSettings localSettings = IsolatedStorageSettings.ApplicationSettings;
        public static Dictionary<string, Object> CityList
        {
            get
            {
                if (localSettings.Contains("cityList"))
                    return (Dictionary<string, Object>)localSettings["cityList"];
                else
                    return null;
            }
            set
            {
                localSettings["cityList"] = value;
            }
        }

    }
}
