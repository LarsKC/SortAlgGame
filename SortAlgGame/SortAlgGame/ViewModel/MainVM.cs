﻿using System;
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
        public Command changeToHauptmenue
        {
            get
            {
                return new Command(action => toHauptmenue());
            }
        }
        
        public void toHauptmenue()
        {
            if(CurrentView is GameVM)
            {
                (CurrentView as GameVM).stopTimer();
            }
            if (CurrentView is ErklaerungVM)
            {
                (CurrentView as ErklaerungVM).AnimationVM.logStop();
            }
            if (CurrentView is ResultVM)
            {
                (CurrentView as ResultVM).P1Animation.logStop();
                (CurrentView as ResultVM).P2Animation.logStop();
            }
            CurrentView = new HauptmenueVM();
        }

        public Command changeToGame
        {
            get
            {
                return new Command(action => CurrentView = new GameVM(this));
            }
        }

        public Command changeToBubble
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("BubbleSort"));
            }
        }

        public Command changeToQuick
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("QuickSort"));
            }
        }

        public Command changeToSelection
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("SelectionSort"));
            }
        }

        public Command changeToInsertion
        {
            get
            {
                return new Command(Action => CurrentView = new ErklaerungVM("InsertionSort"));
            }
        }
    }
}
