namespace MemoryGame.Constants
{
    /// <summary>
    /// Constants for game configuration values in the memory game.
    /// Centralizes default values for game configuration settings.
    /// </summary>
    public static class GameConfigConstants
    {
        /// <summary>
        /// Default number of rows in the game board
        /// </summary>
        public const int DefaultRows = 3;
        
        /// <summary>
        /// Default number of columns in the game board
        /// </summary>
        public const int DefaultCols = 4;
        
        /// <summary>
        /// Default duration for card flip animations in seconds
        /// </summary>
        public const float DefaultFlipDuration = 0.25f;
        
        /// <summary>
        /// Default delay before hiding mismatched cards in seconds
        /// </summary>
        public const float DefaultMismatchHideDelay = 0.7f;
        
        /// <summary>
        /// Default horizontal spacing between cards in world units
        /// </summary>
        public const float DefaultCellX = 1.3f;
        
        /// <summary>
        /// Default vertical spacing between cards in world units
        /// </summary>
        public const float DefaultCellY = 1.6f;
    }
}