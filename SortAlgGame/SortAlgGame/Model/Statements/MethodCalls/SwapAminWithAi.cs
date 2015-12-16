using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class SwapAminWithAi : Statement
    {
        public SwapAminWithAi(Player player, Statement parent)
            : base(player, parent)
        {
            content = "swap (a, min, i);";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.Min != Config.NOTUSED && actDataSet.I != Config.NOTUSED)
            {
                int tmp = actDataSet.A[actDataSet.Min];
                actDataSet.A[actDataSet.Min] = actDataSet.A[actDataSet.I];
                actDataSet.A[actDataSet.I] = tmp;
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
