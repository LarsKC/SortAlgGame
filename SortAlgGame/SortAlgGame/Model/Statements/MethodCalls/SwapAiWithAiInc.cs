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

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            try
            {
                int tmp = actDataSet.A[actDataSet.I];
                actDataSet.A[actDataSet.I] = actDataSet.A[actDataSet.I + 1];
                actDataSet.A[actDataSet.I + 1] = tmp;
            }
            catch (IndexOutOfRangeException e)
            {
                return Config.OUT_OF_RANGE_ERROR;
            }
            if (buildLog) updateLog();
            return null;
        }
    }
}
