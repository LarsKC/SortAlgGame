using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;

namespace SortAlgGame.Model.Statements
{
    class BaseStatement : ListStm
    {
        public BaseStatement(Player player, ListStm parent)
            : base(player, parent)
        {
            content = "public sort(int[] a, int left, int right) {";
            indent = 0;
        }

        public override string execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            actDataSet.Left = (actDataSet.Left == Config.NOTUSED) ? 0 : actDataSet.Left;
            actDataSet.Right = (actDataSet.Right == Config.NOTUSED) ? actDataSet.A.Length - 1 : actDataSet.Right;
            return executeList(buildLog);
        }
    }
}
