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

        public override int Indent
        {
            get { return indent; }
            set
            {
                int dif = value - indent;

                if (dif > 0)
                {
                    for (int i = 0; i < dif; i++)
                    {
                        content = "    " + content;
                    }
                    indent = value;
                }
                else if (dif < 0)
                {
                    content = content.Substring(dif*(-1)*4);
                    indent = value;
                }

                foreach (Statement x in StmList)
                {
                    x.Indent = this.Indent + 1;
                }
            }
        }


        public ListStm(Player player, ListStm parent)
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

        //TODO !!!
        public void removeStm(Statement stm)
        {
            stm.Parent = null;
            stm.Indent = 0;
            if (stm is ListStm)
            {
                for (int i = 0; i < (stm as ListStm).StmList.Count - 2; i++ )
                {
                    (stm as ListStm).StmList.ElementAt<Statement>(0).Indent = this.Indent + 1;
                    this.StmList.AddBefore(this.StmList.Find(stm), (stm as ListStm).StmList.ElementAt<Statement>(0));
                    (stm as ListStm).StmList.Remove((stm as ListStm).StmList.ElementAt<Statement>(0));
                }
            }
            stmList.Remove(stm);
        }
    }
}
