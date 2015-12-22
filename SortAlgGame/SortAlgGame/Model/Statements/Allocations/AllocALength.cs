using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocALength : Statement
    {
        public AllocALength(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "int n = right +1;";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            actDataSet.N = actDataSet.Right;
            //TODO LOG + RUNTIME
            if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
        }
    }
}
