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
    class BubbleSortVM : AnimationVM
    {
        public BubbleSortVM() : base()
        {
            _programm.buildBubblesort();
            initAnimation();
        }
    }
}
