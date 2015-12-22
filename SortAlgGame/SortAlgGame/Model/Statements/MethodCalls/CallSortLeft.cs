using System;
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

        public override void execute(bool buildLog)
        {
            DataSet actDataSet = player.Stack.Peek();
            if (actDataSet.J != Config.NOTUSED)
            {
                DataSet newDataSet = new DataSet(actDataSet.A);
                newDataSet.Left = actDataSet.Left;
                newDataSet.Right = actDataSet.J;
                player.Stack.Push(newDataSet);
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(newDataSet)));
                player.Stm.execute(buildLog);
                player.Stack.Pop();
                if (buildLog) player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(player.Stack.Peek())));
            }
            else
            {
                //TODO ExceptionHandling
            }
        }
    }
}
