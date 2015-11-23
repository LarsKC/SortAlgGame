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
            set { this.i = value; }
        }

        private int j;
        public int J
        {
            get { return j; }
            set { this.j = value; }
        }

        public int n; 
        public int N
        {
            get { return n; }
            set { this.n = value; }
        }

        private int[] a; 
        public int[] A
        {
            get { return a; }
            set { this.a = (int[])value.Clone(); } 
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

        //Konstruktor
        public DataSet(int i, int j, int n, int[] a, int min, int pivot)
        {
            this.i = i;
            this.j = j;
            this.n = n;
            this.a = a;
            this.min = min;
            this.pivot = pivot;
        }

        public DataSet(DataSet dataSet)
        {
            this.i = dataSet.I;
            this.j = dataSet.J;
            this.n = dataSet.N;
            this.a = dataSet.A;
            this.min = dataSet.Min;
            this.pivot = dataSet.Pivot;
        }
  
    }
}
