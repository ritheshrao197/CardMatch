using UnityEngine;
namespace MemoryGame
{
    /// <summary>
    /// Definition of a single level in the Memory Game
    /// </summary>
    public class LevelDef
    {
        [Header("Board")] public int rows = 2; public int cols = 2;
        [Header("Limits")] public int moveLimit = 0; // 0 = unlimited
        public float timeLimitSec = 0f;              // 0 = unlimited
        [Header("Overrides")] public float flipDuration = 0f; public float mismatchHideDelay = 0f;
    }
}