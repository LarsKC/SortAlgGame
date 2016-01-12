using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocRight : Statement
    {
        public AllocRight(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "j = right;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            actDataSet.J = actDataSet.Right;
            if (buildLog) updateLog();
            return null;
        }
    }
}
