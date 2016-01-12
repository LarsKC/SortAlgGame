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
using SortAlgGame.Model.Statements.Loops;
using SortAlgGame.Model.Statements.Allocations;
using SortAlgGame.Model.Statements.Conditions;
using SortAlgGame.Model.Statements.MethodCalls;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using System.Windows.Threading;

namespace SortAlgGame.ViewModel
{
    class AnimationVM : BaseViewModel
    {
        protected ObservableCollection<Tuple<int, int, ObservableCollection<string>>> _animationData;
        protected ObservableCollection<string> _dataSetValues;
        protected ObservableCollection<Tuple<Statement, String>> _stms;
        protected Player _programm;
        protected ArrayGen _arrayGen;
        protected int[] _testArray;
        protected Tuple<Statement, DataSet> _curLogSet;
        protected DispatcherTimer _timer;
        protected int _maxAnimationHeight;

        public ICommand animationForward
        {
            get
            {
                return new RelayCommand(action => logForward(), predicate=> !_timer.IsEnabled);
            }
        }

        public ICommand animationBackward
        {
            get
            {
                return new RelayCommand(action => logBackwards(), predicate => !_timer.IsEnabled);
            }
        }

        public ICommand animationStart
        {
            get
            {
                return new RelayCommand(action => logPlay(), predicate => !_timer.IsEnabled);
            }
        }

        public ICommand animationStop
        {
            get
            {
                return new RelayCommand(action => logStop(), predicate => _timer.IsEnabled);
            }
        }

        public int MaxAnimationHeight
        {
            get { return _maxAnimationHeight; }
            set
            {
                _maxAnimationHeight = value;
                NotifyPropertyChanged("MaxAnimationHeight");
            }
        }

        public ObservableCollection<string> DataSetValues
        {
            get { return _dataSetValues; }
            set
            {
                _dataSetValues = value;
                NotifyPropertyChanged("DataSetValue");
            }
        }

        public ObservableCollection<Tuple<int, int, ObservableCollection<string>>> AnimationData
        {
            get { return _animationData; }
            set
            {
                _animationData = value;
                NotifyPropertyChanged("AnimationData");
            }
        }

        public ObservableCollection<Tuple<Statement, String>> Stms
        {
            get { return _stms; }
            set
            {
                _stms = value;
                NotifyPropertyChanged("Stms");
            }
        }

        public AnimationVM()
        {
            _maxAnimationHeight = Config.RUNS[0] * Config.RECTMULTIPLIKATOR + 200;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0 , 0, Config.ANIMATIONTIMER);
            _timer.Tick += new EventHandler(dispatcherAnimationTimer);
            _programm = new Player();
            _arrayGen = new ArrayGen();
            _testArray = _arrayGen.getRndArray(Config.RUNS[0]);
            _animationData = new ObservableCollection<Tuple<int,int,ObservableCollection<string>>>();
            _dataSetValues = new ObservableCollection<string>();
        }

        public void initAnimation()
        {
            _programm.execute(_testArray, true);
            _curLogSet = _programm.Log.First.Value;
            updateAnimationData();
            updateDataSetValues();
            initStms();
            //printExecute();
       
        }


        public void logForward()
        {
            _curLogSet = (_programm.Log.Find(_curLogSet).Next != null) ? _programm.Log.Find(_curLogSet).Next.Value: _programm.Log.First.Value;
            update();
        }

        public void logBackwards()
        {
            _curLogSet = (_programm.Log.Find(_curLogSet).Previous != null) ? _programm.Log.Find(_curLogSet).Previous.Value : _programm.Log.Last.Value;
            update();
        }

        public void logPlay()
        {
            _timer.Start();
        }

        public void logStop()
        {
            _timer.Stop();
        }

        private void dispatcherAnimationTimer(object sender, EventArgs e)
        {
            logForward();
        }

        public void updateAnimationData()
        {
            int rectHeight;
            int fieldValue;
            AnimationData.Clear();
            for (int i = 0; i < _curLogSet.Item2.A.Length; i++)
            {
                fieldValue = _curLogSet.Item2.A[i];
                rectHeight = fieldValue * Config.RECTMULTIPLIKATOR;
                ObservableCollection<string> pointer = new ObservableCollection<string>();
                if (_curLogSet.Item2.I == i) pointer.Add("i");
                if (_curLogSet.Item2.J == i) pointer.Add("j");
                if (_curLogSet.Item2.N == i) pointer.Add("n"); 
                AnimationData.Add(new Tuple<int,int,ObservableCollection<string>>(fieldValue, rectHeight, pointer));
            }
        }

        public void initStms()
        {
            ObservableCollection<Statement> tmpCol = new ObservableCollection<Statement>(_programm.getActualStmNesting());
            _stms = new ObservableCollection<Tuple<Statement, string>>();
            _stms.Add(new Tuple<Statement,string>(tmpCol.ElementAt(0), Config.textBold));
            for (int i = 1; i < tmpCol.Count; i++)
            {
                _stms.Add(new Tuple<Statement, string>(tmpCol.ElementAt(i), Config.textNormal));
            }
        }

        public void updateFontStyle(Statement s)
        {
            bool foundOldStm = false;
            bool foundNewStm = false;
            int indexOld = Config.NOTUSED;
            int indexNew = Config.NOTUSED;
            foreach(Tuple<Statement, string> x in _stms)
            {
                if(x.Item2 == Config.textBold)
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
            if (indexNew != Config.NOTUSED)
            {
                Tuple<Statement, string> tmpTuple = _stms.ElementAt(indexNew);
                _stms.Insert(indexNew, new Tuple<Statement, string>(_stms.ElementAt<Tuple<Statement, string>>(indexNew).Item1, Config.textBold));
                _stms.Remove(tmpTuple);
            }

            if (indexOld != Config.NOTUSED)
            {
                Tuple<Statement, string> tmpTuple = _stms.ElementAt(indexOld);
                _stms.Insert(indexOld, new Tuple<Statement, string>(_stms.ElementAt(indexOld).Item1, Config.textNormal));
                _stms.Remove(tmpTuple);
            }
        }

        public void update()
        {
            updateAnimationData();
            updateFontStyle(_curLogSet.Item1);
            updateDataSetValues();
        }

        public void updateDataSetValues()
        {
            DataSetValues.Clear();
            if (_curLogSet.Item2.I != Config.NOTUSED) DataSetValues.Add("i = " + _curLogSet.Item2.I);
            if (_curLogSet.Item2.J != Config.NOTUSED) DataSetValues.Add("j = " + _curLogSet.Item2.J);
            if (_curLogSet.Item2.N != Config.NOTUSED) DataSetValues.Add("n = " + _curLogSet.Item2.N);
            if (_curLogSet.Item2.Left != Config.NOTUSED) DataSetValues.Add("left = " + _curLogSet.Item2.Left);
            if (_curLogSet.Item2.Right != Config.NOTUSED) DataSetValues.Add("right = " + _curLogSet.Item2.Right);
            if (_curLogSet.Item2.Min != Config.NOTUSED) DataSetValues.Add("min = " + _curLogSet.Item2.Min);
            if (_curLogSet.Item2.Pivot != Config.NOTUSED) DataSetValues.Add("pivot = " + _curLogSet.Item2.Pivot);
        }

        public void printExecute()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Lars\Desktop\executePrint.txt");
            file.WriteLine(_programm.Stm.Content);
            rekursivePrint((_programm.Stm as ListStm).StmList, file);
            file.WriteLine("---------------------------------------------------------------------------------");
            foreach (int x in _programm.Stack.Peek().A)
            {
                file.Write(x);
            }
            file.Close();
        }

        public void rekursivePrint(LinkedList<Statement> stmList, System.IO.StreamWriter file)
        {
            foreach (Statement x in stmList)
            {
                file.WriteLine(x.Content);
                if (x is ListStm)
                {
                    rekursivePrint((x as ListStm).StmList, file);
                }
            }
        }
    }
}
