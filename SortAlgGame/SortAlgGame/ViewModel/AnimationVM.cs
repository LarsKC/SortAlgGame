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
    /// Die Klasse AnimationVM stellt die Daten fuer die Animation zur Verfuegung. Sie erbt von der Klasse NotifyChangeBase, um der GUI
    /// Aenderungen mitteilen zu koennen.
    /// </summary>
    class AnimationVM : NotifyChangeBase
    {
        #region Member
        /// <summary>
        /// Daten fuer das Balkendiagramm (3-Tuple): Rechteckhoehe, Zahlenwert des Feldes im Array, Liste von Pointern die auf dieses Feld 
        /// zeigen.
        /// </summary>
        private ObservableCollection<Tuple<int, int, ObservableCollection<string>>> _animationData;
        /// <summary>
        /// Vom Algorithmus benutze Variablen und deren Belegung. 
        /// </summary>
        private ObservableCollection<string> _dataSetValues;
        /// <summary>
        /// Auflistung der Bausteine und des Bausteinstexts.
        /// </summary>
        private ObservableCollection<Tuple<Statement, String>> _stms;
        /// <summary>
        /// Referenz auf das Programm, was in der Animation dargestellt werden soll.
        /// </summary>
        private Programm _programm;
        /// <summary>
        /// Aktuelle Position im Log des Programms.
        /// </summary>
        private Tuple<Statement, DataSet> _curLogSet;
        /// <summary>
        /// Timer um die Animation automatisch zu durchlaufen.
        /// </summary>
        private DispatcherTimer _timer;
        /// <summary>
        /// Maximale Hoehe der Animation.
        /// </summary>
        private int _maxAnimationHeight;
        #endregion

        #region Accessoren & Commands
        /// <summary>
        /// Befehl zum Vorspulen der Animation.
        /// </summary>
        public Command animationForward
        {
            get
            {
                return new Command(action => logForward(false));
            }
        }
        /// <summary>
        /// Befehl zum Zurueckspulen der Animation
        /// </summary>
        public Command animationBackward
        {
            get
            {
                return new Command(action => logBackwards());
            }
        }
        /// <summary>
        /// Befehl zum Starten der Animation
        /// </summary>
        public Command animationStart
        {
            get
            {
                return new Command(action => logPlay());
            }
        }
        /// <summary>
        /// Befehl zum Stoppen der Animation
        /// </summary>
        public Command animationStop
        {
            get
            {
                return new Command(action => logStop());
            }
        }
        /// <summary>
        /// _maxAnimationHeight Accessor
        /// </summary>
        public int MaxAnimationHeight
        {
            get { return _maxAnimationHeight; }
            set
            {
                _maxAnimationHeight = value;
                NotifyPropertyChanged("MaxAnimationHeight");
            }
        }
        /// <summary>
        /// _dataSetValues Accessor
        /// </summary>
        public ObservableCollection<string> DataSetValues
        {
            get { return _dataSetValues; }
            set
            {
                _dataSetValues = value;
                NotifyPropertyChanged("DataSetValue");
            }
        }
        /// <summary>
        /// _animationData Accessor
        /// </summary>
        public ObservableCollection<Tuple<int, int, ObservableCollection<string>>> AnimationData
        {
            get { return _animationData; }
            set
            {
                _animationData = value;
                NotifyPropertyChanged("AnimationData");
            }
        }
        /// <summary>
        /// _stms Accessor
        /// </summary>
        public ObservableCollection<Tuple<Statement, String>> Stms
        {
            get { return _stms; }
            set
            {
                _stms = value;
                NotifyPropertyChanged("Stms");
            }
        }
        /// <summary>
        /// _programm Accessor
        /// </summary>
        public Programm Programm
        {
            get { return _programm; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="prg">Programm, dass die Animation darstellen soll.</param>
        public AnimationVM(Programm prg)
        {
            _maxAnimationHeight = Config.RUNS[0] * Config.RECT_MULTIPLIKATOR + 80;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0 , 0, Config.ANIMATION_TIMER);
            _timer.Tick += new EventHandler(animationEvent);
            _programm = prg;
            _curLogSet = _programm.Log.First.Value;
            _animationData = new ObservableCollection<Tuple<int,int,ObservableCollection<string>>>();
            _dataSetValues = new ObservableCollection<string>();
            updateAnimationData();
            updateDataSetValues();
            initStms();

        }
        #endregion

        #region Method
        /// <summary>
        /// Laed den naechsten Schritt des Sortierablaufs.
        /// </summary>
        /// <param name="autom"></param>
        private void logForward(bool autom)
        {
            if (!autom)
            {
                _timer.Stop();
            }
            _curLogSet = (_programm.Log.Find(_curLogSet).Next != null) ? _programm.Log.Find(_curLogSet).Next.Value: _programm.Log.First.Value;
            update();
        }
        /// <summary>
        /// Laed den vorherigen Schritt des Sortierablaufs.
        /// </summary>
        private void logBackwards()
        {
            _timer.Stop();
            _curLogSet = (_programm.Log.Find(_curLogSet).Previous != null) ? _programm.Log.Find(_curLogSet).Previous.Value : _programm.Log.Last.Value;
            update();
        }
        /// <summary>
        /// Startet den automatisierten Ablauf der Animation.
        /// </summary>
        private void logPlay()
        {
            _timer.Start();
        }
        /// <summary>
        /// Stoppt den automatisierten Ablauf der Animation
        /// </summary>
        public void logStop()
        {
            _timer.Stop();
        }
        /// <summary>
        /// Wird durch den _timer ausgeloest. Laed den naechsten Schritt der Animation.
        /// </summary>
        /// <param name="sender">Ausloeser des Events</param>
        /// <param name="e">Event</param>
        private void animationEvent(object sender, EventArgs e)
        {
            logForward(true);
        }
        /// <summary>
        /// Fuehrt die Aktualisierung der Animationsdaten durch.
        /// </summary>
        private void updateAnimationData()
        {
            int rectHeight;
            int fieldValue;
            AnimationData.Clear();
            for (int i = 0; i < _curLogSet.Item2.A.Length; i++)
            {
                fieldValue = _curLogSet.Item2.A[i];
                rectHeight = fieldValue * Config.RECT_MULTIPLIKATOR;
                ObservableCollection<string> pointer = new ObservableCollection<string>();
                if (_curLogSet.Item2.I == i) pointer.Add("i");
                if (_curLogSet.Item2.J == i) pointer.Add("j");
                if (_curLogSet.Item2.N == i) pointer.Add("n"); 
                AnimationData.Add(new Tuple<int,int,ObservableCollection<string>>(fieldValue, rectHeight, pointer));
            }
        }
        /// <summary>
        /// Initialisiert den Pseudocode
        /// </summary>
        private void initStms()
        {
            ObservableCollection<Statement> tmpCol = new ObservableCollection<Statement>(_programm.getActualStmNesting());
            _stms = new ObservableCollection<Tuple<Statement, string>>();
            _stms.Add(new Tuple<Statement,string>(tmpCol.ElementAt(0), Config.TEXT_RED));
            for (int i = 1; i < tmpCol.Count; i++)
            {
                _stms.Add(new Tuple<Statement, string>(tmpCol.ElementAt(i), Config.TEXT_NORMAL));
            }
        }

        /// <summary>
        /// Aktualisiert die Schriftfarbe des Pseudocodes.
        /// </summary>
        /// <param name="s"></param>
        private void updateFontStyle(Statement s)
        {
            bool foundOldStm = false;
            bool foundNewStm = false;
            int indexOld = Config.NOT_USED;
            int indexNew = Config.NOT_USED;
            foreach(Tuple<Statement, string> x in _stms)
            {
                if(x.Item2 == Config.TEXT_RED)
                {
                    indexOld = _stms.IndexOf(x);
                    foundOldStm = true;
                }
                if(x.Item1 == s)
                {
                    indexNew = _stms.IndexOf(x);
                    foundNewStm = true;
                }
                if(foundOldStm && foundNewStm)
                {
                    break;
                }
            }
            if (indexNew != Config.NOT_USED)
            {
                Tuple<Statement, string> tmpTuple = _stms.ElementAt(indexNew);
                _stms.Insert(indexNew, new Tuple<Statement, string>(_stms.ElementAt<Tuple<Statement, string>>(indexNew).Item1, Config.TEXT_RED));
                _stms.Remove(tmpTuple);
            }

            if (indexOld != Config.NOT_USED)
            {
                Tuple<Statement, string> tmpTuple = _stms.ElementAt(indexOld);
                _stms.Insert(indexOld, new Tuple<Statement, string>(_stms.ElementAt(indexOld).Item1, Config.TEXT_NORMAL));
                _stms.Remove(tmpTuple);
            }
        }

        /// <summary>
        /// Methode zum gebuendelten Aufrufen der Updatemethoden.
        /// </summary>
        private void update()
        {
            updateAnimationData();
            updateFontStyle(_curLogSet.Item1);
            updateDataSetValues();
        }
        /// <summary>
        /// Aktualisiert die ObservableCollection _dataSetValues.
        /// </summary>
        private void updateDataSetValues()
        {
            DataSetValues.Clear();
            if (_curLogSet.Item2.I != Config.NOT_USED) DataSetValues.Add("i = " + _curLogSet.Item2.I);
            if (_curLogSet.Item2.J != Config.NOT_USED) DataSetValues.Add("j = " + _curLogSet.Item2.J);
            if (_curLogSet.Item2.N != Config.NOT_USED) DataSetValues.Add("n = " + _curLogSet.Item2.N);
            if (_curLogSet.Item2.Left != Config.NOT_USED) DataSetValues.Add("left = " + _curLogSet.Item2.Left);
            if (_curLogSet.Item2.Right != Config.NOT_USED) DataSetValues.Add("right = " + _curLogSet.Item2.Right);
            if (_curLogSet.Item2.Min != Config.NOT_USED) DataSetValues.Add("min = " + _curLogSet.Item2.Min);
            if (_curLogSet.Item2.Pivot != Config.NOT_USED) DataSetValues.Add("pivot = " + _curLogSet.Item2.Pivot);
        }
        #endregion
    }
}
