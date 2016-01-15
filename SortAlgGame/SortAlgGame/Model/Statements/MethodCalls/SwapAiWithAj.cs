using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class SwapAiWithAj : Statement
    {
        public SwapAiWithAj(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "swap(a, i, j);";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED || actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            try
            {
                int tmp = actDataSet.A[actDataSet.I];
                actDataSet.A[actDataSet.I] = actDataSet.A[actDataSet.J];
                actDataSet.A[actDataSet.J] = tmp;
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
