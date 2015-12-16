using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class WhileJGreaterNull : ListStm
    {
        public WhileJGreaterNull(Player player, Statement parent)
            : base(player, parent)
        {
            content = "while (j > 0 && a[j-1] > a[i]) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J != Config.NOTUSED && actDataSet.I != Config.NOTUSED)
            {
                while (actDataSet.J > 0 && actDataSet.A[actDataSet.J - 1] > actDataSet.A[actDataSet.I])
                {
                    executeList(buildLog);
                }
            }
            else
            {
                //TODO ExceptionHandling
            }
        }
    }
}
