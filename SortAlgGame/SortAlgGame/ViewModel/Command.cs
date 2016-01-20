using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SortAlgGame.ViewModel
{
    public class Command : ICommand
    {

        // The action to execute.
        private readonly Action<object> execute;

        // The Predicate to indicate wether this command can execure or not.
        private readonly Predicate<object> canExecute;

        /// Initializes a new instance of the <see cref="Command"/> class.
        public Command(Action<object> execute)
            : this(execute, null)
        {
        }

        // Initializes a new instance of the <see cref="RelayCommand"/> class.
        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        // Defines the method that determines whether the command can execute in its current state.
        // <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        // <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }

            return canExecute(parameter);
        }

        // Occurs when changes occur that affect whether or not the command should execute.
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Defines the method to be called when the command is invoked.
        // <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
