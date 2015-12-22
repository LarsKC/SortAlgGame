using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class ForOutBubble : ListStm
    {
        public ForOutBubble(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "for (int n = right+1; n > 1; n--) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            int tmpN = actDataSet.N;
            for (int n = actDataSet.Right + 1; n > 1; n--)
            {
                actDataSet.N = n;
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
                //TODO LOG + RUNTIME
                executeList(buildLog);
            }
            actDataSet.N = tmpN;
            if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
        }
    }
}
