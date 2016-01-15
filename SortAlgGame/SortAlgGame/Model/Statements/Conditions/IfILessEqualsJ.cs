using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfILessEqualsJ : ListStm
    {
        public IfILessEqualsJ(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "if (i <= j) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED || actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            programm.Stack.Push(new DataSet(actDataSet));
            actDataSet = programm.Stack.Peek();
            if (buildLog) updateLog();
            string tmpError = null;
            if (actDataSet.I <= actDataSet.J)
            {
                tmpError = executeList(buildLog);
            }
            updateDataSets();
            return tmpError;
        }
    }
}
