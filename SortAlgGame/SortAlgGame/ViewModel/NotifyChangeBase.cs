using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace SortAlgGame.ViewModel
{
    abstract class NotifyChangeBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
