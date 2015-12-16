using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class ForInBubble : ListStm
    {
        public ForInBubble(Player player, Statement parent)
            : base(player, parent)
        {
            content = "for (int i = left; i > n-1; i++) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.N != Config.NOTUSED)
            {
                int tmpI = actDataSet.I;
                for (int i = actDataSet.Left; i > actDataSet.N - 1; i++)
                {
                    actDataSet.I = i;
                    if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
                    executeList(buildLog);
                }
                actDataSet.I = tmpI;
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
            }
            else
            {
                //TODO ExceptionHandling
            }
        }
    }
}
