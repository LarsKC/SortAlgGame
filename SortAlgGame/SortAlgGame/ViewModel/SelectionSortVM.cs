using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.ViewModel
{
    class SelectionSortVM : SortVM
    {
        public SelectionSortVM()
            : base()
        {
            _programm.buildSelectionsort();
            runAnimation();
        }
    }
}
