using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocALength : Statement
    {
        public AllocALength(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "n = right +1;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            actDataSet.N = actDataSet.Right +1 ;
            if (buildLog) updateLog();
            return null;
        }
    }
}
