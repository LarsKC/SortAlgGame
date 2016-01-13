using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocIToJ : Statement
    {
        public AllocIToJ(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "j = i;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOT_USED)
            {
                actDataSet.J = actDataSet.I;
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
