using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfILessRight : ListStm
    {
        public IfILessRight(Player player, Statement parent)
            : base(player, parent)
        {
            content = "if (i < right) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOTUSED && actDataSet.Right != Config.NOTUSED)
            {
                if (actDataSet.I < actDataSet.Right)
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
