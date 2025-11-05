
using UnityEngine;
namespace MemoryGame.Config
{
    /// <summary>
    /// Configuration data for the Memory Game
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "MemoryGame/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Board Size")]
        public int rows = 3;
        public int cols = 4;

        [Header("Animation & Rules")]
        [Tooltip("Flip animation duration in seconds")]
        public float flipDuration = 0.25f;
        [Tooltip("Delay before hiding mismatched pair")]
        public float mismatchHideDelay = 0.7f;

        [Header("Layout")]
        [Tooltip("Spacing between columns (world units)")]
        public float cellX = 1.3f;
        [Tooltip("Spacing between rows (world units)")]
        public float cellY = 1.6f;
    }
}