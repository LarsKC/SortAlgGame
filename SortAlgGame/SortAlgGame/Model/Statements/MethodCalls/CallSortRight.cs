using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class CallSortRight : Statement
    {
        public CallSortRight(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "sort (a, i, right);";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            DataSet newDataSet = new DataSet(actDataSet.A);
            newDataSet.Left = actDataSet.I;
            newDataSet.Right = actDataSet.Right;
            programm.Stack.Push(newDataSet);
            if (buildLog) updateLog();
            string tmpError = programm.Stm.execute(buildLog);
            if (tmpError != null) return tmpError;
            newDataSet = programm.Stack.Pop();
            actDataSet = programm.Stack.Peek();
            actDataSet.A = newDataSet.A;
            if (buildLog) updateLog();
            return null;
        }
    }
}
