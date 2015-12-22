using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class SwapAiWithAj : Statement
    {
        public SwapAiWithAj(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "swap(a, i, j);";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOTUSED && actDataSet.J != Config.NOTUSED)
            {
                int tmp = actDataSet.A[actDataSet.I];
                actDataSet.A[actDataSet.I] = actDataSet.A[actDataSet.J];
                actDataSet.A[actDataSet.J] = tmp;
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
                //TODO LOG + RUNTIME
            }
            else
            {
                //TODO ExceptionHandling
            }

        }

    }
}
