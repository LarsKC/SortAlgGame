using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SortAlgGame.ViewModel
{
    /// <summary>
    /// Ermoeglicht eine einfache Erstellung der Commands
    /// Basierend auf [Micd] Microsoft Corporation, Patterns - WPF Apps With The Model-View-ViewModel Design Pattern, https://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030 
    /// </summary>
    public class Command : ICommand
    {
        #region Member
        /// <summary>
        /// Auszufuehrende Aktion.
        /// </summary>
        private readonly Action<object> execute;
        /// <summary>
        /// Praedikat zum pruefen, ob die Ausfuehrung moeglich ist.
        /// </summary>
        private readonly Predicate<object> canExecute;
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="execute">Auszufuehrende Aktion</param>
        public Command(Action<object> execute)
            : this(execute, null)
        {
        }
        /// <summary>
        /// Konstruktor mit Praedikat
        /// </summary>
        /// <param name="execute">Auszufuehrende Aktion</param>
        /// <param name="canExecute">Praedikat. Bestimmt ob die Aktion ausgefuehrt werden kann.</param>
        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Bestimmt ob die Aktion des Commands ausgefuehrt werden kann/darf.
        /// </summary>
        /// <param name="parameter">Vom Command genutzte Parameter.</param>
        /// <returns>True, wenn die Ausfuehrung des Commands moeglich ist. False, wenn nicht.</returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }

            return canExecute(parameter);
        }
        /// <summary>
        /// Event das immer ausgeloest wird, wenn eine Aenderung stattfindet, die die Ausfuehrbarkeit des Commands beeinflusst.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        /// <summary>
        /// Die Methode wird immer dann ausgefuehrt, wenn das Command aufgerufen wird.
        /// </summary>
        /// <param name="parameter">Die vom Command benutzen Parameter.</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion
    }
}
