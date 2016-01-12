using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocJToMin : Statement
    {
        public AllocJToMin(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "min = j;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J != Config.NOTUSED)
            {
                actDataSet.Min = actDataSet.J;
                if (buildLog) updateLog();
                return null;
            }
            else
            {
                return "j wurde nicht initialisiert!";
            }
        }
    }
}
