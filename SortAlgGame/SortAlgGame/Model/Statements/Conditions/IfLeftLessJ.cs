using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfLeftLessJ : ListStm
    {
        public IfLeftLessJ(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "if (left < j) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J == Config.NOTUSED) return Config.NOTINITERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
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
