using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SortAlgGame.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel currentView;

        public MainViewModel()
        {
            currentView = new HauptmenueViewModel();
        }

        public BaseViewModel CurrentView
        {
            get { return currentView; }
            set
            {
                if (currentView != value)
                {
                    currentView = value;
                    NotifyPropertyChanged("CurrentView");
                }
            }
        }

        //TODO: Methoden zum switchen der View
        public ICommand changeToHauptmenue
        {
            get
            {
                return new RelayCommand(action => CurrentView = new HauptmenueViewModel());
            }
        }
    }
}
