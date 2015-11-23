using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model
{
    class DataSet
    {
        //Variablen mit Get und Set Accessor
        private int i;
        public int I
        {
            get { return i; }
            set { i = value; }
        }

        private int j;
        public int J
        {
            get { return j; }
            set { j = value; }
        }

        private int n; 
        public int N
        {
            get { return n; }
            set { n = value; }
        }

        private int[] a; 
        public int[] A
        {
            get { return a; }
            set { a = value; } 
        }

        private int min;
        public int Min
        {
            get { return min; }
            set { n = value; }
        }

        private int pivot;
        public int Pivot
        {
            get { return pivot; }
            set { pivot = value; }
        }

        private int left;
        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        private int right;
        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        //Konstruktor
        public DataSet(int[] a)
        {
            this.i = 0;
            this.j = 0;
            this.n = 0;
            this.a = a;
            this.min = 0;
            this.pivot = 0;
            this.left = 0;
            this.right = 0;
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
