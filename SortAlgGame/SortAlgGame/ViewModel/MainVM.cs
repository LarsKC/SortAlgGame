using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SortAlgGame.ViewModel
{
    class MainVM : NotifyChangeBase
    {
        private NotifyChangeBase currentView;

        public MainVM()
        {
            currentView = new HauptmenueVM();
        }

        public NotifyChangeBase CurrentView
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
                return new Command(action => menueChange());
            }
        }
        
        public void menueChange()
        {
            if(CurrentView is GameVM)
            {
                (CurrentView as GameVM).stopTimer();
            }
            CurrentView = new HauptmenueVM();
        }

        public ICommand changeToGame
        {
            get
            {
                return new Command(action => CurrentView = new GameVM(this));
            }
        }

        public ICommand changeToBubble
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("BubbleSort"));
            }
        }

        public ICommand changeToQuick
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("QuickSort"));
            }
        }

        public ICommand changeToSelection
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("SelectionSort"));
            }
        }

        public ICommand changeToInsertion
        {
            get
            {
                return new Command(Action => CurrentView = new ErklaerungVM("InsertionSort"));
            }
        }
    }
}
