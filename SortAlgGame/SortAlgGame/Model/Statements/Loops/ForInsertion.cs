﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Loops
{
    class ForInsertion : ListStm
    {
        public ForInsertion(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "for (int i = left+1; i < n; i++) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.N == Config.NOTUSED) return Config.NOTINITERROR;
            player.Stack.Push(new DataSet(actDataSet));
            actDataSet = player.Stack.Peek();
            string tmpError = null;
            for (int i = actDataSet.Left + 1; i < actDataSet.N; i++)
            {
                actDataSet.I = i;
                if (buildLog) updateLog();
                tmpError = executeList(buildLog);
                if (tmpError != null) return tmpError;
                actDataSet = player.Stack.Peek();
            }
            if (buildLog) updateLog();
            updateDataSets();
            return tmpError;
        }
    }
}
