using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    abstract class Statement
    {
        //Var
        protected int indent = 0;
        protected string content;
        protected Programm programm;
        protected ListStm parent;
        //Accessore
        public virtual int Indent
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
                    content = content.Substring(dif * (-1) * 4);
                    indent = value;
                }
            }
        }

        public string Content
        {
            get { return content; }
        }

        public ListStm Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Programm Programm
        {
            get { return programm; }
        }
        //Konstruktoren
        public Statement(Programm player, ListStm parent)
        {
            this.programm = player;
            this.parent = parent;
        }
        //Methods
        public abstract string execute(bool buildLog);

        public void updateLog()
        {
            programm.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(programm.Stack.Peek())));
        }
    }
}
