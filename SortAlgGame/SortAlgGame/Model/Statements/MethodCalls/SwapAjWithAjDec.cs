using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class SwapAjWithAjDec : Statement
    {
        public SwapAjWithAjDec(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "swap (a, j, j-1);";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J != Config.NOTUSED && actDataSet.I != Config.NOTUSED)
            {
                int tmp = actDataSet.A[actDataSet.J];
                actDataSet.A[actDataSet.J] = actDataSet.A[actDataSet.J-1];
                actDataSet.A[actDataSet.J - 1] = tmp;
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
