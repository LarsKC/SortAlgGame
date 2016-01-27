using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;

namespace SortAlgGame.Model
{
    /// <summary>
    /// Die Klasse Programm repraesentiert den aus Bausteinen bestehenden Algorithmus und dessen Speicherverwaltung.
    /// </summary>
    class Programm
    {
        #region Member
        /// <summary>
        /// 3-Tuple, welches die aktuellen Daten, des auf einen Testfall angewandten Algorihmus enthaelt: Laenge der zu sortierenden 
        /// Zahlenfolge, Fehlerbericht, Laufzeit.
        /// </summary>
        private Tuple<int, string, string> _actStats;
        /// <summary>
        /// Enthaelt die Laufzeit des Algorithmus, die beim letzten Testfall ermittelt wurde.
        /// </summary>
        private int _actRuntime;
        /// <summary>
        /// Stellt den Log des Algorithmus dar. In ihm sind alle Datenbewegungen bzw. Variablenveraenderungen die in der 
        /// Speicherverwaltung des Algorithmus stattgefunden haben vermerkt. 
        /// </summary>
        private LinkedList<Tuple<Statement, DataSet>> _log;
        /// <summary>
        /// Stellt die Speicherverwaltung des Algorithmus in Form eines Stacks dar.
        /// </summary>
        private Stack<DataSet> _stack;
        /// <summary>
        /// Referenz auf den Startbaustein des Algorithmus.
        /// </summary>
        private ListStm _stm;
        #endregion

        #region Accessoren
        /// <summary>
        /// _actStats Accessor
        /// </summary>
        public Tuple<int, string, string> ProgrammStats
        {
            get { return _actStats; }
        }
        /// <summary>
        /// _stack Accessor
        /// </summary>
        public Stack<DataSet> Stack
        {
            get { return _stack; }
        }
        /// <summary>
        /// _stm Accessor
        /// </summary>
        public Statement Stm
        {
            get { return _stm; }
        }
        /// <summary>
        /// _log Accessor
        /// </summary>
        public LinkedList<Tuple<Statement, DataSet>> Log
        {
            get { return _log; }
        }
        /// <summary>
        /// _actRuntime Accessor
        /// </summary>
        public int ActRuntime
        {
            get { return _actRuntime; }
            set { _actRuntime = value; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        public Programm()
        {
            _actStats = null;
            _stm = new ListStm(this, null, Config.EXECUTE.BASE_STM);
            _stack = new Stack<DataSet>();
            _log = new LinkedList<Tuple<Statement, DataSet>>();
            _actRuntime = 0;
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Ruft die execute Methode des _stm auf und legt nach dessen Ausfaehrung die _actStats des Algorithmus fest.
        /// </summary>
        /// <param name="a">Zu sortierende Zahlenfolge.</param>
        /// <param name="genLog">Bestimmt ob ein Log angelegt werden soll.</param>
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
        /// <summary>
        /// Erstellt aus der rekursiven Schachtelung der Statement und ListStm Klassen eine Listenstruktur. Erwartet keinen Uebergabeparamter.
        /// </summary>
        /// <returns>Algorithmus Bausteine in Listenstruktur.</returns>
        public LinkedList<Statement> getActualStmNesting()
        {
            return getActualStmNesting(_stm);
        }
        /// <summary>
        /// Erstellt aus der rekursiven Schachtelung der Statement und ListStm Klassen eine Listenstruktur.
        /// </summary>
        /// <param name="baseStm">Referenz auf ein ListStm.</param>
        /// <returns>Algorithmus Bausteine in Form einer Liste.</returns>
        private LinkedList<Statement> getActualStmNesting(ListStm baseStm)
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
        /// <summary>
        /// Konkateniert zwei Listen die Statements beinhalten.
        /// </summary>
        /// <param name="first">Liste 1</param>
        /// <param name="second">Liste 2</param>
        /// <returns>Konkatenation der beiden uebergebenen Listen.</returns>
        private LinkedList<Statement> concatList(LinkedList<Statement> first, LinkedList<Statement> second)
        {
            LinkedList<Statement> newList = new LinkedList<Statement>(first);
            foreach (Statement x in second)
            {
                newList.AddLast(x);
            }
            return newList;
        }
        /// <summary>
        /// Erstellt die Statement und ListStm Schachtelung, die den Algorithmus Bubblesort repraesentiert.
        /// </summary>
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

        /// <summary>
        /// Erstellt die Statement und ListStm Schachtelung, die den Algorithmus Quicksort repraesentiert.
        /// </summary>
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
        }
        /// <summary>
        /// Erstellt die Statement und ListStm Schachtelung, die den Algorithmus Selectionsort repraesentiert.
        /// </summary>
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
        /// <summary>
        /// Erstellt die Statement und ListStm Schachtelung, die den Algorithmus Insertionsort repraesentiert.
        /// </summary>
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
        #endregion
    }
}
