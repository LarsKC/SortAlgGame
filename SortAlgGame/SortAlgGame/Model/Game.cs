using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;

namespace SortAlgGame.Model
{
    class Game
    {
        #region Variablen
        private Player _player1;
        private Player _player2;
        private Player _winner;
        private Random _rnd;
        private DateTime _startTime;
        private int[][] _testArrays;
        private bool _playerRdy;
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

        public bool PlayerRdy
        {
            get { return _playerRdy; }
            set { _playerRdy = value; }
        }
        #endregion

        #region Konstruktoren
        public Game()
        {
            _player1 = new Player();
            _player2 = new Player();
            _rnd = new Random();
            _playerRdy = false;
            _startTime = DateTime.Now;
            _testArrays = new int[Config.RUNS.Length][];
            for (int i = 0; i < _testArrays.Length; i++ )
            {
                _testArrays[i] = getRndArray(Config.RUNS[i]);
            }
        }
        #endregion

        #region Methods
        public int[] getRndArray(int length)
        {
            int[] array = new int[length];
            foreach (int i in array)
            {
                array[i] = _rnd.Next(Config.MIN, Config.MAX + 1);
            }
            return array;
        }

        /*public void run()
        {
            for (int i = 0; i < _testArrays.Length; i++)
            {
                if (i == 0)
                {
                    _player1.init(_testArrays[i]);
                    _player1.Stm.execute(true);
                    _player2.init(_testArrays[i]);
                    _player2.Stm.execute(true);
                }
                else
                {
                    _player1.resetStack(_testArrays[i]);
                    _player1.Stm.execute(false);
                    _player2.resetStack(_testArrays[i]);
                    _player2.Stm.execute(false);
                }
            }
        }*/
        #endregion
    }
}
