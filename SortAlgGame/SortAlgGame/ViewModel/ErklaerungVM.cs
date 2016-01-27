using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SortAlgGame.Model;
using SortAlgGame.Model.Statements;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using System.Windows.Threading;

namespace SortAlgGame.ViewModel
{
    /// <summary>
    /// Stellt die Daten fuer die Algorithmus Erklaerungen zur Verfuegung. Sie erbt von der Klasse NotifyChangeBase, um der GUI 
    /// Aenderungen mitteilen zu koennen.
    /// </summary>
    class ErklaerungVM : NotifyChangeBase
    {
        #region Member
        /// <summary>
        /// Referenz auf ein AnimationVM Objekt, ueber welches die Daten fuer die Animation bereitgestellt werden.
        /// </summary>
        protected AnimationVM _animationVM;
        /// <summary>
        /// Referenz auf ein ArrayGen Objekt zum erzeugen von Zahlenfolgen.
        /// </summary>
        private ArrayGen _arrayGen;
        /// <summary>
        /// Die auf den Algorithmus anzuwendene Zahlenfolge.
        /// </summary>
        private int[] _testArray;
        /// <summary>
        /// Informationstext des Algorithmus.
        /// </summary>
        private string _infoText;
        /// <summary>
        /// Algorithmus Bezeichnung.
        /// </summary>
        private string _sortName;
        /// <summary>
        /// Der Algorithmus und seine Speicherverwaltung.
        /// </summary>
        protected Programm _programm;
        #endregion

        #region Accessoren
        /// <summary>
        /// _sortName Accessor
        /// </summary>
        public string SortName
        {
            get { return _sortName; }
            set
            {
                _sortName = value;
                NotifyPropertyChanged("SortName");
            }
        }
        /// <summary>
        /// _animationVM Accessor
        /// </summary>
        public AnimationVM AnimationVM
        {
            get { return _animationVM; }
        }
        /// <summary>
        /// _infoText Accessor
        /// </summary>
        public string InfoText
        {
            get { return _infoText; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="sortAlg">Der anzuzeigende Algorithmus</param>
        public ErklaerungVM(string sortAlg)
        {
            _arrayGen = new ArrayGen();
            _testArray = _arrayGen.getRndArray(Config.RUNS[0]);
            _programm = new Programm();
            switchOnAlg(sortAlg);
            runAnimation();
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Bestimmt die anzuzeigenden Daten, die Algorithmus spezifisch sind: Infotext, Algorithmus Bezeichnung, Algorithmus im Objekt _programm.
        /// </summary>
        /// <param name="sortAlg">Algorithmus Bezeichnung</param>
        private void switchOnAlg(string sortAlg)
        {
            switch (sortAlg)
            {
                case "BubbleSort":
                    _programm.buildBubblesort();
                    _infoText = Config.INFO_BUBBLE;
                    _sortName = sortAlg;
                    break;
                case "InsertionSort":
                    _programm.buildInsertionsort();
                    _infoText = Config.INFO_INSERTION;
                    _sortName = sortAlg;
                    break;
                case "SelectionSort":
                    _programm.buildSelectionsort();
                    _infoText = Config.INFO_SELECTION;
                    _sortName = sortAlg;
                    break;
                case "QuickSort":
                    _programm.buildQuicksort();
                    _infoText = Config.INFO_QUICK;
                    _sortName = sortAlg;
                    break;
                default:
                    //Nothing
                    break;
            }
        }
        /// <summary>
        /// Initialisiert die Animation der Erklaerung.
        /// </summary>
        private void runAnimation()
        {
            _programm.execute(_testArray, true);
            _animationVM = new AnimationVM(_programm);
        }
        #endregion
    }
}
