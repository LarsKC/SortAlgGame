using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;

namespace SortAlgGame.Model
{
    class Programm
    {
        #region Variablen
        private Tuple<int, string, string> _actStats;
        private int _actRuntime;
        private DateTime _endTime;
        private LinkedList<Tuple<Statement, DataSet>> _log;
        private Stack<DataSet> _stack;
        private ListStm _stm;

        #endregion

        #region Accessor
        public Tuple<int, string, string> ProgrammStats
        {
            get { return _actStats; }
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

        public int ActRuntime
        {
            get { return _actRuntime; }
            set { _actRuntime = value; }
        }
        #endregion

        #region Konstruktor
        public Programm()
        {
            _actStats = null;
            _stm = new ListStm(this, null, Config.EXECUTE.BASE_STM);
            _stack = new Stack<DataSet>();
            _log = new LinkedList<Tuple<Statement, DataSet>>();
            _actRuntime = 0;
        }
        #endregion

        #region Methods
        public void execute(int[] a, bool genLog)
        {
            _actRuntime = 0;
            _actStats = null;
            _stack.Clear();
            DataSet dataSet = new DataSet(a);
            _stack.Push(dataSet);
            string error = _stm.execute(genLog);
            if (error != null)
            {
                _actStats = new Tuple<int, string, string>(a.Length, error, Config.RUNTIME_NA);
            }
            else
            {
                _actStats = new Tuple<int, string, string>(a.Length, "keine", _actRuntime.ToString());
            }
        }

        public TimeSpan calcTimeSpan(DateTime startTime)
        {
            return _endTime - startTime;
        }

        public LinkedList<Statement> getActualStmNesting()
        {
            return getActualStmNesting(_stm);
        }

        public LinkedList<Statement> getActualStmNesting(ListStm baseStm)
        {
            LinkedList<Statement> itemList = new LinkedList<Statement>();
            itemList.AddLast(baseStm);
            foreach (Statement x in (baseStm as ListStm).StmList)
            {
                if (x is ListStm)
                {
                    itemList = concatList(itemList, getActualStmNesting(x as ListStm));
                }
                else
                {
                    itemList.AddLast(x);
                }
            }
            return itemList;
        }

        public LinkedList<Statement> concatList(LinkedList<Statement> first, LinkedList<Statement> second)
        {
            LinkedList<Statement> newList = new LinkedList<Statement>(first);
            foreach (Statement x in second)
            {
                newList.AddLast(x);
            }
            return newList;
        }

        public void buildBubblesort()
        {
            _stm.addBeforeStm(null, new ListStm(this, _stm, Config.EXECUTE.FOR_OUT_BUBBLE));
            ListStm tmp = _stm.StmList.First.Value as ListStm;
            tmp.addBeforeStm(null, new ListStm(this, tmp, Config.EXECUTE.FOR_IN_BUBBLE));
            tmp = tmp.StmList.First.Value as ListStm;
            tmp.addBeforeStm(null, new ListStm(this, tmp, Config.EXECUTE.IF_AI_GREATER_AI_INC));
            tmp = tmp.StmList.First.Value as ListStm;
            tmp.addBeforeStm(null, new Statement(this, tmp, Config.EXECUTE.SWAP_AI_WITH_AI_INC));
        }


        public void buildQuicksort()
        {
            _stm.addBeforeStm(null, new Statement(this, _stm, Config.EXECUTE.ALLOC_LEFT));
            _stm.addBeforeStm(null, new Statement(this, _stm, Config.EXECUTE.ALLOC_RIGHT));
            _stm.addBeforeStm(null, new Statement(this, _stm, Config.EXECUTE.ALLOC_PIVOT));
            _stm.addBeforeStm(null, new ListStm(this, _stm, Config.EXECUTE.WHILE_I_LESS_EQUALS_J));
            ListStm tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new ListStm(this, tmp1, Config.EXECUTE.WHILE_AI_LESS_PIVOT));
            ListStm tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new Statement(this, tmp2, Config.EXECUTE.INC_I));
            tmp1.addBeforeStm(null, new ListStm(this, tmp1, Config.EXECUTE.WHILE_AJ_GREATER_PIVOT));
            tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new Statement(this, tmp2, Config.EXECUTE.DEC_J));
            tmp1.addBeforeStm(null, new ListStm(this, tmp1, Config.EXECUTE.IF_I_LESS_EQUALS_J));
            tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new Statement(this, tmp2, Config.EXECUTE.SWAP_AI_WITH_AJ));
            tmp2.addBeforeStm(null, new Statement(this, tmp2, Config.EXECUTE.INC_I));
            tmp2.addBeforeStm(null, new Statement(this, tmp2, Config.EXECUTE.DEC_J));
            _stm.addBeforeStm(null, new ListStm(this, _stm, Config.EXECUTE.IF_LEFT_LESS_J));
            tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new Statement(this, tmp1, Config.EXECUTE.CALL_SORT_LEFT));
            _stm.addBeforeStm(null, new ListStm(this, _stm, Config.EXECUTE.IF_I_LESS_RIGHT));
            tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new Statement(this, tmp1, Config.EXECUTE.CALL_SORT_RIGHT));
            printExecute();
        }

        public void buildSelectionsort()
        {
            _stm.addBeforeStm(null, new Statement(this, _stm, Config.EXECUTE.ALLOC_ALENGHT));
            _stm.addBeforeStm(null, new ListStm(this, _stm, Config.EXECUTE.FOR_IN_BUBBLE));
            ListStm tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new Statement(this, tmp1, Config.EXECUTE.ALLOC_I_TO_MIN));
            tmp1.addBeforeStm(null, new ListStm(this, tmp1, Config.EXECUTE.FOR_IN_SELECTION));
            ListStm tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new ListStm(this, tmp2, Config.EXECUTE.IF_AJ_LESS_AMIN));
            tmp2 = tmp2.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new Statement(this, tmp2, Config.EXECUTE.ALLOC_J_TO_MIN));
            tmp1.addBeforeStm(null, new ListStm(this, tmp1, Config.EXECUTE.IF_I_NOTEQUALS_MIN));
            tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new Statement(this, tmp2, Config.EXECUTE.SWAP_AMIN_WITH_AI));
        }

        public void buildInsertionsort()
        {
            _stm.addBeforeStm(null, new Statement(this, _stm, Config.EXECUTE.ALLOC_ALENGHT));
            _stm.addBeforeStm(null, new ListStm(this, _stm, Config.EXECUTE.FOR_INSERTION));
            ListStm tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new Statement(this, tmp1, Config.EXECUTE.ALLOC_I_TO_J));
            tmp1.addBeforeStm(null, new ListStm(this, tmp1, Config.EXECUTE.WHILE_J_GREATER_NULL));
            tmp1 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new Statement(this, tmp1, Config.EXECUTE.SWAP_AJ_WITH_AJ_DEC));
            tmp1.addBeforeStm(null, new Statement(this, tmp1, Config.EXECUTE.DEC_J));
        }

        public void printExecute()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Lars\Desktop\executePrint.txt");
            file.WriteLine(_stm.Content);
            rekursivePrint((_stm as ListStm).StmList, file);
            file.WriteLine("---------------------------------------------------------------------------------");
            /*foreach (int x in _stack.Peek().A)
            {
                file.Write(x);
            }*/
            file.Close();
        }

        public void rekursivePrint(LinkedList<Statement> stmList, System.IO.StreamWriter file)
        {
            foreach (Statement x in stmList)
            {
                file.WriteLine(x.Content);
                if (x is ListStm)
                {
                    rekursivePrint((x as ListStm).StmList, file);
                }
            }
        }
        #endregion
    }
}
