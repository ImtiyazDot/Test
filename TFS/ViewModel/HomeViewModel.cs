using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TFS.BussinessLogic;
using TFS.Model;

namespace TFS.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CityDetails> cityDataSource;
        private bool visibility;
        private ICommand btnsearch;
        public static CityDetails CityDeatil { get; set; }

        public HomeViewModel()
        {
            CityDeatil = new CityDetails();
            this.btnsearch = new DelegateCommand(this.ButtonCitySearchAction);
        }

        private void ButtonCitySearchAction(object obj)
        {
            GetCityData(CityDeatil.txtSearch);
        }

        // Loding city data from the local storage   
        public void GetCityData(String searchKey)
        {
            try
            {
                if (DatastoreCity.CityList != null)
                {
                    this.visibility = true;
                    this.RaisePropertyChanged("IsVisible");
                    IEnumerable<KeyValuePair<string, object>> query = null;

                    if (searchKey != "All")
                    {
                        query = from city in DatastoreCity.CityList where city.Key == searchKey select city;
                    }
                    else
                    {
                        query = from city in DatastoreCity.CityList select city;
                    }                   
                   
                    foreach (var city in query)
                    {
                        this.DataSource.Add(new CityDetails() { City = city.Key, Credentials= city.Value.ToString() });
                    }                    
                }
            }
            catch
            {
            }
        }

        // Binding data into the city listbox   
        public ObservableCollection<CityDetails> DataSource
        {
            get
            {
                if (this.cityDataSource == null)
                {
                    this.cityDataSource = new ObservableCollection<CityDetails>();
                }
                return this.cityDataSource;
            }
        }

        //City Selected item
        #region city selected item

        private CityDetails selectedCity;
        public CityDetails SelectedCity
        {
            get {
                //MessageBox.Show(selectedCity.City);
                return this.selectedCity;
            }
            set
            {
                if (this.selectedCity != value)
                {
                    this.selectedCity = value;
                    MessageBox.Show(value.City);

                    if (this.selectedCity != null)
                    {
                        this.visibility = false;
                    }
                    this.RaisePropertyChanged("IsVisible");
                    //this.RaisePropertyChanged("SelectedAge");
                }
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
        #endregion

        public bool IsVisible
        {
            get
            {
                //if (this.SelectedCity != null)
                //{
                    return visibility;
                //}
                //return false;
            }
            set { }           
        }
      
        public ICommand btnSearchCity
        {
            get
            {
                return this.btnsearch;
            }
        }

    }
}
