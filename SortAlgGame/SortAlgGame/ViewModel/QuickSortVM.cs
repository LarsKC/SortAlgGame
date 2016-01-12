using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.ViewModel
{
    class QuickSortVM : AnimationVM
    {
        public QuickSortVM()
            : base()
        {
            _programm.buildQuicksort();
            initAnimation();
        }
    }
}
