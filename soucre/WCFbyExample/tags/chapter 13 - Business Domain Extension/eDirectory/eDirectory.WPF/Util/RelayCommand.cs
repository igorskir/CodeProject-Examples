using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace eDirectory.WPF.Util
{
    public class RelayCommand
        :ICommand
    {
               #region Fields

        readonly Action<object> ExecuteInstance;
        readonly Predicate<object> CanExecuteInstance;

        #endregion Fields

        #region Constructors

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }
        
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if(execute == null) throw new ArgumentNullException("execute");
            this.ExecuteInstance = execute;
            this.CanExecuteInstance = canExecute;
        }

        #endregion Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return this.CanExecuteInstance == null ? true : this.CanExecuteInstance(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.ExecuteInstance(parameter);
        }

        #endregion ICommand Members
    }
}
