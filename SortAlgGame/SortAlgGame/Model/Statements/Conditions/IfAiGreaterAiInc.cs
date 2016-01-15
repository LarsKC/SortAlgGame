using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfAiGreaterAiInc : ListStm
    {
        public IfAiGreaterAiInc(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "if (a[i] > a[i+1]) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            programm.Stack.Push(new DataSet(actDataSet));
            actDataSet = programm.Stack.Peek();
            if (buildLog) updateLog();
            string tmpError = null;
            try
            {
                if (actDataSet.A[actDataSet.I] > actDataSet.A[actDataSet.I + 1])
                {
                    tmpError = executeList(buildLog);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return Config.OUT_OF_RANGE_ERROR;
            }
            updateDataSets();
            return tmpError;
        }
    }
}
