using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DatabindingDemo
{
    public class Person : INotifyPropertyChanged
    {
        string _firstName;
        string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                this.OnPropertyChanged("FirstName");
                this.OnPropertyChanged("FullName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                this.OnPropertyChanged("LastName");
                this.OnPropertyChanged("FullName");
            }
        }

        public string FullName
        {
            get
            {
                return String.Format("{0}, {1}",
                    this.LastName, this.FirstName);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(
                    this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }

    // This is used by the "Manually Move Data" logic. 
    //public class Person
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }

    //    public string FullName
    //    {
    //        get
    //        {
    //            return String.Format("{0}, {1}",
    //                this.LastName, this.FirstName);
    //        }
    //    }
    //}
}