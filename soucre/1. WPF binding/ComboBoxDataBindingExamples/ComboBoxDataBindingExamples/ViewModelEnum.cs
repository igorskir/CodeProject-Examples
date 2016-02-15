using System.ComponentModel;

namespace ComboBoxDataBindingExamples
{
    /// <summary>
    /// Class used to bind the combobox selections to. Must implement 
    /// INotifyPropertyChanged in order to get the data binding to 
    /// work correctly.
    /// </summary>
    public class ViewModelEnum : INotifyPropertyChanged
    {
        /// <summary>
        /// Enum used by the combobox and the view model.
        /// </summary>
        public enum Colors
        {
            Red = 1,
            Green = 2,
            Blue = 3,
        }

        /// <summary>
        /// Need a void constructor in order to use as an object element 
        /// in the XAML.
        /// </summary>
        public ViewModelEnum()
        {
        }

        private ViewModelEnum.Colors _colorEnum = ViewModelEnum.Colors.Red;
        /// <summary>
        /// ViewModelEnum.Colors Enum property used in binding examples.
        /// </summary>
        public ViewModelEnum.Colors ColorEnum
        {
            get { return _colorEnum; }
            set
            {
                if (_colorEnum != value)
                {
                    _colorEnum = value;
                    NotifyPropertyChanged("ColorEnum");
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
