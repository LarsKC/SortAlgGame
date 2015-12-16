using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    abstract class ListStm : Statement
    {
        protected LinkedList<Statement> stmList;

        public LinkedList<Statement> StmList
        {
            get { return stmList; }
        }

        public ListStm(Player player, Statement parent)
            : base(player, parent)
        {
            stmList = new LinkedList<Statement>();
            stmList.AddLast(new AddBrick(player, this));
            stmList.AddLast(new LoopEnd(player, this));
            stmList.Last.Previous.Value.Indent++;
        }

        public void executeList(bool buildLog)
        {
            for(int i = 0; i < stmList.Count - 2; i++)
            {
                stmList.ElementAt(i).execute(buildLog);
            }
        }

        public void addStm(Statement stm)
        {
            stm.Parent = this;
            stmList.AddBefore(stmList.Last.Previous, stm);
            stm.Indent = this.Indent + 1;
            if (stm is ListStm)
            {
                (stm as ListStm).StmList.Last.Value.Indent = stm.Indent;
                (stm as ListStm).StmList.Last.Previous.Value.Indent = stm.Indent + 1;
            }
        }

        public void remove(Statement stm)
        {
            stm.Parent = null;
            stmList.Remove(stm);
        }
    }
}
