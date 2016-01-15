using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;
using SortAlgGame.Model.Statements.Loops;
using SortAlgGame.Model.Statements.Allocations;
using SortAlgGame.Model.Statements.Conditions;
using SortAlgGame.Model.Statements.MethodCalls;

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
            _stm = new BaseStatement(this, null);
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
            _stm.addBeforeStm(null, new ForOutBubble(this, _stm));
            ListStm tmp = _stm.StmList.First.Value as ListStm;
            tmp.addBeforeStm(null, new ForInBubble(this, tmp));
            tmp = tmp.StmList.First.Value as ListStm;
            tmp.addBeforeStm(null, new IfAiGreaterAiInc(this, tmp));
            tmp = tmp.StmList.First.Value as ListStm;
            tmp.addBeforeStm(null, new SwapAiWithAiInc(this, tmp));
        }


        public void buildQuicksort()
        {
            _stm.addBeforeStm(null, new AllocLeft(this, _stm));
            _stm.addBeforeStm(null, new AllocRight(this, _stm));
            _stm.addBeforeStm(null, new AllocPivot(this, _stm));
            _stm.addBeforeStm(null, new WhileILessEqualsJ(this, _stm));
            ListStm tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new WhileAiLessPivot(this, tmp1));
            ListStm tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new IncI(this, tmp2));
            tmp1.addBeforeStm(null, new WhileAjGreaterPivot(this, tmp1));
            tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new DecJ(this, tmp2));
            tmp1.addBeforeStm(null, new IfILessEqualsJ(this, tmp1));
            tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new SwapAiWithAj(this, tmp2));
            tmp2.addBeforeStm(null, new IncI(this, tmp2));
            tmp2.addBeforeStm(null, new DecJ(this, tmp2));
            _stm.addBeforeStm(null, new IfLeftLessJ(this, _stm));
            tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new CallSortLeft(this, tmp1));
            _stm.addBeforeStm(null, new IfILessRight(this, _stm));
            tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new CallSortRight(this, tmp1));
            printExecute();
        }

        public void buildSelectionsort()
        {
            _stm.addBeforeStm(null, new AllocALength(this, _stm));
            _stm.addBeforeStm(null, new ForInBubble(this, _stm));
            ListStm tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new AllocIToMin(this, tmp1));
            tmp1.addBeforeStm(null, new ForInSelection(this, tmp1));
            ListStm tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new IfAjLessAmin(this, tmp2));
            tmp2 = tmp2.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new AllocJToMin(this, tmp2));
            tmp1.addBeforeStm(null, new IfINotEqualsMin(this, tmp1));
            tmp2 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp2.addBeforeStm(null, new SwapAminWithAi(this, tmp2));
        }

        public void buildInsertionsort()
        {
            _stm.addBeforeStm(null, new AllocALength(this, _stm));
            _stm.addBeforeStm(null, new ForInsertion(this, _stm));
            ListStm tmp1 = _stm.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new AllocIToJ(this, tmp1));
            tmp1.addBeforeStm(null, new WhileJGreaterNull(this, tmp1));
            tmp1 = tmp1.StmList.Last.Previous.Value as ListStm;
            tmp1.addBeforeStm(null, new SwapAjWithAjDec(this, tmp1));
            tmp1.addBeforeStm(null, new DecJ(this, tmp1));
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
