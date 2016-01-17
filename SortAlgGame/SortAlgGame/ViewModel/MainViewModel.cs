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
                return new RelayCommand(action => menueChange());
            }
        }
        
        public void menueChange()
        {
            if(CurrentView is GameVM)
            {
                (CurrentView as GameVM).stopTimer();
            }
            CurrentView = new HauptmenueViewModel();
        }

        public ICommand changeToGame
        {
            get
            {
                return new RelayCommand(action => CurrentView = new GameVM(this));
            }
        }

        public ICommand changeToBubble
        {
            get
            {
                return new RelayCommand(action => CurrentView = new SortVM("BubbleSort"));
            }
        }

        public ICommand changeToQuick
        {
            get
            {
                return new RelayCommand(action => CurrentView = new SortVM("QuickSort"));
            }
        }

        public ICommand changeToSelection
        {
            get
            {
                return new RelayCommand(action => CurrentView = new SortVM("SelectionSort"));
            }
        }

        public ICommand changeToInsertion
        {
            get
            {
                return new RelayCommand(Action => CurrentView = new SortVM("InsertionSort"));
            }
        }
    }
}
