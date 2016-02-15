using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace eDirectory.WPF.Core
{
    public class ViewModelBase
        :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { return; };

        protected void RaisePropertyChanged(string propertyName)
        {
            VerifyPropertyExists(propertyName);
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("expression must be a property expression");

            RaisePropertyChanged(memberExpression.Member.Name);
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyExists(string propertyName)
        {
            PropertyInfo currentProperty = GetType().GetProperty(propertyName);
            string message = string.Format("Property Name \"{0}\" does not exist in {1}", propertyName, GetType());
            Debug.Assert(currentProperty != null, message);
        }
    }
}
