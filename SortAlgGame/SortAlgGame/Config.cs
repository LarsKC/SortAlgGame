using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame
{
    static class Config
    {
        public const int NOTUSED = -100;
        public const string INDENT = "  ";
        public const int MIN = 0, MAX = 10000;
        public static readonly int[] RUNS = { 10, 100, 1000, 100000 };
        public const int ANIMATIONTIMER = 500;
        public const string textBold = "Bold", textNormal = "Normal";
        public const int RECTMULTIPLIKATOR = 15;
        public const string OUTOFRANGEERROR = "Zugriff außerhalb des Array-Index-Bereichs!";
        public const string NOTINITERROR = "Eine Variable wurde nicht Initialisiert!";
    }
}
