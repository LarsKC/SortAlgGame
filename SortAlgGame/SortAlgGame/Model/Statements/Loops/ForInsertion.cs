using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class ForInsertion : ListStm
    {
        public ForInsertion(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "for (int i = left+1; i < n; i++) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.N == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            string tmpError = null;
            for (int i = actDataSet.Left + 1; i < actDataSet.N; i++)
            {
                actDataSet.I = i;
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
