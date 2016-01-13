using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class DecJ : Statement
    {
        public DecJ(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "j--;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            actDataSet.J--;
            if(buildLog) updateLog();
            return null;
        }
    }
}
