using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocPivot : Statement
    {
        public AllocPivot(Player player, Statement parent)
            : base(player, parent)
        {
            content = "int pivot = a[(int)((left+right)/2)];";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            actDataSet.Pivot = actDataSet.A[(int)((actDataSet.Left + actDataSet.Right)/2)];
            //TODO LOG + RUNTIME
            if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
        }
    }
}
