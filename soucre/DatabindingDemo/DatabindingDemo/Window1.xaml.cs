using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabindingDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            // Only one of the following method
            // calls should be uncommented at a time.

            //this.ManuallyMoveData();

            //this.BindInCode();

            this.BindInXaml();
        }

        #region ManuallyMoveData

        Person _person;

        // This method is invoked by the Window's constructor.
        private void ManuallyMoveData()
        {
            _person = new Person
            {
                FirstName = "Josh",
                LastName = "Smith"
            };

            this.firstNameTextBox.Text = _person.FirstName;
            this.lastNameTextBox.Text = _person.LastName;
            this.fullNameTextBlock.Text = _person.FullName;

            this.firstNameTextBox.TextChanged += firstNameTextBox_TextChanged;
            this.lastNameTextBox.TextChanged += lastNameTextBox_TextChanged;
        }

        void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _person.LastName = this.lastNameTextBox.Text;
            this.fullNameTextBlock.Text = _person.FullName;
        }

        void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _person.FirstName = this.firstNameTextBox.Text;
            this.fullNameTextBlock.Text = _person.FullName;
        }

        #endregion // ManuallyMoveData

        #region BindInCode

        private void BindInCode()
        {
            var person = new Person
            {
                FirstName = "Josh",
                LastName = "Smith"
            };

            Binding b = new Binding();
            b.Source = person;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            b.Path = new PropertyPath("FirstName");
            this.firstNameTextBox.SetBinding(TextBox.TextProperty, b);

            b = new Binding();
            b.Source = person;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            b.Path = new PropertyPath("LastName");
            this.lastNameTextBox.SetBinding(TextBox.TextProperty, b);

            b = new Binding();
            b.Source = person;
            b.Path = new PropertyPath("FullName");
            this.fullNameTextBlock.SetBinding(TextBlock.TextProperty, b);
        }

        #endregion // BindInCode        

        #region BindInXaml

        private void BindInXaml()
        {
            DataContext = new Person
            {
                FirstName = "Josh",
                LastName = "Smith"
            };
        }

        #endregion // BindInXaml        
    }
}