using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class IncI : Statement
    {
        public IncI(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "i++;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I == Config.NOTUSED) return Config.NOTINITERROR;
            actDataSet.I++;
            if (buildLog) updateLog();
            return null;

        }
    }
}
