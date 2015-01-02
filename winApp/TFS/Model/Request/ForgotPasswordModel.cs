using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TFS.Model.Request
{
    public class ForgotPasswordModel : INotifyPropertyChanged
    {
        private  string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                RaisePropertyChanged("Email");
            }
        }

        private string _PhoneNumber ;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                _PhoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
