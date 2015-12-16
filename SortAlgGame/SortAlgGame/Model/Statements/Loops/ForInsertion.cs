using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class ForInsertion : ListStm
    {
        public ForInsertion(Player player, Statement parent)
            : base(player, parent)
        {
            content = "for (int i = left+1; i < n; i++) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.N != Config.NOTUSED)
            {
                int tmpI = actDataSet.I;
                for (int i = actDataSet.Left + 1; i < actDataSet.N; i++)
                {
                    actDataSet.I = i;
                    if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
                    //TODO LOG + RUNTIME
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
