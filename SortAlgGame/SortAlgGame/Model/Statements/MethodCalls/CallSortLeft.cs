﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.MethodCalls
{
    class CallSortLeft : Statement
    {
        public CallSortLeft(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "sort(a, left, J);";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
                if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                DataSet newDataSet = new DataSet(actDataSet.A);
                newDataSet.Left = actDataSet.Left;
                newDataSet.Right = actDataSet.J;
                player.Stack.Push(newDataSet);
                if (buildLog) updateLog();
                player.Stm.execute(buildLog);
                newDataSet = player.Stack.Pop();
                actDataSet = player.Stack.Peek();
                actDataSet.A = newDataSet.A;
                if (buildLog) updateLog();
                return null;
        }
    }
}
