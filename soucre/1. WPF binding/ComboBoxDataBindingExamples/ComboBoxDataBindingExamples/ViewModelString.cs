﻿using System.ComponentModel;

namespace ComboBoxDataBindingExamples
{
    /// <summary>
    /// Class used to bind the combobox selections to. Must implement 
    /// INotifyPropertyChanged in order to get the data binding to 
    /// work correctly.
    /// </summary>
    public class ViewModelString : INotifyPropertyChanged
    {
        /// <summary>
        /// Need a void constructor in order to use as an object element 
        /// in the XAML.
        /// </summary>
        public ViewModelString()
        {
        }

        private string _colorString = "Red";
        /// <summary>
        /// String property used in binding examples.
        /// </summary>
        public string ColorString
        {
            get { return _colorString; }
            set
            {
                if (_colorString != value)
                {
                    _colorString = value;
                    NotifyPropertyChanged("ColorString");
                }
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Need to implement this interface in order to get data binding
        /// to work properly.
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
