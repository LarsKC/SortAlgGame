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
        private Player _player1;
        private Player _player2;
        private Player _quicksortPrg;
        private Player _winner;
        private ArrayGen _arrayGen;
        private int[][] _testArrays;
        private List<Tuple<int, string, string, string, int>> _p1PointList;
        private List<Tuple<int, string, string, string, int>> _p2PointList;
        private int _p1Points;
        private int _p2Points;
        private int _p1Time;
        private int _p2Time;
        private Player _fastestPlayer;

        #endregion

        #region Accessor
        public Player Player1
        {
            get { return _player1; }
        }

        public Player Player2
        {
            get { return _player2; }
        }

        public Player Winner
        {
            get { return _winner; }
        }

        public List<Tuple<int, string, string, string, int>> P1PointList
        {
            get { return _p1PointList; }
        }

        public List<Tuple<int, string, string, string, int>> P2PointList
        {
            get { return _p2PointList; }
        }

        public int P1Points
        {
            get { return _p1Points; }
        }

        public int P2Points
        {
            get { return _p2Points; }
        }

        public int P1Time
        {
            get { return _p1Time; }
            set { _p1Time = value; }
        }

        public int P2Time
        {
            get { return _p2Time; }
            set { _p2Time = value; }
        }

        public Player FastestPlayer
        {
            get { return _fastestPlayer; }
        }

        #endregion

        #region Konstruktoren
        public Game()
        {
            _player1 = new Player();
            _player2 = new Player();
            _quicksortPrg = new Player();
            _quicksortPrg.buildBubblesort();
            _arrayGen = new ArrayGen();
            _testArrays = _arrayGen.getTestArrays();
            _p1PointList = new List<Tuple<int, string, string, string, int>>();
            _p2PointList = new List<Tuple<int, string, string, string, int>>();
            _p1Points = 0;
            _p2Points = 0;
            _winner = null;
            _p1Time = 0;
            _p2Time = 0;
            _fastestPlayer = null;
        }
        #endregion

        #region Methods

        public void evaluate()
        {
            _quicksortPrg.execute(_testArrays[0], true);
            _player1.execute(_testArrays[0], true);
            _player2.execute(_testArrays[0], true);
            updateProgrammStats();
            for (int i = 1; i < Config.RUNS.Length; i++)
            {
                _quicksortPrg.execute(_testArrays[i], false);
                _player1.execute(_testArrays[i], false);
                _player2.execute(_testArrays[i], false);
                updateProgrammStats();
            }
            if (_p1Time < _p2Time)
            {
                _fastestPlayer = Player1;
                _p1Points++;
            }
            else if (_p1Time > _p2Time)
            {
                _fastestPlayer = Player2;
                _p2Points++;
            }
            if (_p1Points > _p2Points)
            {
                _winner = _player1;
            }
            else if (_p1Points < _p2Points)
            {
                _winner = _player2;
            }
        }

        public void updateProgrammStats()
        {
            int p1RoundWin = 0;
            int p2RoundWin = 0;
            bool p1Sorted = Enumerable.SequenceEqual<int>(_quicksortPrg.Stack.Peek().A, _player1.Stack.Peek().A);
            bool p2Sorted = Enumerable.SequenceEqual<int>(_quicksortPrg.Stack.Peek().A, _player2.Stack.Peek().A);

            if (p1Sorted && p2Sorted && _player1.ProgrammStats.Item3 != Config.RUNTIME_NA && _player2.ProgrammStats.Item3 != Config.RUNTIME_NA)
            {
                if (int.Parse(_player1.ProgrammStats.Item3) > int.Parse(_player2.ProgrammStats.Item3))
                {
                    p2RoundWin = 1;
                    _p2Points++;
                }
                else if (int.Parse(_player1.ProgrammStats.Item3) < int.Parse(_player2.ProgrammStats.Item3))
                {
                    p1RoundWin = 1;
                    _p1Points++;
                }
            }
            else if (p1Sorted && _player1.ProgrammStats.Item3 != Config.RUNTIME_NA)
            {
                p1RoundWin = 1;
                _p1Points++;
            }
            else if (p2Sorted && _player2.ProgrammStats.Item3 != Config.RUNTIME_NA)
            {
                p2RoundWin = 1;
                _p2Points++;
            }
            _p1PointList.Add(new Tuple<int, string, string, string, int>(
                _player1.ProgrammStats.Item1,
                _player1.ProgrammStats.Item2,
                _player1.ProgrammStats.Item3,
                (p1RoundWin == 1) ? "ja" : "nein",
                p1RoundWin));
            _p2PointList.Add(new Tuple<int, string, string, string, int>(
                _player2.ProgrammStats.Item1,
                _player2.ProgrammStats.Item2,
                _player2.ProgrammStats.Item3,
                (p2RoundWin == 1) ? "ja" : "nein",
                p2RoundWin));
        }
        #endregion
    }
}
