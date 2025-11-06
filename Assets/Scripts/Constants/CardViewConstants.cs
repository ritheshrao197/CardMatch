namespace MemoryGame.Constants
{
    /// <summary>
    /// Constants for card view-related values in the memory game.
    /// Centralizes numerical constants for card animations and visual representation.
    /// </summary>
    public static class CardViewConstants
    {
        /// <summary>
        /// Minimum scale value to prevent cards from disappearing during flip animation
        /// </summary>
        public const float MinFlipScale = 0.0001f;
        
        /// <summary>
        /// Half of the minimum scale value for flip animation calculations
        /// </summary>
        public const float HalfMinFlipScale = 0.0001f * 0.5f;
    }
}