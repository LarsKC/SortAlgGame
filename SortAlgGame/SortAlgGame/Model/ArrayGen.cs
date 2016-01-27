using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model
{
    /// <summary>
    /// Dient zur Erstellung von zufaellig belegten Zahlenfolgen.
    /// </summary>
    class ArrayGen
    {
        #region Member
        /// <summary>
        /// Random Objekt zum bestimmen der Zahlen 
        /// </summary>
        private Random _rnd;
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ArrayGen()
        {
            _rnd = new Random();
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Erstellt vier zufaellig belegte Zahlenfolgen.
        /// </summary>
        /// <returns>Zweidimensionales Array, welches die generierten Zahlenfolgen enthaelt.</returns>
        public int[][] getTestArrays()
        {
            int[][] testArrays = new int[Config.RUNS.Length][];
            for (int i = 0; i < testArrays.Length; i++)
            {
                testArrays[i] = getRndArray(Config.RUNS[i]);
            }
            return testArrays;
        }
        /// <summary>
        /// Erstellt eine zufaellige Folge von Zahlen.
        /// </summary>
        /// <param name="length">Laenge der Zahlenfolge.</param>
        /// <returns>Generierte Zahlenfolge.</returns>
        public int[] getRndArray(int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = _rnd.Next(length + 1);
            }
            return array;
        }
        #endregion
    }
}
