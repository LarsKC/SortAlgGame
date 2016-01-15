using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame
{
    static class Config
    {
        public const int NOT_USED = -100;
        public const string INDENT = "  ";
        public static readonly int[] RUNS = { 10, 100, 500, 1000};
        public const int ANIMATION_TIMER = 500;
        public const string TEXT_RED = "Red", TEXT_NORMAL = "White";
        public const int RECT_MULTIPLIKATOR = 30;
        public const string OUT_OF_RANGE_ERROR = "Zugriff außerhalb des Array-Index-Bereichs!";
        public const string NOT_INIT_ERROR = "Eine Variable wurde nicht Initialisiert!";
        public const string RUNTIME_NA = "nicht messbar";
        public const string MAX_RUNTIME_ERROR = "Laufzeit größer n²";
        public const string WAITING_Player = "WaitForPlayer";
        public const string WAITING_RESULT = "WaitForResult";
        public const string INFO_BUBBLE = "TestBubble";
        public const string INFO_INSERTION = "TestInsertion";
        public const string INFO_SELECTION = "TestSelection";
        public const string INFO_QUICK = "TestQuick";

        public static int MAX_RUNTIME(int n)
        {
            return n * n;
        }
    }
}
