using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class ForOutBubble : ListStm
    {
        public ForOutBubble(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "for (int n = right+1; n > 1; n--) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            programm.Stack.Push(new DataSet(actDataSet));
            actDataSet = programm.Stack.Peek();
            string tmpError = null;
            for (int n = actDataSet.Right + 1; n > 1; n--)
            {
                actDataSet.N = n;
                programm.ActRuntime++;
                if (programm.ActRuntime >= Config.MAX_RUNTIME(actDataSet.A.Length)) return Config.MAX_RUNTIME_ERROR;
                if (buildLog) updateLog();
                tmpError = executeList(buildLog);
                if (tmpError != null) return tmpError;
                actDataSet = programm.Stack.Peek();
            }
            if (buildLog) updateLog();
            updateDataSets();
            return tmpError;
        }
    }
}
