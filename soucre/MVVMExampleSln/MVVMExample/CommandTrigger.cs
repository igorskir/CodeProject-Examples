using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MVVMExample
{
    public class CommandTrigger : TriggerAction<Button>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof (string),
                typeof (CommandTrigger),
                null);
        
        public string Command
        {
            get
            {
                return (string) GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);                
            }
        }

        protected override void Invoke(object parameter)
        {
            var dc = AssociatedObject.DataContext; 
            if (dc != null)
            {
                var commandProperty =
                    (from p in dc.GetType().GetProperties() where p.Name.Equals(Command) select p).FirstOrDefault();

                if (commandProperty != null)
                {
                    var command = commandProperty.GetValue(dc, null) as ICommand;                     

                    if (command != null && command.CanExecute(null))
                    {
                        command.Execute(((ContactViewModel)dc).CurrentContact);
                    }
                }
            }
        }
    }
}
