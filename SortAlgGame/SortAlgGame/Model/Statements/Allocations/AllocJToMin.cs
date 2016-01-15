using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocJToMin : Statement
    {
        public AllocJToMin(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "min = j;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.J != Config.NOT_USED)
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
