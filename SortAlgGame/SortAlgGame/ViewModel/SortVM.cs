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
        protected Player _programm;

        public AnimationVM AnimationVM
        {
            get { return _animationVM; }
        }

        public SortVM()
        {
            _arrayGen = new ArrayGen();
            _testArray = _arrayGen.getRndArray(Config.RUNS[0]);
            _programm = new Player();
        }

        public void runAnimation()
        {
            _programm.execute(_testArray, true);
            _animationVM = new AnimationVM(_programm);
        }
    }
}
