using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp_Part3_POE
{
    // RelayCommand class that implements the ICommand interface to handle command logic
    public class RelayCommand : ICommand
    {
        // Declaring variables
        private readonly Action<object> execute; // Action to execute when the command is invoked
        private readonly Predicate<object> canExecute; // Predicate to determine if the command can execute

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            // Ensuring the execute action is not null
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method determines if the command can execute
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method executes the command
        public void Execute(object parameter)
        {
            execute(parameter);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//