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
    class ErklaerungVM : NotifyChangeBase
    {
        protected AnimationVM _animationVM;
        private ArrayGen _arrayGen;
        private int[] _testArray;
        private string _infoText;
        private string _sortName;
        protected Programm _programm;

        public string SortName
        {
            get { return _sortName; }
            set
            {
                _sortName = value;
                NotifyPropertyChanged("SortName");
            }
        }

        public AnimationVM AnimationVM
        {
            get { return _animationVM; }
        }

        public string InfoText
        {
            get { return _infoText; }
        }

        public ErklaerungVM(string sortAlg)
        {
            _arrayGen = new ArrayGen();
            _testArray = _arrayGen.getRndArray(Config.RUNS[0]);
            _programm = new Programm();
            switchOnAlg(sortAlg);
            runAnimation();
        }

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

        public void runAnimation()
        {
            _programm.execute(_testArray, true);
            _animationVM = new AnimationVM(_programm);
        }
    }
}
