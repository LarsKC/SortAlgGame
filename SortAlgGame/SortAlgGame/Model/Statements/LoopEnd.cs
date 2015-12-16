using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    class LoopEnd : Statement
    {
        public LoopEnd(Player player, Statement parent)
            : base(player, parent)
        {
            content = "}";
        }

        public override void execute(bool buildLog)
        {
            //Do Nothing
        }
    }
}
