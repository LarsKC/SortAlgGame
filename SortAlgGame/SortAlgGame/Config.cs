using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame
{
    /// <summary>
    /// Konfigurationsdatei
    /// </summary>
    static class Config
    {
        /// <summary>
        /// Belegung einer Variable, die nicht verwendet wird.
        /// </summary>
        public const int NOT_USED = -100;
        /// <summary>
        /// Breite der Einrueckung des Pseudocodes
        /// </summary>
        public const string INDENT = "  ";
        /// <summary>
        /// Laenge der zu erstellenden Testzahlfenfolgen bei der Auswertung eines Algorithmus.
        /// </summary>
        public static readonly int[] RUNS = { 10, 100, 500, 1000};
        /// <summary>
        /// Aktualisierungs Interval des Animation Timers in ms
        /// </summary>
        public const int ANIMATION_TIMER = 500;
        /// <summary>
        /// Begrenzung der Spielzeit Anzeige in Sekunden
        /// </summary>
        public const int MAX_GAME_TIME = 3659;
        /// <summary>
        /// Standard Schriftfarbe der Pseudocode Bausteine
        /// </summary>
        public const string TEXT_NORMAL = "White";
        /// <summary>
        /// Schriftfarbe eines selektierten Pseudocode Bausteins
        /// </summary>
        public const string TEXT_RED = "Red";
        /// <summary>
        /// Multiplikator der Rechteckhoehe im Balkendiagramm
        /// </summary>
        public const int RECT_MULTIPLIKATOR = 30;
        /// <summary>
        /// Fehlermeldung bei einem Zugriff außerhalb des Arrays.
        /// </summary>
        public const string OUT_OF_RANGE_ERROR = "Zugriff außerhalb des Arrays!";
        /// <summary>
        /// Fehlermeldung beim Zugriff auf eine nicht initialisierte Variable.
        /// </summary>
        public const string NOT_INIT_ERROR = "Nicht initialisierte Variable!";
        /// <summary>
        /// Platzhalter, wenn die Laufzeit nicht bestimmt werden konnte.
        /// </summary>
        public const string RUNTIME_NA = "nicht messbar";
        /// <summary>
        /// Fehlermeldung, wenn die maximal moegliche Laufzeit ueberschritten wurde.
        /// </summary>
        public const string MAX_RUNTIME_ERROR = "Laufzeit größer n²!";
        /// <summary>
        /// Konstante zum Bestimmen des Wartetextes im Spiel, wenn auf den anderen Spieler gewartet werden muss.
        /// </summary>
        public const string WAITING_Player = "WaitForPlayer";
        /// <summary>
        /// Konstante zum Bestimmen des Wartetextes im Spiel, wenn auf die Auswertung gewartet werden muss.
        /// </summary>
        public const string WAITING_RESULT = "WaitForResult";
        /// <summary>
        /// Informationstext des Algorithmus Bubblesort.
        /// </summary>
        public const string INFO_BUBBLE = "Bubblesort ist ein Sortieralgorithmus der meist nur in der Lehre verwendet wird. Er ist besonders einfach zu implementieren, ist im Vergleich zu anderen Algorithmen allerdings relative langsam beim Sortieren. Bubblesort ist ein vergleichsbasierender Algorithmus. Die Laufzeit des Algorithmus liegt im schlechtesten Fall in O(n²) und im besten Fall in O(n). Die Laufzeit O(n) wird nur dann erreicht, wenn der Algorithmus auf eine bereits sortierte Folge von Elementen angewandt wird. Die durchschnittliche Laufzeit von Bubblesort liegt ebenfalls in O(n²). Äquivalente Objekte behalten beim Sortieren ihre relative Reihenfolge bei.";
        /// <summary>
        /// Informationstextdes Algorithmus Insertionsort
        /// </summary>
        public const string INFO_INSERTION = "Insertionsort ist ein auf Einfügen barsierter Sortieralgorithmus. Die ideale Laufzeit von O(n) wird bei Anwendung auf eine bereits sortierte Folge von Elementen erreicht. Der schlechteste und durchschnittliche Fall liegen beide in O(n²). Äquivalente Objekte behalten beim Sortieren ihre relative Reihenfolge bei.";
        /// <summary>
        /// Informationstext des Algorithmus Selectionsort
        /// </summary>
        public const string INFO_SELECTION = "Selectionsort teilt die zu sortierende Folge von Elementen in zwei Bereiche, der sortierte Bereich und der unsortierte Bereich. Im sortierten Bereich befindet sich zu Beginn der Sortierung nur ein Element. In Folge des Sortiervorgangs schrumpft der unsortierte Bereich, wohingegen der sortierte Bereich anwächst. Betrachtet man die Laufzeit des Algorithmus, stellt man im durchschnittlichen, schlechtesten und besten Fall fest, dass die Laufzeit in O(n²) liegt. Äquivalente Objekte behalten beim Sortieren nicht ihre relative Reihenfolge.";
        /// <summary>
        /// Informationstext des Algorithmus Quicksort
        /// </summary>
        public const string INFO_QUICK = "Quicksort basiert auf dem Sortierprinzip teile und hersche (devide and conquer). Das bedeutet, dass ein großes Gesamtproblem in immer kleiner werdende Teilprobleme zerlegt wird. Quicksort ist ein relativ schneller Sortieralgorithmus. Im besten und durchschnittlichen Fall liegt die Laufzeit in O(n*log(n)). Nur im schlechtesten Fall liegt die Laufzeit in O(n²). Die Effektivität dieses Algorithmus hängt stark von der Wahl des Pivot Elements ab. Beim Sortieren kann nicht garantiert werden, das äquivalente Objekte ihre relative Reihenfolge beibehalten.";
        /// <summary>
        /// Enumerator zum Bestimmen der Bausteinart.
        /// </summary>
        public enum EXECUTE 
        { 
            FOR_IN_BUBBLE, 
            FOR_OUT_BUBBLE, 
            FOR_INSERTION, 
            FOR_IN_SELECTION, 
            WHILE_AI_LESS_PIVOT, 
            WHILE_AJ_GREATER_PIVOT, 
            WHILE_I_LESS_EQUALS_J, 
            WHILE_J_GREATER_NULL, 
            IF_AI_GREATER_AI_INC, 
            IF_I_LESS_EQUALS_J, 
            IF_I_LESS_RIGHT, 
            IF_I_NOTEQUALS_MIN, 
            IF_LEFT_LESS_J, 
            IF_AJ_LESS_AMIN, 
            BASE_STM, 
            ALLOC_LEFT, 
            ALLOC_PIVOT, 
            ALLOC_RIGHT, 
            ALLOC_ALENGHT, 
            ALLOC_I_TO_J, 
            ALLOC_I_TO_MIN, 
            ALLOC_J_TO_MIN, 
            DEC_J, 
            INC_I, 
            CALL_SORT_LEFT, 
            CALL_SORT_RIGHT, 
            SWAP_AI_WITH_AJ, 
            SWAP_AJ_WITH_AJ_DEC, 
            SWAP_AMIN_WITH_AI, 
            SWAP_AI_WITH_AI_INC,
            LOOP_END
        };
        /// <summary>
        /// Methode zum Bestimmen der maximalen Laufzeit.
        /// </summary>
        /// <param name="n">Anzahl der zu sortierenden Elemente</param>
        /// <returns>Maximale Laufzeit</returns>
        public static int MAX_RUNTIME(int n)
        {
            return n * n * 10;
        }
    }
}
