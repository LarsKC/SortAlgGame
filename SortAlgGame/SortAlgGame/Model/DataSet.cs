using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model
{
    class DataSet
    {
        //Variablen
        private int i;
        private int j;
        private int n;
        private int[] a;
        private int min;
        private int pivot;
        private int left;
        private int right;

        //Accessor
        public int I
        {
            get { return i; }
            set { i = value; }
        }

        public int J
        {
            get { return j; }
            set { j = value; }
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public int[] A
        {
            get { return a; }
            set { a = value; }
        }

        public int Min
        {
            get { return min; }
            set { n = value; }
        }

        public int Pivot
        {
            get { return pivot; }
            set { pivot = value; }
        }

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        //Konstruktoren
        public DataSet()
        {
            this.i = Config.NOTUSED;
            this.j = Config.NOTUSED;
            this.n = Config.NOTUSED;
            this.a = null;
            this.min = Config.NOTUSED;
            this.pivot = Config.NOTUSED;
            this.left = Config.NOTUSED;
            this.right = Config.NOTUSED;
        }
        public DataSet(int[] a)
        {
            this.i = Config.NOTUSED;
            this.j = Config.NOTUSED;
            this.n = Config.NOTUSED;
            this.a = a;
            this.min = Config.NOTUSED;
            this.pivot = Config.NOTUSED;
            this.left = Config.NOTUSED;
            this.right = Config.NOTUSED;
        }

        public DataSet(int i, int j, int n, int[] a, int min, int pivot, int left, int right)
        {
            this.i = i;
            this.j = j;
            this.n = n;
            this.a = a;
            this.min = min;
            this.pivot = pivot;
            this.left = left;
            this.right = right;
        }

        public DataSet(DataSet dataSet)
        {
            this.i = dataSet.I;
            this.j = dataSet.J;
            this.n = dataSet.N;
            this.a = dataSet.A;
            this.min = dataSet.Min;
            this.pivot = dataSet.Pivot;
            this.left = dataSet.Left;
            this.right = dataSet.Right;
        }
    }
}
