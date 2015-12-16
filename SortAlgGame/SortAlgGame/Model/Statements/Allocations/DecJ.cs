using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class DecJ : Statement
    {
        public DecJ(Player player, Statement parent)
            : base(player, parent)
        {
            content = "j--;";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J != Config.NOTUSED)
            {
                actDataSet.J--;
                if(buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(actDataSet)));
            }
            else
            {
                //TODO ExceptionHandling
            }
        }
    }
}
