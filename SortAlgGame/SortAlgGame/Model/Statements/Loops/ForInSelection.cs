using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class ForInSelection : ListStm
    {
        public ForInSelection(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "for (int j= i+1; j < n; j++) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED || actDataSet.N == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            string tmpError = null;
            for (int j = actDataSet.I + 1; j < actDataSet.N; j++)
            {
                actDataSet.J = j;
                player.ActRuntime++;
                if (player.ActRuntime >= Config.MAX_RUNTIME(actDataSet.A.Length)) return Config.MAX_RUNTIME_ERROR;
                if (buildLog) updateLog();
                tmpError = executeList(buildLog);
                if (tmpError != null) return tmpError;
                actDataSet = player.Stack.Peek();
            }
            if (buildLog) updateLog();
            updateDataSets();
            return tmpError;
        }
    }
}
