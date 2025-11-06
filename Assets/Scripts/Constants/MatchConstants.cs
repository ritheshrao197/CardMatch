namespace MemoryGame.Constants
{
    /// <summary>
    /// Constants for match-related values in the memory game.
    /// Centralizes numerical constants for matching logic and game flow.
    /// </summary>
    public static class MatchConstants
    {
        /// <summary>
        /// Default delay before hiding mismatched cards when no config is available
        /// </summary>
        public const float DefaultMismatchHideDelay = 0.7f;
        
        /// <summary>
        /// Tiny buffer delay in seconds for smoother gameplay
        /// </summary>
        public const float ResolveBufferDelay = 0.05f;
    }
}