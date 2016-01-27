using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model
{
    /// <summary>
    /// Die Klasse Player repraesentiert einen Spieler im Spiel der Applikation.
    /// </summary>
    class Player
    {
        #region Member
        /// <summary>
        /// Referenz auf das Programm des Spielers.
        /// </summary>
        private Programm _programm;
        /// <summary>
        /// Liste von einem 5-Tuple. Beinhaltet die ausgewerteten Daten für alle Testfaelle: Laenge des Testarrays, Fehlerbericht, 
        /// Laufzeit, Sortierung, Punkte.
        /// </summary>
        private List<Tuple<int, string, string, string, int>> _pointList;
        /// <summary>
        /// Gesamtpunktzahl des Spielers.
        /// </summary>
        private int _points;
        /// <summary>
        /// Vom spieler benoetigte Zeit zum zusammenbauen seines Algorithmus.
        /// </summary>
        private int _time;
        #endregion

        #region Accessoren
        /// <summary>
        /// _programm Accessor
        /// </summary>
        public Programm Programm
        {
            get { return _programm; }
        }
        /// <summary>
        /// _pointList Accessor
        /// </summary>
        public List<Tuple<int, string, string, string, int>> PointList
        {
            get { return _pointList; }
            set { _pointList = value; }
        }
        /// <summary>
        /// _points Accessor
        /// </summary>
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }
        /// <summary>
        /// _time Accessor
        /// </summary>
        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }
        #endregion
        
        #region Konstruktoren
        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        public Player()
        {
            _programm = new Programm();
            _pointList = new List<Tuple<int, string, string, string, int>>();
            _points = 0;
            _time = 0;
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Fuegt ein neues 5-Tuple in die _pointList ein.
        /// </summary>
        /// <param name="sorted">Sortierung</param>
        /// <param name="roundPoint">In dem Testfall erreichte Punktzahl (0 oder 1)</param>
        public void addActStat(bool sorted, int roundPoint)
        {
            _pointList.Add(new Tuple<int, string, string, string, int>(
                Programm.ProgrammStats.Item1,
                Programm.ProgrammStats.Item2,
                Programm.ProgrammStats.Item3,
                (sorted) ? "ja" : "nein",
                roundPoint));
        }
        /// <summary>
        /// Kontrolliert ob die aktuelle Zahlenfolge in der Speicherverwaltung sortiert ist.
        /// </summary>
        /// <returns>True wenn sortiert. False wenn unsortiert.</returns>
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
        /// <summary>
        /// Kontrolliert ob eine Laufzeit ermittelt werden konnte.
        /// </summary>
        /// <returns>True, wenn Laufzeit ermittelt werden konnte. False, wenn nicht. </returns>
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
