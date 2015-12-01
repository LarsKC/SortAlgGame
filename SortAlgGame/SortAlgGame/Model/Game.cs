using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgGame.Model.Statements;

namespace SortAlgGame.Model
{
    class Game
    {

        PlayerProgramm player1;
        PlayerProgramm player2;
        PlayerProgramm winner;
        Dictionary<int, int> runtimePlayer1;
        Dictionary<int, int> runtimePlayer2;
        DateTime startTime;
        DateTime finPlayer1;
        DateTime finPlayer2;
        TimeSpan tsPlayer1;
        TimeSpan tspPlayer2;
        LinkedList<Tuple<Statement, DataSet>> logPlayer1;
        LinkedList<Tuple<Statement, DataSet>> logPlayer2;
        Random rnd;

        public Game()
        {
            this.player1 = new PlayerProgramm();
            this.player2 = new PlayerProgramm();
            this.winner = null;
            this.rnd = new Random();
            this.startTime = DateTime.Now;
        }

        public void evaluateGame()
        {
            foreach (int i in Config.RUNS)
            {
                int[] testArray = getRndArray(i);
                player1.initData(testArray);
                player2.initData(testArray);
                player1.Stm.execute();
                player2.Stm.execute();
                runtimePlayer1.Add(i, player1.Runtime);
                runtimePlayer2.Add(i, player2.Runtime);
                if (i == Config.RUNS[1])
                {
                    logPlayer1 = new LinkedList<Tuple<Statement,DataSet>>(player1.Log);
                    logPlayer2 = new LinkedList<Tuple<Statement, DataSet>>(player2.Log);
                }


            }
            tsPlayer1 = finPlayer1 - startTime;
            tspPlayer2 = finPlayer2 - startTime;


            
        }

        public int[] getRndArray(int length)
        {
            int[] array = new int[length];
            foreach (int i in array)
            {
                array[i] = rnd.Next(Config.MIN, Config.MAX+1);
            }
            return array;
        }

        public void stopTimePlayer1()
        {
            this.finPlayer1 = DateTime.Now;
        }

        public void stopTimePlayer2()
        {
            this.finPlayer2 = DateTime.Now;
        }
    }
}
