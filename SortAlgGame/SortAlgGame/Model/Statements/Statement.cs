using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Module
{
    abstract class Statement
    {
        protected string header;
        public string Header
        {
            get { return header; }
        }

        protected PlayerProgramm programm;
        public Statement(PlayerProgramm programm)
        {
            this.programm = programm;
        }

        public abstract void execute();

        protected void swap(int i, int j)
        {
            DataSet dataSet = programm.Stack.Peek();

            int tmp = dataSet.A[i];
            dataSet.A[i] = dataSet.A[j];
            dataSet.A[j] = tmp;
        }
    }
}
