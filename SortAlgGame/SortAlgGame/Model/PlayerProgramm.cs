using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;

namespace SortAlgGame.Model
{
    class PlayerProgramm
    {
        //Variablen mit Get und Set Accessor
        private Stack<DataSet> stack;
        public Stack<DataSet> Stack
        {
            get { return stack; }
            set { stack = value; }
        }

        private LinkedList<Tuple<Statement, DataSet>> log;
        public LinkedList<Tuple<Statement, DataSet>> Log
        {
            get { return log; }
            set { log = value; }
        }

        private LinkedListNode<Tuple<Statement, DataSet>> curLogSet;
        public LinkedListNode<Tuple<Statement, DataSet>> CurLogSet
        {
            get { return curLogSet; }
            set { curLogSet = value; }
        }

        private Statement stm;
        public Statement Stm
        {
            get { return stm; }
            set { stm = value; }
        }

        private int runtime;
        public int Runtime
        {
            get { return runtime; }
            set { runtime = value; }
        }

        //Konstruktor
        public PlayerProgramm()
        {
            //empty
        }

        //Methoden
        public void initData(int[] a)
        {
            DataSet dataSet = new DataSet(a);
            this.stack = new Stack<DataSet>();
            this.stack.Push(dataSet);
            this.log.AddLast(new Tuple<Statement, DataSet>(stm, dataSet));
            this.curLogSet = log.First;
        }
    }
}
