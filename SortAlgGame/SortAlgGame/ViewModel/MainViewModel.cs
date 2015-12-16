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

        public ICommand changeToErklaerung
        {
            get
            {
                return new RelayCommand(action => CurrentView = new MenueErklaerungViewModel());
            }
        }

        public ICommand changeToGame
        {
            get
            {
                return new RelayCommand(action => CurrentView = new GameVM());
            }
        }

        public ICommand changeToBubble
        {
            get
            {
                return new RelayCommand(action => CurrentView = new BubbleSortVM());
            }
        }
    }
}
