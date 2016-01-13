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
        public const string TEXT_BOLD = "Bold", TEXT_NORMAL = "Normal";
        public const int RECT_MULTIPLIKATOR = 15;
        public const string OUT_OF_RANGE_ERROR = "Zugriff außerhalb des Array-Index-Bereichs!";
        public const string NOT_INIT_ERROR = "Eine Variable wurde nicht Initialisiert!";
        public const string RUNTIME_NA = "nicht messbar";
        public const string MAX_RUNTIME_ERROR = "Laufzeit größer n²";

        public static int MAX_RUNTIME(int n)
        {
            return n * n;
        }
    }
}
