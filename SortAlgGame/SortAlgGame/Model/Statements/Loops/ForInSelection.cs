using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class ForInSelection : ListStm
    {
        public ForInSelection(Player player, Statement parent)
            : base(player, parent)
        {
            content = "for (int j= i+1; j < n; j++) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOTUSED && actDataSet.N != Config.NOTUSED)
            {
                int tmpJ = actDataSet.J;
                for (int j = actDataSet.I + 1; j < actDataSet.N; j++)
                {
                    //TODO LOG + RUNTIME
                    actDataSet.J = j;
                    if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
                    executeList(buildLog);
                }
                actDataSet.J = tmpJ;
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
            }
            else
            {
                //TODO ExceptionHandling
            }
        }
    }
}
