using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class SwapAiWithAiInc : Statement
    {
        public SwapAiWithAiInc(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "swap(a, i, i+1);";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();

            if (actDataSet.I != Config.NOTUSED)
            {
                int tmp = actDataSet.A[actDataSet.I];
                actDataSet.A[actDataSet.I] = actDataSet.A[actDataSet.I + 1];
                actDataSet.A[actDataSet.I + 1] = tmp;
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
            }
            else
            {
                //TODO ExceptionHandling
            }
        }
    }
}
