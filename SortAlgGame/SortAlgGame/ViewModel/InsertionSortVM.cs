using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.ViewModel
{
    class InsertionSortVM : AnimationVM
    {
        public InsertionSortVM()
            : base()
        {
            _programm.buildInsertionsort();
            initAnimation();
        }
    }
}
