using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;

namespace SortAlgGame.Model
{
    class Player
    {
        #region Variablen
        private Dictionary<int, int> _runtime;
        private DateTime _endTime;
        private LinkedList<Tuple<Statement, DataSet>> _log;
        private LinkedListNode<Tuple<Statement, DataSet>> _curLogSet;
        private Stack<DataSet> _stack;
        private Statement _stm;

        #endregion

        #region Accessor
        public Dictionary<int, int> Runtime
        {
            get { return _runtime; }
        }

        public DateTime EndTime
        {
            set { _endTime = value; }
        }

        public Stack<DataSet> Stack
        {
            get { return _stack; }
        }

        public Statement Stm
        {
            get { return _stm; }
        }

        public LinkedList<Tuple<Statement, DataSet>> Log
        {
            get { return _log; }
        }
        #endregion

        #region Konstruktor
        public Player()
        {
            _runtime = new Dictionary<int, int>();
            _stm = new BaseStatement(this, null);
        }
        #endregion

        #region Methods
        public void init(int[] a)
        {
            DataSet dataSet = new DataSet(a);
            _stack = new Stack<DataSet>();
            _stack.Push(dataSet);
            _log = new LinkedList<Tuple<Statement, DataSet>>();
            _log.AddLast(new Tuple<Statement, DataSet>(_stm, dataSet));
        }

        public void resetStack(int[] a)
        {
            _stack = new Stack<DataSet>();
            _stack.Push(new DataSet(a));
        }

        public TimeSpan calcTimeSpan(DateTime startTime)
        {
            return _endTime - startTime;
        }

        #endregion
    }
}
