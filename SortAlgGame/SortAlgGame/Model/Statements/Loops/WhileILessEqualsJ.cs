using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class WhileILessEqualsJ : ListStm
    {
        public WhileILessEqualsJ(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "while(i <= j) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOTUSED && actDataSet.J != Config.NOTUSED)
            {
                while (actDataSet.I <= actDataSet.J)
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
