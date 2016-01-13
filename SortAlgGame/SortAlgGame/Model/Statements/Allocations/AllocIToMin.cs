using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocIToMin : Statement
    {
        public AllocIToMin(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "min = i;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOT_USED)
            {
                actDataSet.Min = actDataSet.I;
                if (buildLog) updateLog();
                return null;
            }
            else
            {
                return "i wurde nicht initialisiert!";
            }

        }
    }
}
