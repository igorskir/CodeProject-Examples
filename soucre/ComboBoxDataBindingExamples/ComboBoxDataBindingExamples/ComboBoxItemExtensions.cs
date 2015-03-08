using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComboBoxDataBindingExamples;

// These extentions are a very important piece to getting the ComboBox item
// binding to work correctly. Data binding can only be set on Dependency 
// properties and data properties. You CANNOT bind to a value or a method. 
// In addition the data type of the object you are binding from MUST match 
// the data type of the oject you are binding to. I.e. a string and only binds
// to a string, an int32 can only binds to an int32. In fact an int CANNOT 
// even be bound to an int64.
namespace ComboBoxDataBindingExamples
{
    /// <summary>
    /// This class provides us with an object to fill a ComboBox with
    /// that can be bound to string fields in the binding object.
    /// </summary>
    public class ComboBoxItemString
    {
        public string ValueString { get; set; }
    }

    /// <summary>
    /// This class provides us with an object to fill a ComboBox with
    /// that can be bound to 'ViewModelEnum.Colors' enum fields in the binding
    ///  object while displaying a string values in the to user in the ComboBox.
    /// </summary>
    public class ComboBoxItemColor
    {
        public ViewModelEnum.Colors ValueColorEnum { get; set; }
        public string ValueColorString { get; set; }
    }
}
