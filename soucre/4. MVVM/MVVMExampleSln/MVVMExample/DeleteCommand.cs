using System;
using System.Windows;
using System.Windows.Input;

namespace MVVMExample
{
    public class DeleteCommand : ICommand 
    {
        private readonly IService _service;
        private readonly Func<bool> _canExecute;
        private readonly Action<ContactModel> _deleted;

        public DeleteCommand(IService service, Func<bool> canExecute, Action<ContactModel> deleted)
        {
            _service = service;
            _canExecute = canExecute;
            _deleted = deleted;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var contact = parameter as ContactModel; 
                if (contact != null)
                {
                    var result = MessageBox.Show("Are you sure you wish to delete the contact?",
                                                              "Confirm Delete", MessageBoxButton.OKCancel);

                    if (result.Equals(MessageBoxResult.OK))
                    {
                        _service.DeleteContact(contact);
                        if (_deleted != null)
                        {
                            _deleted(contact);
                        }
                    }
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
