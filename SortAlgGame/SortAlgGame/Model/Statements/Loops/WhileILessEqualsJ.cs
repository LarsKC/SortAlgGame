using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class WhileILessEqualsJ : ListStm
    {
        public WhileILessEqualsJ(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "while(i <= j) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED || actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            string tmpError = null;
            while (actDataSet.I <= actDataSet.J)
            {
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
