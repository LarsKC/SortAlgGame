using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfILessJ : ListStm
    {
        public IfILessJ(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "if (i < j) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOTUSED && actDataSet.J != Config.NOTUSED)
            {
                if (actDataSet.I < actDataSet.J)
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
