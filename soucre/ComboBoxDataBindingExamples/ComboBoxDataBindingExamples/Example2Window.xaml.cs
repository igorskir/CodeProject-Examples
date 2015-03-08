using System.Windows;
using System.Collections.Generic;

namespace ComboBoxDataBindingExamples
{
    /// <summary>
    /// This example:
    ///  - Displays a 'string' value and binds to an 'enum' value.
    ///  - The ItemsSource is bound to a collection in the code behind.
    ///  - All other binding are done in XAML.
    ///  - Combobox items collection is defined in XAML.
    ///  - A text field is displayed under each combobox that is bound to the
    ///    same property as the combobox.
    ///    - This demonstrates that the binding from the combobox to the data
    ///      object and back out to another cotrol.
    /// </summary>
    public partial class Example2Window : Window
    {
        // Collection property used used to fill the ComboBox with a list
        // of ComboBoxItemColor objects that contain colors options.
        public List<ComboBoxItemColor> ColorListEnum { get; set; }

        // Object to bind the combobox selections to.
        public ViewModelEnum ViewModelEnum { get; set; }
        private ViewModelEnum viewModelEnum = new ViewModelEnum();

        public Example2Window()
        {
            // Set the property to be used for the data binding to and from
            // the ComboBox's.
            ViewModelEnum = viewModelEnum;

            // Set up the collection properties used to bind the ItemsSource 
            // properties to display the list of items in the dropdown lists.
            // The string values are loaded from the resource file which can
            // be translated into other languages.
            ColorListEnum  = new List<ComboBoxItemColor>()
                {
                    new ComboBoxItemColor(){ ValueColorEnum = ViewModelEnum.Colors.Red, ValueColorString = Properties.Resources.RedText },
                    new ComboBoxItemColor(){ ValueColorEnum = ViewModelEnum.Colors.Green, ValueColorString = Properties.Resources.GreenText },
                    new ComboBoxItemColor(){ ValueColorEnum = ViewModelEnum.Colors.Blue, ValueColorString = Properties.Resources.BlueText },
                };

            // Set the data context for this window.
            DataContext = this;

            // Init the window
            InitializeComponent();
        }
    }
}
