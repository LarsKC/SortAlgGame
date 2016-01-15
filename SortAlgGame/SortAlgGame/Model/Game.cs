using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;
using System.Windows.Threading;

namespace SortAlgGame.Model
{
    class Game
    {
        #region Variablen
        private Player _winner;
        private ArrayGen _arrayGen;
        private int[][] _testArrays;
        private Player _fastestPlayer;
        private Player _p1;
        private Player _p2;

        #endregion

        #region Accessoren
        public Player Winner
        {
            get { return _winner; }
        }

        public Player FastestPlayer
        {
            get { return _fastestPlayer; }
        }

        public Player P1
        {
            get { return _p1; }
        }

        public Player P2
        {
            get { return _p2; }
        }

        #endregion

        #region Konstruktoren
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

        public void updateProgrammStats()
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
