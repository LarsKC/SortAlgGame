using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SortAlgGame.ViewModel
{
    /// <summary>
    /// Die Klasse MainVM ist an die SurfaceWindow1 View gebunden. In ihr befinden sich die Commands zum Navigieren durch 
    /// die Benutzersteuerelemente. Sie erbt von der Klasse NotifyChangeBase, um der GUI Aenderungen mitteilen zu koennen.
    /// </summary>
    class MainVM : NotifyChangeBase
    {
        #region Member
        /// <summary>
        /// Beinhaltet das Benutzersteuerelement, das in der SurfaceWindow1 View angezeigt werden soll.
        /// </summary>
        private NotifyChangeBase currentView;
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor
        /// </summary>
        public MainVM()
        {
            currentView = new HauptmenueVM();
        }
        #endregion

        #region Accessoren & Commands
        /// <summary>
        /// _currentView Accessor
        /// </summary>
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

        /// <summary>
        /// Befehl, um zum Hauptmenue zu navigieren.
        /// </summary>
        public Command changeToHauptmenue
        {
            get
            {
                return new Command(action => toHauptmenue());
            }
        }
        /// <summary>
        /// Befehl, um zum Spiel zu navigieren.
        /// </summary>
        public Command changeToGame
        {
            get
            {
                return new Command(action => CurrentView = new GameVM(this));
            }
        }
        /// <summary>
        /// Befehl, um zur Bubblesort Erklaerung zu navigieren.
        /// </summary>
        public Command changeToBubble
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("BubbleSort"));
            }
        }
        /// <summary>
        /// Befehl, um zur Quicksort Erklaerung zu navigieren.
        /// </summary>
        public Command changeToQuick
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("QuickSort"));
            }
        }
        /// <summary>
        /// Befehl, um zur Selectionsort Erklaerung zu navigieren.
        /// </summary>
        public Command changeToSelection
        {
            get
            {
                return new Command(action => CurrentView = new ErklaerungVM("SelectionSort"));
            }
        }
        /// <summary>
        /// Befehl, um zur Insertionsort Erklaerung zu navigieren.
        /// </summary>
        public Command changeToInsertion
        {
            get
            {
                return new Command(Action => CurrentView = new ErklaerungVM("InsertionSort"));
            }
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Deaktiviert laufende Timer vor dem wechsel zum Hauptmenue.
        /// </summary>
        private void toHauptmenue()
        {
            if (CurrentView is GameVM)
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
        #endregion
    }
}
