using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.ViewModel
{
    class InsertionSortVM : SortVM
    {
        public InsertionSortVM() : base()
        {
            _programm.buildInsertionsort();
            runAnimation();
        }
    }
}
