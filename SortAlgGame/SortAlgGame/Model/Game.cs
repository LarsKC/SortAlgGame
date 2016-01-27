using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;
using System.Windows.Threading;

namespace SortAlgGame.Model
{
    /// <summary>
    /// Die Game Klasse repraesentiert die Geschaeftslogik fuer das Spiel
    /// </summary>
    class Game
    {
        #region Member
        /// <summary>
        /// Referenz auf den Gewinner. Null wenn unentschieden.
        /// </summary>
        private Player _winner;
        /// <summary>
        /// ArrayGen Objekt. Zum Erstellen von Zahlenfolgen.
        /// </summary>
        private ArrayGen _arrayGen;
        /// <summary>
        /// Zahlenfolgen, auf die der Algorithmus angewandt werden soll.
        /// </summary>
        private int[][] _testArrays;
        /// <summary>
        /// Referenz auf den schnelleren Spieler. Null wenn beide gleich schnell sind.
        /// </summary>
        private Player _fastestPlayer;
        /// <summary>
        /// Referenz auf den Spieler 1
        /// </summary>
        private Player _p1;
        /// <summary>
        /// Referenz auf dne Spieler 2
        /// </summary>
        private Player _p2;
        #endregion

        #region Accessoren
        /// <summary>
        /// _winner Accessor
        /// </summary>
        public Player Winner
        {
            get { return _winner; }
        }
        /// <summary>
        /// _fastestPlayer Accessor
        /// </summary>
        public Player FastestPlayer
        {
            get { return _fastestPlayer; }
        }
        /// <summary>
        /// _p1 Accessor
        /// </summary>
        public Player P1
        {
            get { return _p1; }
        }
        /// <summary>
        /// _p2 Accessor
        /// </summary>
        public Player P2
        {
            get { return _p2; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        public Game()
        {
            _arrayGen = new ArrayGen();
            _testArrays = _arrayGen.getTestArrays();
            _winner = null;
            _fastestPlayer = null;
            _p1 = new Player();
            _p2 = new Player();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Auswertung des Spiels.
        /// </summary>
        public void evaluate()
        {
            _p1.Programm.execute(_testArrays[0], true);
            _p2.Programm.execute(_testArrays[0], true);
            updateProgrammStats();
            for (int i = 1; i < Config.RUNS.Length; i++)
            {
                _p1.Programm.execute(_testArrays[i], false);
                _p2.Programm.execute(_testArrays[i], false);
                updateProgrammStats();
            }
            if (_p1.Time < _p2.Time)
            {
                _fastestPlayer = _p1;
                _p1.Points++;
            }
            else if (_p1.Time > _p2.Time)
            {
                _fastestPlayer = _p2;
                _p2.Points++;
            }
            if (_p1.Points > _p2.Points)
            {
                _winner = _p1;
            }
            else if (_p1.Points < _p2.Points)
            {
                _winner = _p2;
            }
        }
        /// <summary>
        /// Speichert das Ergebnis des aktuellen Testfalls, der auf den Algorithmus angewandt wird.
        /// </summary>
        private void updateProgrammStats()
        {
            int p1RoundWin = 0;
            int p2RoundWin = 0;
            bool p1Sorted = _p1.arraySorted();
            bool p2Sorted = _p2.arraySorted();

            if (p1Sorted && p2Sorted && _p1.runtimeAvailable() && _p2.runtimeAvailable())
            {
                if (int.Parse(_p1.Programm.ProgrammStats.Item3) > int.Parse(_p2.Programm.ProgrammStats.Item3))
                {
                    p2RoundWin = 1;
                    _p2.Points++;
                }
                else if (int.Parse(_p1.Programm.ProgrammStats.Item3) < int.Parse(_p2.Programm.ProgrammStats.Item3))
                {
                    p1RoundWin = 1;
                    _p1.Points++;
                }
            }
            else if (p1Sorted && _p1.runtimeAvailable())
            {
                p1RoundWin = 1;
                _p1.Points++;
            }
            else if (p2Sorted && _p2.runtimeAvailable())
            {
                p2RoundWin = 1;
                _p2.Points++;
            }
            _p1.addActStat(p1Sorted, p1RoundWin);
            _p2.addActStat(p2Sorted, p2RoundWin);
        }
        #endregion
    }
}
