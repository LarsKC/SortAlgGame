using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class SwapAminWithAi : Statement
    {
        public SwapAminWithAi(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "swap (a, min, i);";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.Min == Config.NOTUSED || actDataSet.I == Config.NOTUSED) return Config.NOTINITERROR;
            try
            {
                int tmp = actDataSet.A[actDataSet.Min];
                actDataSet.A[actDataSet.Min] = actDataSet.A[actDataSet.I];
                actDataSet.A[actDataSet.I] = tmp;
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
