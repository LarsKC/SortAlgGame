using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfINotEqualsMin : ListStm
    {
        public IfINotEqualsMin(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "if (i != min) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED || actDataSet.Min == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            if (buildLog) updateLog();
            string tmpError = null;
            if (actDataSet.I != actDataSet.Min)
            {
                tmpError = executeList(buildLog);
            }
            updateDataSets();
            return tmpError;
        }
    }
}
