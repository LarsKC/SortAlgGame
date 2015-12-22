using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfINotEqualsMin : ListStm
    {
        public IfINotEqualsMin(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "if (i != min) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDatSet = player.Stack.Peek();
            if (actDatSet.I != Config.NOTUSED && actDatSet.Min != Config.NOTUSED)
            {
                if (actDatSet.I != actDatSet.Min)
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
