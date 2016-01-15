﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements.Conditions
{
    class IfAjLessAmin : ListStm
    {
        public IfAjLessAmin(Programm player, ListStm parent)
            : base(player, parent)
        {
            content = "if (a[j] < a[min]) {";
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (actDataSet.J == Config.NOT_USED || actDataSet.Min == Config.NOT_USED) return Config.NOT_INIT_ERROR;
            programm.Stack.Push(new DataSet(actDataSet));
            actDataSet = programm.Stack.Peek();
            string tmpError = null;
            if (buildLog) updateLog();
            try
            {
                if (actDataSet.A[actDataSet.J] < actDataSet.A[actDataSet.Min])
                {
                    tmpError = executeList(buildLog);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return Config.OUT_OF_RANGE_ERROR;
            }
            updateDataSets();
            return tmpError;
        }
    }
}
