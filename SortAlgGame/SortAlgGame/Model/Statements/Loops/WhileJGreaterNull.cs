using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class WhileJGreaterNull : ListStm
    {
        public WhileJGreaterNull(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "while (j > 0 && a[j-1] > a[j]) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J == Config.NOTUSED) return Config.NOTINITERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            string tmpError = null;
            try
            {
                while (actDataSet.J > 0 && actDataSet.A[actDataSet.J - 1] > actDataSet.A[actDataSet.J])
                {
                    if (buildLog) updateLog();
                    tmpError = executeList(buildLog);
                    if (tmpError != null) return tmpError;
                    actDataSet = player.Stack.Peek();
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return Config.OUTOFRANGEERROR;
            }
            if (buildLog) updateLog();
            updateDataSets();
            return tmpError;
        }
    }
}
