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
    class SortVM : BaseViewModel
    {
        protected AnimationVM _animationVM;
        private ArrayGen _arrayGen;
        private int[] _testArray;
        private string _infoText;
        protected Programm _programm;

        public AnimationVM AnimationVM
        {
            get { return _animationVM; }
        }

        public string InfoText
        {
            get { return _infoText; }
        }

        public SortVM(string sortAlg)
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
                    break;
                case "InsertionSort":
                    _programm.buildInsertionsort();
                    _infoText = Config.INFO_INSERTION;
                    break;
                case "SelectionSort":
                    _programm.buildSelectionsort();
                    _infoText = Config.INFO_SELECTION;
                    break;
                case "QuickSort":
                    _programm.buildQuicksort();
                    _infoText = Config.INFO_QUICK;
                    break;
                default:
                    //NOTHING
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
