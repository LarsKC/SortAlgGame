using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    class LoopEnd : Statement
    {
        public LoopEnd(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "}";
        }

        public override string execute(bool buildLog)
        {
            if (buildLog) updateLog();
            return null;
        }
    }
}
