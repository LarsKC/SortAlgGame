using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocLeft : Statement
    {
        public AllocLeft(Player player, Statement parent)
            : base(player, parent)
        {
            content = "int i = left";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            actDataSet.I = actDataSet.Left;
            //TODO LOG + RUNTIME
            if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
        }
    }
}
