using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TFS.Model
{
    public class CityDetails : INotifyPropertyChanged
    {
        public string City
        {
            get;
            set;
            //get
            //{
            //    return _city;
            //}
            //set
            //{
            //    if (this._city != value)
            //    {
            //        this._city = value;
            //        this.RaisePropertyChanged("City");
            //    }
            //}
        }

        public string Credentials { get; set; }

        private string _txtsearch = string.Empty;
        public string txtSearch
        {
            get
            {
                return _txtsearch;
            }
            set
            {
                _txtsearch = value;
                RaisePropertyChanged("txtSearch");
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
