
using System;
using MemoryGame.Controller;
namespace MemoryGame.Events
{
    /// <summary>
    /// General game events for the Memory Game
    /// </summary>
    public static class GameEvents
    {
        // Card & pair
        public static event Action<CardController> OnCardSelected;
        public static event Action<CardController, CardController> OnPairMatched;
        public static event Action<CardController, CardController> OnPairMismatched;
        public static event Action<int> OnRemainingPairsChanged;

        // Level flow
        public static event Action<LevelDef, int> OnLevelStarted; // (levelDef, index)
        public static event Action<int> OnLevelCompleted;         // level index
        public static event Action<string> OnGameLost;            // reason string ("moves" or "time")
        public static event Action OnGameWon;                     // all pairs matched for current level

        public static void RaiseCardSelected(CardController c) => OnCardSelected?.Invoke(c);
        public static void RaisePairMatched(CardController a, CardController b) => OnPairMatched?.Invoke(a, b);
        public static void RaisePairMismatched(CardController a, CardController b) => OnPairMismatched?.Invoke(a, b);
        public static void RaiseRemainingPairsChanged(int v) => OnRemainingPairsChanged?.Invoke(v);

        public static void RaiseLevelStarted(LevelDef def, int idx) => OnLevelStarted?.Invoke(def, idx);
        public static void RaiseLevelCompleted(int idx) => OnLevelCompleted?.Invoke(idx);
        public static void RaiseGameLost(string reason) => OnGameLost?.Invoke(reason);
        public static void RaiseGameWon() => OnGameWon?.Invoke();
    }

}