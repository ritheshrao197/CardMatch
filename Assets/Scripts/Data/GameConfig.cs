using UnityEngine;
using MemoryGame.Constants;

namespace MemoryGame.Config
{
    /// <summary>
    /// Configuration data for the Memory Game
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "MemoryGame/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Board Size")]
        public int rows = GameConfigConstants.DefaultRows;
        public int cols = GameConfigConstants.DefaultCols;

        [Header("Animation & Rules")]
        [Tooltip("Flip animation duration in seconds")]
        public float flipDuration = GameConfigConstants.DefaultFlipDuration;
        [Tooltip("Delay before hiding mismatched pair")]
        public float mismatchHideDelay = GameConfigConstants.DefaultMismatchHideDelay;

        [Header("Layout")]
        [Tooltip("Spacing between columns (world units)")]
        public float cellX = GameConfigConstants.DefaultCellX;
        [Tooltip("Spacing between rows (world units)")]
        public float cellY = GameConfigConstants.DefaultCellY;
    }
}