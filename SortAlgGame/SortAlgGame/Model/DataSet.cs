using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model
{
    /// <summary>
    /// Die Klasse DataSet ist ein Teil der Speicherverwaltung des Algorithmus. In ihr befinden sich alle vom Algorithmus benoetigten Variablen.
    /// </summary>
    class DataSet
    {
        #region Member
        private int i;
        private int j;
        private int n;
        private int[] a;
        private int min;
        private int pivot;
        private int left;
        private int right;
        #endregion

        #region Accessoren
        /// <summary>
        /// i Accessor
        /// </summary>
        public int I
        {
            get { return i; }
            set { i = value; }
        }
        /// <summary>
        /// j Accessor
        /// </summary>
        public int J
        {
            get { return j; }
            set { j = value; }
        }
        /// <summary>
        /// n Accessor
        /// </summary>
        public int N
        {
            get { return n; }
            set { n = value; }
        }
        /// <summary>
        /// Array a Accessor
        /// </summary>
        public int[] A
        {
            get { return a; }
            set { a = value; }
        }
        /// <summary>
        /// min Accessor
        /// </summary>
        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        /// <summary>
        /// pivot Accessor
        /// </summary>
        public int Pivot
        {
            get { return pivot; }
            set { pivot = value; }
        }
        /// <summary>
        /// left Accessor
        /// </summary>
        public int Left
        {
            get { return left; }
            set { left = value; }
        }
        /// <summary>
        /// right Accessor
        /// </summary>
        public int Right
        {
            get { return right; }
            set { right = value; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        public DataSet()
        {
            this.i = Config.NOT_USED;
            this.j = Config.NOT_USED;
            this.n = Config.NOT_USED;
            this.a = null;
            this.min = Config.NOT_USED;
            this.pivot = Config.NOT_USED;
            this.left = Config.NOT_USED;
            this.right = Config.NOT_USED;
        }
        /// <summary>
        /// Konstruktor mit Array Uebergabe. Dabei wird eine echte Kopie des uebergebenen Array erstellt.
        /// </summary>
        /// <param name="array">Zahlenfolge</param>
        public DataSet(int[] array)
        {
            this.i = Config.NOT_USED;
            this.j = Config.NOT_USED;
            this.n = Config.NOT_USED;
            this.a = new int[array.Length];
            array.CopyTo(a, 0);
            this.min = Config.NOT_USED;
            this.pivot = Config.NOT_USED;
            this.left = Config.NOT_USED;
            this.right = Config.NOT_USED;
        }
        /// <summary>
        /// Konstruktor mit DataSet Objekt als Uebergabe. Dabei wird eine echte Kopie des Array a erstellt.
        /// </summary>
        /// <param name="dataSet">Zu kopierendes DataSet Objekt.</param>
        public DataSet(DataSet dataSet)
        {
            this.i = dataSet.I;
            this.j = dataSet.J;
            this.n = dataSet.N;
            a = new int[dataSet.A.Length];
            dataSet.A.CopyTo(a, 0);
            this.min = dataSet.Min;
            this.pivot = dataSet.Pivot;
            this.left = dataSet.Left;
            this.right = dataSet.Right;
        }
        #endregion
    }
}
