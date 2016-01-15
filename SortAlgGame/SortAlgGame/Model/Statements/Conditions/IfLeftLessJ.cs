using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfLeftLessJ : ListStm
    {
        public IfLeftLessJ(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "if (left < j) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            programm.Stack.Push(new DataSet(actDataSet));
            actDataSet = programm.Stack.Peek();
            if (buildLog) updateLog();
            string tmpError = null;
            if (actDataSet.Left < actDataSet.J)
            {
                tmpError = executeList(buildLog);
            }
            updateDataSets();
            return tmpError;
        }
    }
}
