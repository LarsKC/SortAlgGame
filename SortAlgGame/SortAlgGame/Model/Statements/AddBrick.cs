﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    class AddBrick : Statement
    {
        public AddBrick(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "+++++++++";
        }



        public override void execute(bool buildLog)
        {
            //Nothing
        }
    }
}
