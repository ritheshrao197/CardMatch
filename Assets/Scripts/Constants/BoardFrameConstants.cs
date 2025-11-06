namespace MemoryGame.Constants
{
    /// <summary>
    /// Constants for board frame values in the memory game.
    /// Centralizes numerical constants for board frame configuration and layout.
    /// </summary>
    public static class BoardFrameConstants
    {
        /// <summary>
        /// Default inner padding as a fraction of frame size
        /// </summary>
        public const float DefaultInnerPadding = 0.05f;
        
        /// <summary>
        /// Default card padding as a fraction of frame size
        /// </summary>
        public const float DefaultCardPadding = 0.1f;
        
        /// <summary>
        /// Minimum padding value to prevent issues with card layout
        /// </summary>
        public const float MinPadding = 0f;
        
        /// <summary>
        /// Maximum padding value to prevent issues with card layout
        /// </summary>
        public const float MaxPadding = 0.5f;
    }
}