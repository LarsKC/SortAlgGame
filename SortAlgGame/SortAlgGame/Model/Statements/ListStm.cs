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

                for (int i = 0; i < StmList.Count -1; i++)
                {
                    StmList.ElementAt<Statement>(i).Indent = this.Indent + 1;
                }
                StmList.Last.Value.Indent = this.Indent;
            }
        }


        public ListStm(Player player, ListStm parent)
            : base(player, parent)
        {
            stmList = new LinkedList<Statement>();
            stmList.AddLast(new LoopEnd(player, this));
            //stmList.Last.Previous.Value.Indent++;
        }

        public string executeList(bool buildLog)
        {
            string tmpError = null;
            for(int i = 0; i < stmList.Count && tmpError == null; i++)
            {
                 tmpError = stmList.ElementAt(i).execute(buildLog);
            }
            return tmpError;
        }

        public void addBeforeStm(Statement pos, Statement stm)
        {
            if (pos == null)
            {
                StmList.AddBefore(StmList.Last, stm);
            }
            else
            {
                StmList.AddBefore(StmList.Find(pos), stm);
            }
            stm.Parent = this;
            stm.Indent = this.Indent + 1;
        }

        //TODO !!!
        public void removeStm(Statement stm)
        {
            stm.Parent = null;
            stm.Indent = 0;
            if (stm is ListStm)
            {
                for (int i = (stm as ListStm).StmList.Count - 2; i  >= 0; i--)
                {

                    (stm as ListStm).StmList.ElementAt<Statement>(i).Indent = this.Indent + 1;
                    this.addBeforeStm(stm, (stm as ListStm).StmList.ElementAt<Statement>(i));
                    (stm as ListStm).StmList.Remove((stm as ListStm).StmList.ElementAt<Statement>(i));
                }
            }
            stmList.Remove(stm);
        }

        public bool stmListContains(Statement stm, ListStm listStm)
        {
            if (stm == listStm)
                return true;

            foreach (Statement x in listStm.StmList)
            {
                if (x is ListStm)
                {
                    if (stmListContains(stm, x as ListStm))
                        return true;
                }
                else if(x == stm)
                {
                    return true;
                }
            }
            return false;
        }

        public void updateDataSets()
        {
            DataSet actDataSet = Player.Stack.Pop();
            DataSet oldDataSet = Player.Stack.Pop();
            if (oldDataSet.I == Config.NOTUSED) actDataSet.I = Config.NOTUSED;
            if (oldDataSet.J == Config.NOTUSED) actDataSet.J = Config.NOTUSED;
            if (oldDataSet.N == Config.NOTUSED) actDataSet.N = Config.NOTUSED;
            if (oldDataSet.Min == Config.NOTUSED) actDataSet.Min = Config.NOTUSED;
            if (oldDataSet.Pivot == Config.NOTUSED) actDataSet.Pivot = Config.NOTUSED;
            player.Stack.Push(actDataSet);
        }
    }
}
