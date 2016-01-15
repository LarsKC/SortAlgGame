using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model
{
    class Player
    {
        #region Variablen
        private Programm _programm;
        private List<Tuple<int, string, string, string, int>> _pointList;
        private int _points;
        private int _time;
        #endregion

        #region Accessoren
        public Programm Programm
        {
            get { return _programm; }
        }

        public List<Tuple<int, string, string, string, int>> PointList
        {
            get { return _pointList; }
            set { _pointList = value; }
        }

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }
        #endregion
        
        #region Konstruktoren
        public Player()
        {
            _programm = new Programm();
            _pointList = new List<Tuple<int, string, string, string, int>>();
            _points = 0;
            _time = 0;
        }
        #endregion

        #region Methoden
        public void addActStat(bool sorted, int roundPoint)
        {
            _pointList.Add(new Tuple<int, string, string, string, int>(
                Programm.ProgrammStats.Item1,
                Programm.ProgrammStats.Item2,
                Programm.ProgrammStats.Item3,
                (sorted) ? "ja" : "nein",
                roundPoint));
        }

        public bool arraySorted()
        {
            if (_programm.Stack.Peek() != null)
            {
                int[] a = _programm.Stack.Peek().A;
                for (int i = 0; i < a.Length - 1; i++)
                {
                    if (a[i] > a[i + 1])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public bool runtimeAvailable()
        {
            if (Programm.ProgrammStats.Item3 != Config.RUNTIME_NA && Programm.ProgrammStats.Item3 != null)
            {
                return true;
            }
            return false;
        }
        #endregion

    }
}
