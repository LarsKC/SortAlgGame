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

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J == Config.NOTUSED) return Config.NOTINITERROR;
            try
            {
                int tmp = actDataSet.A[actDataSet.J];
                actDataSet.A[actDataSet.J] = actDataSet.A[actDataSet.J - 1];
                actDataSet.A[actDataSet.J - 1] = tmp;
            }
            catch (IndexOutOfRangeException e)
            {
                return Config.OUTOFRANGEERROR;
            }
            if (buildLog) updateLog();
            return null;
        }
    }
}
