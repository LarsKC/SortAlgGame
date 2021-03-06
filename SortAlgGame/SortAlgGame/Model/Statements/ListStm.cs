﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    /// <summary>
    /// Diese Klasse ListStm repraesentiert die If-, For- und While-Bausteine.
    /// </summary>
    class ListStm : Statement
    {
        #region Member
        /// <summary>
        /// Liste der im Block enthaltenen Bausteine
        /// </summary>
        protected LinkedList<Statement> stmList;
        #endregion

        #region Accessoren
        /// <summary>
        /// stmList Accessor.
        /// </summary>
        public LinkedList<Statement> StmList
        {
            get { return stmList; }
        }

        /// <summary>
        /// indent Accessor. Ueberschreibt den indent Accessor der Klasse Statement. Bestimmt die Einrueckung des Bausteins
        /// </summary>
        public override int Indent
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
                    content = content.Substring(dif*(-1)*4);
                    indent = value;
                }

                for (int i = 0; i < StmList.Count -1; i++)
                {
                    StmList.ElementAt<Statement>(i).Indent = this.Indent + 1;
                }
                StmList.Last.Value.Indent = this.Indent;
            }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor. Ruft zusaetzlich den Konstruktor der Klasse Statement auf.
        /// </summary>
        /// <param name="programm">Die Programm Klasse, zu dem der Baustein gehoert.</param>
        /// <param name="parent">Der Baustein, in dem sich dieser Baustein befindet.</param>
        /// <param name="brick">Art des Bausteins.</param>
        public ListStm(Programm programm, ListStm parent, Config.EXECUTE brick)
            : base(programm, parent, brick)
        {
            stmList = new LinkedList<Statement>();
            stmList.AddLast(new Statement(programm, this, Config.EXECUTE.LOOP_END));            
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Ruft die execute Methode fuer alle in diesem Baustein enthaltenen Statements auf.
        /// </summary>
        /// <param name="buildLog">Legt fest oft ein Log erstellt werden soll.</param>
        /// <returns>Aufgetretene Fehlermeldung. Null wenn kein Fehler festgestellt wurde.</returns>
        public string executeList(bool buildLog)
        {
            string tmpError = null;
            for(int i = 0; i < stmList.Count && tmpError == null; i++)
            {
                 tmpError = stmList.ElementAt(i).execute(buildLog);
            }
            return tmpError;
        }

        /// <summary>
        /// Fuegt einen neuen Baustein an der uebergebenen Position ein.
        /// </summary>
        /// <param name="pos">Position des neuen Bausteins.</param>
        /// <param name="stm">Einzufuegender Baustein.</param>
        public void addBeforeStm(Statement pos, Statement stm)
        {
            if (pos == null)
            {
                StmList.AddBefore(StmList.Last, stm);
            }
            else
            {
                StmList.AddBefore(StmList.Find(pos), stm);
            }
            stm.Parent = this;
            stm.Indent = this.Indent + 1;
        }

        /// <summary>
        /// Entfernt den uebergebenen Baustein aus der stmList.
        /// </summary>
        /// <param name="stm">Zu entfernender Baustein.</param>
        public void removeStm(Statement stm)
        {
            stm.Parent = null;
            stm.Indent = 0;
            if (stm is ListStm)
            {
                for (int i = (stm as ListStm).StmList.Count - 2; i  >= 0; i--)
                {

                    (stm as ListStm).StmList.ElementAt<Statement>(i).Indent = this.Indent + 1;
                    this.addBeforeStm(stm, (stm as ListStm).StmList.ElementAt<Statement>(i));
                    (stm as ListStm).StmList.Remove((stm as ListStm).StmList.ElementAt<Statement>(i));
                }
            }
            stmList.Remove(stm);
        }

        /// <summary>
        /// Prueft ob sich der uebergebene Baustein in der stmList befindet.
        /// </summary>
        /// <param name="stm">Gesuchter Baustein.</param>
        /// <param name="listStm">Zu durchsuchende Liste.</param>
        /// <returns>True, wenn der Baustein gefunden wurde. False, wenn nicht.</returns>
        public bool stmListContains(Statement stm, ListStm listStm)
        {
            if (stm == listStm)
                return true;

            foreach (Statement x in listStm.StmList)
            {
                if (x is ListStm)
                {
                    if (stmListContains(stm, x as ListStm))
                        return true;
                }
                else if(x == stm)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Aktualisiert die Speicherverwaltung des Algorithmus.
        /// </summary>
        private void updateDataSets()
        {
            DataSet actDataSet = Programm.Stack.Pop();
            DataSet oldDataSet = Programm.Stack.Pop();
            if (oldDataSet.I == Config.NOT_USED) actDataSet.I = Config.NOT_USED;
            if (oldDataSet.J == Config.NOT_USED) actDataSet.J = Config.NOT_USED;
            if (oldDataSet.N == Config.NOT_USED) actDataSet.N = Config.NOT_USED;
            if (oldDataSet.Min == Config.NOT_USED) actDataSet.Min = Config.NOT_USED;
            if (oldDataSet.Pivot == Config.NOT_USED) actDataSet.Pivot = Config.NOT_USED;
            programm.Stack.Push(actDataSet);
        }

        /// <summary>
        /// Bestimmt den anzuzeigenden Text des Bausteins
        /// </summary>
        /// <param name="brick">Art des Bausteins</param>
        /// <returns>Bausteintext.</returns>
        protected override string switchContent(Config.EXECUTE brick)
        {
            switch (brick)
            {
                case Config.EXECUTE.BASE_STM:
                    return "public sort(int[] a, int left, int right) {";
                case Config.EXECUTE.FOR_IN_BUBBLE:
                    return "for (int i = left; i < n-1; i++) {";
                case Config.EXECUTE.FOR_IN_SELECTION:
                    return "for (int j = i+1; j < n; j++) {";
                case Config.EXECUTE.FOR_INSERTION:
                    return "for (int i = left+1; i < n; i++) {";
                case Config.EXECUTE.FOR_OUT_BUBBLE:
                    return "for (int n = right+1; n > 1; n--) {";
                case Config.EXECUTE.IF_AI_GREATER_AI_INC:
                    return "if (a[i] > a[i+1]) {";
                case Config.EXECUTE.IF_AJ_LESS_AMIN:
                    return "if (a[j] < a[min]) {";
                case Config.EXECUTE.IF_I_LESS_EQUALS_J:
                    return "if (i <= j) {";
                case Config.EXECUTE.IF_I_LESS_RIGHT:
                    return "if (i < right) {";
                case Config.EXECUTE.IF_I_NOTEQUALS_MIN:
                    return "if (i != min) {";
                case Config.EXECUTE.IF_LEFT_LESS_J:
                    return "if (left < j) {";
                case Config.EXECUTE.WHILE_AI_LESS_PIVOT:
                    return "while (a[i] < pivot) {";
                case Config.EXECUTE.WHILE_AJ_GREATER_PIVOT:
                    return "while (a[j] > pivot) {";
                case Config.EXECUTE.WHILE_I_LESS_EQUALS_J:
                    return "while(i <= j) {";
                case Config.EXECUTE.WHILE_J_GREATER_NULL:
                    return "while (j > 0 && a[j-1] > a[j]) {";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Sorgt für das Ausfuehren des zum Baustein gehoerenden Programmcodes.
        /// </summary>
        /// <param name="buildLog">Bestimmt, ob ein Log angelegt werden soll.</param>
        /// <returns>Aufgetretener Fehler. Null, wenn kein Fehler gefunden wurde.</returns>
        public override string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            if (this.Brick == Config.EXECUTE.BASE_STM)
            {
                actDataSet.Left = (actDataSet.Left == Config.NOT_USED) ? 0 : actDataSet.Left;
                actDataSet.Right = (actDataSet.Right == Config.NOT_USED) ? actDataSet.A.Length - 1 : actDataSet.Right;
                if (buildLog) updateLog();
                return executeList(buildLog);
            }
            else
            {
                programm.Stack.Push(new DataSet(actDataSet));
                actDataSet = programm.Stack.Peek();
                string tmpError = null;
                switch (brick)
                {
                    case Config.EXECUTE.FOR_IN_BUBBLE:
                    case Config.EXECUTE.FOR_IN_SELECTION:
                    case Config.EXECUTE.FOR_INSERTION:
                    case Config.EXECUTE.FOR_OUT_BUBBLE:
                        tmpError = switchOnFor(actDataSet, buildLog);
                        break;
                    case Config.EXECUTE.IF_AI_GREATER_AI_INC:
                    case Config.EXECUTE.IF_AJ_LESS_AMIN:
                    case Config.EXECUTE.IF_I_LESS_EQUALS_J:
                    case Config.EXECUTE.IF_I_LESS_RIGHT:
                    case Config.EXECUTE.IF_I_NOTEQUALS_MIN:
                    case Config.EXECUTE.IF_LEFT_LESS_J:
                        tmpError = switchOnIf(actDataSet, buildLog);
                        break;
                    case Config.EXECUTE.WHILE_AI_LESS_PIVOT:
                    case Config.EXECUTE.WHILE_AJ_GREATER_PIVOT:
                    case Config.EXECUTE.WHILE_I_LESS_EQUALS_J:
                    case Config.EXECUTE.WHILE_J_GREATER_NULL:
                        tmpError = switchOnWhile(actDataSet, buildLog);
                        break;
                    default:
                        //NOTHING
                        break;

                }
                updateDataSets();
                return tmpError;
            }
        }

        /// <summary>
        /// Bestimmt den Programmcode einer For-Schleife.
        /// </summary>
        /// <param name="actDataSet">Aktuelle Speicherbelegung in der Speicheverwaltung des Algorithmus.</param>
        /// <param name="buildLog">Bestimmt, ob ein Log angelegt werden soll.</param>
        /// <returns>Aufgetretener Fehler. Null, wenn kein Fehler gefunden wurde.</returns>
        private string switchOnFor(DataSet actDataSet, bool buildLog)
        {
            string tmpError = null;
            switch(this.brick)
            {
                case Config.EXECUTE.FOR_IN_BUBBLE:
                    if (actDataSet.N == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    for (int i = actDataSet.Left; i < actDataSet.N - 1; i++)
                    {
                        actDataSet.I = i;
                        tmpError = runChildStatements(actDataSet, buildLog);
                        if (tmpError != null) return tmpError;
                        actDataSet = programm.Stack.Peek();
                    }
                    break;
                case Config.EXECUTE.FOR_IN_SELECTION:
                    if (actDataSet.I == Config.NOT_USED || actDataSet.N == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    for (int j = actDataSet.I + 1; j < actDataSet.N; j++)
                    {
                        actDataSet.J = j;
                        tmpError = runChildStatements(actDataSet, buildLog);
                        if (tmpError != null) return tmpError;
                        actDataSet = programm.Stack.Peek();
                    }
                    break;
                case Config.EXECUTE.FOR_INSERTION:
                    if (actDataSet.N == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    for (int i = actDataSet.Left + 1; i < actDataSet.N; i++)
                    {
                        actDataSet.I = i;
                        tmpError = runChildStatements(actDataSet, buildLog);
                        if (tmpError != null) return tmpError;
                        actDataSet = programm.Stack.Peek();
                    }
                    break;
                case Config.EXECUTE.FOR_OUT_BUBBLE:
                    for (int n = actDataSet.Right + 1; n > 1; n--)
                    {
                        actDataSet.N = n;
                        tmpError = runChildStatements(actDataSet, buildLog);
                        if (tmpError != null) return tmpError;
                        actDataSet = programm.Stack.Peek();
                    }
                    break;
                default:
                    //Nothing
                    break;
            }
            if (buildLog) updateLog();
            return tmpError;
        }

        /// <summary>
        /// Bestimmt die aktuelle Laufzeit und ruft die Methode zum Ausfuehren aller, in stmList enthaltenen Bausteine auf.
        /// </summary>
        /// <param name="actDataSet">Aktuelle Speicherbelegung in der Speicheverwaltung des Algorithmus.</param>
        /// <param name="buildLog">Bestimmt, ob ein Log angelegt werden soll.</param>
        /// <returns>Aufgetretener Fehler. Null, wenn kein Fehler gefunden wurde.</returns>
        private string runChildStatements(DataSet actDataSet, bool buildLog)
        {
            string tmpError;
            programm.ActRuntime++;
            if (programm.ActRuntime >= Config.MAX_RUNTIME(actDataSet.A.Length)) return Config.MAX_RUNTIME_ERROR;
            if (buildLog) updateLog();
            tmpError = executeList(buildLog);
            return tmpError;
        }

        /// <summary>
        /// Bestimmt den Programmcode einer If-Bedingung.
        /// </summary>
        /// <param name="actDataSet">Aktuelle Speicherbelegung in der Speicheverwaltung des Algorithmus.</param>
        /// <param name="buildLog">Bestimmt, ob ein Log angelegt werden soll.</param>
        /// <returns>Aufgetretener Fehler. Null, wenn kein Fehler gefunden wurde.</returns>
        private string switchOnIf(DataSet actDataSet, bool buildLog)
        {
            if (buildLog) updateLog();
            string tmpError = null;
            switch(this.brick)
            {
                case Config.EXECUTE.IF_AI_GREATER_AI_INC:
                    if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        if (actDataSet.A[actDataSet.I] > actDataSet.A[actDataSet.I + 1])
                        {
                            tmpError = executeList(buildLog);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    break;
                case Config.EXECUTE.IF_AJ_LESS_AMIN:
                    if (actDataSet.J == Config.NOT_USED || actDataSet.Min == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        if (actDataSet.A[actDataSet.J] < actDataSet.A[actDataSet.Min])
                        {
                            tmpError = executeList(buildLog);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    break;
                case Config.EXECUTE.IF_I_LESS_EQUALS_J:
                    if (actDataSet.I == Config.NOT_USED || actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    if (actDataSet.I <= actDataSet.J)
                    {
                        tmpError = executeList(buildLog);
                    }
                    break;
                case Config.EXECUTE.IF_I_LESS_RIGHT:
                    if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    if (actDataSet.I < actDataSet.Right)
                    {
                        tmpError = executeList(buildLog);
                    }
                    break;
                case Config.EXECUTE.IF_I_NOTEQUALS_MIN:
                    if (actDataSet.I == Config.NOT_USED || actDataSet.Min == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    if (actDataSet.I != actDataSet.Min)
                    {
                        tmpError = executeList(buildLog);
                    }
                    break;
                case Config.EXECUTE.IF_LEFT_LESS_J:
                    if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    if (actDataSet.Left < actDataSet.J)
                    {
                        tmpError = executeList(buildLog);
                    }
                    break;
                default:
                    //Nothing
                    break;
            }
            return tmpError;
        }

        /// <summary>
        /// Bestimmt den Programmcode einer While-Schleife.
        /// </summary>
        /// <param name="actDataSet">Aktuelle Speicherbelegung in der Speicheverwaltung des Algorithmus.</param>
        /// <param name="buildLog">Bestimmt, ob ein Log angelegt werden soll.</param>
        /// <returns>Aufgetretener Fehler. Null, wenn kein Fehler gefunden wurde.</returns>
        private string switchOnWhile(DataSet actDataSet, bool buildLog)
        {
            string tmpError = null;
            switch(this.brick)
            {
                case Config.EXECUTE.WHILE_AI_LESS_PIVOT:
                    if (actDataSet.I == Config.NOT_USED || actDataSet.Pivot == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        while (actDataSet.A[actDataSet.I] < actDataSet.Pivot)
                        {
                            tmpError = runChildStatements(actDataSet, buildLog);
                            if (tmpError != null) return tmpError;
                            actDataSet = programm.Stack.Peek();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    if (buildLog) updateLog();
                    break;
                case Config.EXECUTE.WHILE_AJ_GREATER_PIVOT:
                    if (actDataSet.J == Config.NOT_USED || actDataSet.Pivot == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        while (actDataSet.A[actDataSet.J] > actDataSet.Pivot)
                        {
                            tmpError = runChildStatements(actDataSet, buildLog);
                            if (tmpError != null) return tmpError;
                            actDataSet = programm.Stack.Peek();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    if (buildLog) updateLog();
                    break;
                case Config.EXECUTE.WHILE_I_LESS_EQUALS_J:
                    if (actDataSet.I == Config.NOT_USED || actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    while (actDataSet.I <= actDataSet.J)
                    {
                        tmpError = runChildStatements(actDataSet, buildLog);
                        if (tmpError != null) return tmpError;
                        actDataSet = programm.Stack.Peek();
                    }
                    if (buildLog) updateLog();
                    break;
                case Config.EXECUTE.WHILE_J_GREATER_NULL:
                    if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        while (actDataSet.J > 0 && actDataSet.A[actDataSet.J - 1] > actDataSet.A[actDataSet.J])
                        {
                            tmpError = runChildStatements(actDataSet, buildLog);
                            if (tmpError != null) return tmpError;
                            actDataSet = programm.Stack.Peek();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    if (buildLog) updateLog();
                    break;
                default:
                    //Nothing
                    break;
            }
            return tmpError;
        }
        #endregion
    }
}
