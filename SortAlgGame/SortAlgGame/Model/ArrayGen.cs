using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model
{
    class ArrayGen
    {
        private Random _rnd;

        public ArrayGen()
        {
            _rnd = new Random();
        }

        public int[][] getTestArrays()
        {
            int[][] testArrays = new int[Config.RUNS.Length][];
            for (int i = 0; i < testArrays.Length; i++)
            {
                testArrays[i] = getRndArray(Config.RUNS[i]);
            }
            return testArrays;
        }

        public int[] getRndArray(int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = _rnd.Next(length + 1);
            }
            return array;
        }
    }
}
