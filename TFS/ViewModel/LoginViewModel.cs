using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TFS.Model;

namespace TFS.ViewModel
{
    public class LoginViewModel
    {
        public CustomerLogin Login { get; set; }
        public string Text { get; set; }
        public LoginViewModel()
        {
            Login = new CustomerLogin();
        }

        //public string na()
        //{
        //    return "";
        //}
    }
}
