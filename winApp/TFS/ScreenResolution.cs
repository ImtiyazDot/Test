using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS
{
    public class ScreenResolution
    {
        public static bool IsWvga
        {
            get
            {
                return App.Current.Host.Content.ScaleFactor == 100;
            }
        }

        public static bool IsWxga
        {
            get
            {
                return App.Current.Host.Content.ScaleFactor == 160;
            }
        }

        public static bool Is720p
        {
            get
            {
                return App.Current.Host.Content.ScaleFactor == 150;
            }
        }

        public static string CurrentResolution
        {
            get
            {
                if (IsWvga) return "WVGA";
                else if (IsWxga) return "WXGA";
                else if (Is720p) return "HD720p";
                else throw new InvalidOperationException("Unknown resolution");
            }
        }
    }
}
