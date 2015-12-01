using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    abstract class loopBased : Statement
    {  
        //Variablen
        private LinkedList<Statement> _stmList;
        private Statement _parent;

        //Accessor
        public LinkedList<Statement> StmList
        {
            get { return _stmList; }
        }
        public Statement Parent
        {
            get { return _parent; }
        }

        //Konstruktoren
        public loopBased(PlayerProgramm programm, Statement parent)
            : base(programm)
        {
            _parent = parent;
            _stmList = new LinkedList<Statement>();
            //TODO Blank Statement for adding module zur stmList hinzufügen?!
            indent = _parent.Indent +1;
        }

        //Methoden
        public void addStm(Statement stm)
        {
            _stmList.AddLast(stm);
        }
    }
}
