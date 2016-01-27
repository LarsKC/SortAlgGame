using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Statements
{
    /// <summary>
    /// Die Klasse Statement repraesentiert alle Bausteine, die keine in sich geschachtelten Bausteine enthalten 
    /// (z.B. einfache Variablenzuweisung).
    /// </summary>
    class Statement
    {
        #region Member
        /// <summary>
        /// Einrueckung des Bausteintexts.
        /// </summary>
        protected int indent = 0;
        /// <summary>
        /// Bausteintext.
        /// </summary>
        protected string content;
        /// <summary>
        /// Programm Objekt, zu dem der Baustein gehoert.
        /// </summary>
        protected Programm programm;
        /// <summary>
        /// Baustein, in dem sich dieser Baustein befindet.
        /// </summary>
        protected ListStm parent;
        /// <summary>
        /// Enumerator zum Bestimmen der Bausteinart.
        /// </summary>
        protected Config.EXECUTE brick;
        #endregion

        #region Accessoren
        /// <summary>
        /// indent Accessor
        /// </summary>
        public virtual int Indent
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
                    content = content.Substring(dif * (-1) * 4);
                    indent = value;
                }
            }
        }
        /// <summary>
        /// content Accessor
        /// </summary>
        public string Content
        {
            get { return content; }
        }
        /// <summary>
        /// parrent Accessor
        /// </summary>
        public ListStm Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        /// <summary>
        /// programm Accessor
        /// </summary>
        public Programm Programm
        {
            get { return programm; }
        }
        /// <summary>
        /// brick Accessor
        /// </summary>
        public Config.EXECUTE Brick
        {
            get { return brick; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Kontruktor.
        /// </summary>
        /// <param name="programm">Die Programm Klasse, zu dem der Baustein gehoert.<</param>
        /// <param name="parent">Der Baustein, in dem sich dieser Baustein befindet.</param>
        /// <param name="brick">Art des Bausteins.</param>
        public Statement(Programm programm, ListStm parent, Config.EXECUTE brick)
        {
            this.programm = programm;
            this.parent = parent;
            this.brick = brick;
            this.content = switchContent(brick);
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Erstellt aus dem aktuellen DataSet Objekt der Speicherverwaltung einen neuen Logeintrag.
        /// </summary>
        protected void updateLog()
        {
            programm.Log.AddLast(new Tuple<Statement, DataSet>(this, new DataSet(programm.Stack.Peek())));
        }
        /// <summary>
        /// Bestimmt den Bausteintext.
        /// </summary>
        /// <param name="brick">Bausteinart.</param>
        /// <returns>Bausteintext.</returns>
        protected virtual string switchContent(Config.EXECUTE brick)
        {
            switch (brick)
            {
                case Config.EXECUTE.ALLOC_ALENGHT:
                    return "n = right +1;";
                case Config.EXECUTE.ALLOC_I_TO_J:
                    return "j = i;";
                case Config.EXECUTE.ALLOC_I_TO_MIN:
                    return "min = i;";
                case Config.EXECUTE.ALLOC_J_TO_MIN:
                    return "min = j;";
                case Config.EXECUTE.ALLOC_LEFT:
                    return "i = left";
                case Config.EXECUTE.ALLOC_PIVOT:
                    return "pivot = a[(int)((left+right)/2)];";
                case Config.EXECUTE.ALLOC_RIGHT:
                    return "j = right;";
                case Config.EXECUTE.CALL_SORT_LEFT:
                    return "sort(a, left, j);";
                case Config.EXECUTE.CALL_SORT_RIGHT:
                    return "sort (a, i, right);";
                case Config.EXECUTE.DEC_J:
                    return "j--;";
                case Config.EXECUTE.INC_I:
                    return "i++;";
                case Config.EXECUTE.LOOP_END:
                    return "}";
                case Config.EXECUTE.SWAP_AI_WITH_AI_INC:
                    return "swap(a, i, i+1);";
                case Config.EXECUTE.SWAP_AI_WITH_AJ:
                    return "swap(a, i, j);";
                case Config.EXECUTE.SWAP_AJ_WITH_AJ_DEC:
                    return "swap (a, j, j-1);";
                case Config.EXECUTE.SWAP_AMIN_WITH_AI:
                    return "swap (a, min, i);";
                default:
                    return null;
            }
        }
        /// <summary>
        /// Führt den zur Bausteinart passenden Programmcode aus.
        /// </summary>
        /// <param name="buildLog">Bestimmt, ob ein Log angelegt werden soll.</param>
        /// <returns>Aufgetretener Fehler. Null, wenn kein Fehler gefunden wurde.</returns>
        public virtual string execute(bool buildLog)
        {
            DataSet actDataSet = programm.Stack.Peek();
            switch (brick)
            {
                case Config.EXECUTE.ALLOC_ALENGHT:
                    actDataSet.N = actDataSet.Right + 1;
                    break;
                case Config.EXECUTE.ALLOC_I_TO_J:
                    if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    actDataSet.J = actDataSet.I;
                    break;
                case Config.EXECUTE.ALLOC_I_TO_MIN:
                    if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    actDataSet.Min = actDataSet.I;
                    break;
                case Config.EXECUTE.ALLOC_J_TO_MIN:
                    if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    actDataSet.Min = actDataSet.J;
                    break;
                case Config.EXECUTE.ALLOC_LEFT:
                    actDataSet.I = actDataSet.Left;
                    break;
                case Config.EXECUTE.ALLOC_PIVOT:
                    actDataSet.Pivot = actDataSet.A[(int)((actDataSet.Left + actDataSet.Right) / 2)];
                    break;
                case Config.EXECUTE.ALLOC_RIGHT:
                    actDataSet.J = actDataSet.Right;
                    break;
                case Config.EXECUTE.CALL_SORT_LEFT:
                    if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    DataSet newDataSetLeft = new DataSet(actDataSet.A);
                    newDataSetLeft.Left = actDataSet.Left;
                    newDataSetLeft.Right = actDataSet.J;
                    programm.Stack.Push(newDataSetLeft);
                    if (buildLog) updateLog();
                    string tmpErrorLeft = programm.Stm.execute(buildLog);
                    if (tmpErrorLeft != null) return tmpErrorLeft;
                    newDataSetLeft = programm.Stack.Pop();
                    actDataSet = programm.Stack.Peek();
                    actDataSet.A = newDataSetLeft.A;
                    break;
                case Config.EXECUTE.CALL_SORT_RIGHT:
                    if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    DataSet newDataSetRight = new DataSet(actDataSet.A);
                    newDataSetRight.Left = actDataSet.I;
                    newDataSetRight.Right = actDataSet.Right;
                    programm.Stack.Push(newDataSetRight);
                    if (buildLog) updateLog();
                    string tmpErrorRight = programm.Stm.execute(buildLog);
                    if (tmpErrorRight != null) return tmpErrorRight;
                    newDataSetRight = programm.Stack.Pop();
                    actDataSet = programm.Stack.Peek();
                    actDataSet.A = newDataSetRight.A;
                    break;
                case Config.EXECUTE.DEC_J:
                    if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    actDataSet.J--;
                    break;
                case Config.EXECUTE.INC_I:
                    if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    actDataSet.I++;
                    break;
                case Config.EXECUTE.SWAP_AI_WITH_AI_INC:
                    if (actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        int tmp = actDataSet.A[actDataSet.I];
                        actDataSet.A[actDataSet.I] = actDataSet.A[actDataSet.I + 1];
                        actDataSet.A[actDataSet.I + 1] = tmp;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    break;
                case Config.EXECUTE.SWAP_AI_WITH_AJ:
                    if (actDataSet.I == Config.NOT_USED || actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        int tmp = actDataSet.A[actDataSet.I];
                        actDataSet.A[actDataSet.I] = actDataSet.A[actDataSet.J];
                        actDataSet.A[actDataSet.J] = tmp;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    break;
                case Config.EXECUTE.SWAP_AJ_WITH_AJ_DEC:
                    if (actDataSet.J == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        int tmp = actDataSet.A[actDataSet.J];
                        actDataSet.A[actDataSet.J] = actDataSet.A[actDataSet.J - 1];
                        actDataSet.A[actDataSet.J - 1] = tmp;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    break;
                case Config.EXECUTE.SWAP_AMIN_WITH_AI:
                    if (actDataSet.Min == Config.NOT_USED || actDataSet.I == Config.NOT_USED) return Config.NOT_INIT_ERROR;
                    try
                    {
                        int tmp = actDataSet.A[actDataSet.Min];
                        actDataSet.A[actDataSet.Min] = actDataSet.A[actDataSet.I];
                        actDataSet.A[actDataSet.I] = tmp;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return Config.OUT_OF_RANGE_ERROR;
                    }
                    break;
                default:
                    //Nothing
                    break;
            }
            if (buildLog) updateLog();
            return null;
        }
        #endregion
    }
}
