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

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOTUSED)
            {
                actDataSet.I++;
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
            }
            else
            {
                //TODO ExceptionHAndling
            }
        }
    }
}
