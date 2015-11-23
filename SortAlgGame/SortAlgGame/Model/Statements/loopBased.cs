using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    abstract class loopBased : Statement
    {
        //Variablen mit Get und Set Accessor
        protected string footer;
        public string Footer
        {
            get { return footer; }
        }

        protected Statement innerStatement;
        public Statement InnerStatement
        {
            get { return innerStatement; }
            set { innerStatement = value; }
        }

        public loopBased(PlayerProgramm programm)
            : base(programm)
        {
            footer = "}";
        }

    }
}
