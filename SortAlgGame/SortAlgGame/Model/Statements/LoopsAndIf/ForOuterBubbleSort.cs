using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.Model.Module.LoopsAndIf
{
    class ForOuterBubbleSort : loopBased
    {
        public ForOuterBubbleSort(PlayerProgramm programm)
            : base(programm)
        {
            header = "for( i=a.size; i>1; i--) {";
        }

        public override void execute()
        {
            DataSet actDataSet = programm.Stack.Peek();
            int n = actDataSet.N;
            int[] a = actDataSet.A;

            for (n = a.Length; n > 1; n--)
            {
                //DataSet Aktualisieren
                actDataSet.N = n;
                // TODO: Log Eintrag
                innerStatement.execute();
            }
        }

         
    }
}
