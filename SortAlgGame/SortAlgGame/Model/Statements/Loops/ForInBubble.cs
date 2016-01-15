using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class ForInBubble : ListStm
    {
        public ForInBubble(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "for (int i = left; i < n-1; i++) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.N == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            programm.Stack.Push(new DataSet(actDataSet));
            actDataSet = programm.Stack.Peek();
            string tmpError = null;
            for (int i = actDataSet.Left; i < actDataSet.N - 1; i++)
            {
                actDataSet.I = i;
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
