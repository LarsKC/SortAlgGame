﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Allocations
{
    class AllocIToJ : Statement
    {
        public AllocIToJ(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "j = i;";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            actDataSet.J = actDataSet.I;
            if (buildLog) updateLog();
            return null;
        }
    }
}
