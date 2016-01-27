using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace SortAlgGame.ViewModel
{
    /// <summary>
    /// Die Klasse NotifyChangeBase Implementiert das Interface INotifyPropertyChanged und stellt eine Methode 
    /// zur Verfuegung um ein PropertyChanged Event auszuloesen. 
    /// </summary>
    abstract class NotifyChangeBase : INotifyPropertyChanged
    {
        #region Member
        /// <summary>
        /// Event Handler zur Benachrichtigung ueber Aenderungen von Variablen.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methoden
        /// <summary>
        /// Die Methode ermoeglicht das Erstellen eines neuen PropertyChanged Events.
        /// </summary>
        /// <param name="property">Name der aktualisierten Variable.</param>
        public void NotifyPropertyChanged(String property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
