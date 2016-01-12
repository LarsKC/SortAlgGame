using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.ViewModel
{
    class SelectionSortVM : AnimationVM
    {
        public SelectionSortVM()
            : base()
        {
            _programm.buildSelectionsort();
            initAnimation();
        }
    }
}
