using System.Windows;

namespace ComboBoxDataBindingExamples
{
    /// <summary>
    /// This example:
    ///  - Displays a 'string' value and binds to a 'string' value.
    ///  - All binding are done in XAML.
    ///  - Combobox items collection is defined in XAML.
    ///  - A text field is displayed under each combobox that is bound to the
    ///    same property as the combobox.
    ///    - This demonstrates that the binding from the combobox to the data
    ///      object and back out to another cotrol.
    /// </summary>
    public partial class Example1Window : Window
    {
        // Object to bind the combobox selections to.
        private ViewModelString viewModelString = new ViewModelString();

        public Example1Window()
        {
            // Set the data context for this window.
            DataContext = viewModelString;

            // Display the window
            InitializeComponent();
        }
    }
}
