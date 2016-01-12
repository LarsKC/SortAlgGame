﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfAiGreaterAiInc : ListStm
    {
        public IfAiGreaterAiInc(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "if (a[i] > a[i+1]) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.I == Config.NOTUSED) return Config.NOTINITERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            if (buildLog) updateLog();
            string tmpError = null;
            try
            {
                if (actDataSet.A[actDataSet.I] > actDataSet.A[actDataSet.I + 1])
                {
                    tmpError = executeList(buildLog);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return Config.OUTOFRANGEERROR;
            }
            updateDataSets();
            return tmpError;
        }
    }
}
