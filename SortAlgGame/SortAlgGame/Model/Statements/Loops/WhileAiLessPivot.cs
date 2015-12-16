﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class WhileAiLessPivot : ListStm
    {
        public WhileAiLessPivot(Player player, Statement parent)
            : base(player, parent)
        {
            content = "while (a[i] > pivot) {";
        }

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I != Config.NOTUSED && actDataSet.Pivot != Config.NOTUSED)
            {
                while (actDataSet.A[actDataSet.I] > actDataSet.Pivot)
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
