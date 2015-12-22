using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class WhileAjGreaterPivot : ListStm
    {
        public WhileAjGreaterPivot(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "while (a[j] > pivot) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();

            if (actDataSet.J != Config.NOTUSED && actDataSet.Pivot != Config.NOTUSED)
            {
                while (actDataSet.A[actDataSet.J] > actDataSet.Pivot)
                {
                    //TODO LOG + RUNTIME
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
