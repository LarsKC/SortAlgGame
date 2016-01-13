using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.ViewModel
{
    class QuickSortVM : SortVM
    {
        public QuickSortVM()
            : base()
        {
            _programm.buildQuicksort();
            runAnimation();
        }
    }
}
