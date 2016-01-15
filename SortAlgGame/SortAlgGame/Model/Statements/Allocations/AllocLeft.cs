using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocLeft : Statement
    {
        public AllocLeft(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "i = left";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            actDataSet.I = actDataSet.Left;
            if (buildLog) updateLog();
            return null;
        }
    }
}
