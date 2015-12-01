using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    abstract class Statement
    {
#region Old
        /*//Old
        protected string header;
        public string Header
        {
            get { return header; }
        }

        protected PlayerProgramm programm;

        public Statement(PlayerProgramm programm)
        {
            this.programm = programm;
        }

        public abstract void execute();
        */
#endregion
        
        //Var
        protected int indent;
        protected string content;
        protected PlayerProgramm programm;

        //Accessore
        public int Indent
        {
            get { return indent; }
        }

        //Konstruktoren
        public Statement(PlayerProgramm programm)
        {
            this.programm = programm;
        }

        //Methods
        public abstract void toString();
        public abstract void execute();
    }
}
