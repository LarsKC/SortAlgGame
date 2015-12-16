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
        protected Statement parent;
        //Accessore
        public int Indent
        {
            get { return indent; }
            set 
            {
                int dif;
                if ((dif = value - indent) > 0)
                {
                    for (int i = 0; i < dif; i++)
                    {
                        content = "    " + content;
                    }
                    indent = value;
                }
            }
        }

        public string Content
        {
            get { return content; }
        }

        public Statement Parent
        {
            get { return parent;}
            set { parent = value;}
        }

        public Player Player
        {
            get { return player; }
        }
        //Konstruktoren
        public Statement(Player player, Statement parent)
        {
            this.player = player;
            this.parent = parent;
        }

        public abstract void execute(bool buildLog);
    }
}
