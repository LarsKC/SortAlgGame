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
        protected Player player;
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

        public Player Player
        {
            get { return player; }
        }
        //Konstruktoren
        public Statement(Player player, ListStm parent)
        {
            this.player = player;
            this.parent = parent;
        }
        //Methods
        public abstract string execute(bool buildLog);

        public void updateLog()
        {
            player.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(player.Stack.Peek())));
        }
    }
}
