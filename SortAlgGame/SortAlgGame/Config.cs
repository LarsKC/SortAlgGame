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
        public const string OUT_OF_RANGE_ERROR = "Zugriff außerhalb des Arrays!";
        public const string NOT_INIT_ERROR = "Nicht initialisierte Variable!";
        public const string RUNTIME_NA = "nicht messbar";
        public const string MAX_RUNTIME_ERROR = "Laufzeit größer n²";
        public const string WAITING_Player = "WaitForPlayer";
        public const string WAITING_RESULT = "WaitForResult";
        public const string INFO_BUBBLE = "TestBubble";
        public const string INFO_INSERTION = "TestInsertion";
        public const string INFO_SELECTION = "TestSelection";
        public const string INFO_QUICK = "TestQuick";
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

        public static int MAX_RUNTIME(int n)
        {
            return n * n * 10;
        }
    }
}
