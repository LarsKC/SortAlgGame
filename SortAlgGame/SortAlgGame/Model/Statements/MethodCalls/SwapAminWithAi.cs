using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class SwapAminWithAi : Statement
    {
        public SwapAminWithAi(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "swap (a, min, i);";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.Min == Config.NOT_USED || actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            try
            {
                int tmp = actDataSet.A[actDataSet.Min];
                actDataSet.A[actDataSet.Min] = actDataSet.A[actDataSet.I];
                actDataSet.A[actDataSet.I] = tmp;
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
