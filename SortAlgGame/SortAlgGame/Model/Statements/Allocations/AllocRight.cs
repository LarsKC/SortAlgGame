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
            content = "int j = right;";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            actDataSet.J = actDataSet.Right;
            //TODO LOG + RUNTIME
            if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
        }
    }
}
