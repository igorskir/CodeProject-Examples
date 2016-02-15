
using System;
namespace WPFTreeViewDemo
{
    class EmployeeTreeModel : DemoBaseTreeModel
    {
        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged("Position");
            }
        }

        private DateTime _hireDate;
        public DateTime HireDate
        {
            get { return _hireDate; }
            set
            {
                _hireDate = value;
                RaisePropertyChanged("HireDate");
            }
        }

    }
}
