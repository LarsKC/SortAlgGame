using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfLeftLessJ : ListStm
    {
        public IfLeftLessJ(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "if (left < j) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.Left != Config.NOTUSED && actDataSet.J != Config.NOTUSED)
            {
                if (actDataSet.Left < actDataSet.J)
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
