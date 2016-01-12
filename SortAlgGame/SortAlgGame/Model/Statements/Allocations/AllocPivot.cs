using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocPivot : Statement
    {
        public AllocPivot(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "pivot = a[(int)((left+right)/2)];";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            actDataSet.Pivot = actDataSet.A[(int)((actDataSet.Left + actDataSet.Right)/2)];
            if (buildLog) updateLog();
            return null;
        }
    }
}
