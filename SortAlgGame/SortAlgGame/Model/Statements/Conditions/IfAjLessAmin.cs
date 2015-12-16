using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfAjLessAmin : ListStm
    {
        public IfAjLessAmin(Player player, Statement parent)
            : base(player, parent)
        {
            content = "if (a[j] < a[min]) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J != Config.NOTUSED && actDataSet.Min != Config.NOTUSED)
            {
                if (actDataSet.A[actDataSet.J] < actDataSet.A[actDataSet.Min])
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
