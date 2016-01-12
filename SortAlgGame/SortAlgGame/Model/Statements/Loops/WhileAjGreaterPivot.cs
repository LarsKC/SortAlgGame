using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class WhileAjGreaterPivot : ListStm
    {
        public WhileAjGreaterPivot(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "while (a[j] > pivot) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J == Config.NOTUSED || actDataSet.Pivot == Config.NOTUSED) return Config.NOTINITERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            string tmpError = null;
            try
            {
                while (actDataSet.A[actDataSet.J] > actDataSet.Pivot)
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
