using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfAjLessAmin : ListStm
    {
        public IfAjLessAmin(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "if (a[j] < a[min]) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J == Config.NOTUSED || actDataSet.Min == Config.NOTUSED) return Config.NOTINITERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            string tmpError = null;
            if (buildLog) updateLog();
            try
            {
                if (actDataSet.A[actDataSet.J] < actDataSet.A[actDataSet.Min])
                {
                    tmpError = executeList(buildLog);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return Config.OUTOFRANGEERROR;
            }
            updateDataSets();
            return tmpError;
        }
    }
}
