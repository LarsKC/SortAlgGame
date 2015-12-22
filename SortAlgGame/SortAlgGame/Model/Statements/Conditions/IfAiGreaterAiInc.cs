using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfAiGreaterAiInc : ListStm
    {
        public IfAiGreaterAiInc(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "if (a[i] > a[i+1]) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOTUSED)
            {
                if (actDataSet.A[actDataSet.I] > actDataSet.A[actDataSet.I + 1])
                {

                    foreach (Statement x in stmList)
                    {
                        x.execute(buildLog);
                    }
                }
            }
            else
            {
                //TODO ExceptionHandling
            }
        }
    }
}
