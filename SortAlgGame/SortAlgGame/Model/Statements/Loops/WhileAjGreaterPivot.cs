using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class WhileAjGreaterPivot : ListStm
    {
        public WhileAjGreaterPivot(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "while (a[j] > pivot) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.J == Config.NOT_USED || actDataSet.Pivot == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            programm.Stack.Push(new DataSet(actDataSet));
            actDataSet = programm.Stack.Peek();
            string tmpError = null;
            try
            {
                while (actDataSet.A[actDataSet.J] > actDataSet.Pivot)
                {
                    programm.ActRuntime++;
                    if (programm.ActRuntime >= Config.MAX_RUNTIME(actDataSet.A.Length)) return Config.MAX_RUNTIME_ERROR;
                    if (buildLog) updateLog();
                    tmpError = executeList(buildLog);
                    if (tmpError != null) return tmpError;
                    actDataSet = programm.Stack.Peek();
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return Config.OUT_OF_RANGE_ERROR;
            }
            if (buildLog) updateLog();
            updateDataSets();
            return tmpError;
        }
    }
}
