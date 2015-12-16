using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocJToMin : Statement
    {
        public AllocJToMin(Player player, Statement parent)
            : base(player, parent)
        {
            content = "min = j;";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J != Config.NOTUSED)
            {
                actDataSet.Min = actDataSet.J;
                //TODO LOG + RUNTIME
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
            }
            else
            {
                //TODO ExceptionHandling
            }
        }
    }
}
