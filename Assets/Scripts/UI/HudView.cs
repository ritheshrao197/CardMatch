using MemoryGame.Controller;
using MemoryGame.Events;
using TMPro;
using UnityEngine;
namespace MemoryGame.Views
{  /// <summary>
   /// View component for the HUD in the Memory Game
   /// </summary>  
    public class HudView : MonoBehaviour
    {
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI movesText;
        public TextMeshProUGUI statusText;

        private int _moves;
        private int _moveLimit;
        private float _timeLimit;
        private TimerService _timer;

        private void Awake()
        {
            _timer = FindObjectOfType<TimerService>();
            GameEvents.OnLevelStarted += OnLevelStarted;
            GameEvents.OnPairMatched += OnPairEvent;
            GameEvents.OnPairMismatched += OnPairEvent;
            GameEvents.OnRemainingPairsChanged += OnRemainingPairsChanged;
            GameEvents.OnGameWon += OnGameWon;
            GameEvents.OnGameLost += OnGameLost;
        }
        private void OnDestroy()
        {
            GameEvents.OnLevelStarted -= OnLevelStarted;
            GameEvents.OnPairMatched -= OnPairEvent;
            GameEvents.OnPairMismatched -= OnPairEvent;
            GameEvents.OnRemainingPairsChanged -= OnRemainingPairsChanged;
            GameEvents.OnGameWon -= OnGameWon;
            GameEvents.OnGameLost -= OnGameLost;
        }

        private void Update()
        {
            if (_timer != null && timerText != null)
            {
                var t = Mathf.Max(0f, _timer.elapsed);
                int m = (int)(t / 60f);
                int s = (int)(t % 60f);
                timerText.text = _timeLimit > 0f ? $"{m:00}:{s:00} / {FormatTime(_timeLimit)}" : $"{m:00}:{s:00}";
            }
        }

        private string FormatTime(float seconds)
        {
            int m = (int)(seconds / 60f);
            int s = (int)(seconds % 60f);
            return $"{m:00}:{s:00}";
        }

        private void OnLevelStarted(LevelDef def, int idx)
        {
            _moves = 0;
            _moveLimit = def.moveLimit;
            _timeLimit = def.timeLimitSec;
            if (levelText) levelText.text = $"Level {idx + 1}: {def.rows}x{def.cols}";
            if (movesText) movesText.text = _moveLimit > 0 ? $"0 / {_moveLimit}" : "0";
            if (statusText) statusText.text = "Find all pairs";
        }

        private void OnPairEvent(CardController a, CardController b)
        {
            _moves++;
            if (movesText) movesText.text = _moveLimit > 0 ? $"{_moves} / {_moveLimit}" : _moves.ToString();
        }

        private void OnRemainingPairsChanged(int v) { if (statusText) statusText.text = $"Pairs left: {v}"; }
        private void OnGameWon() { if (statusText) statusText.text = "Level complete!"; }
        private void OnGameLost(string reason) { if (statusText) statusText.text = reason == "time" ? "Time up" : "Move limit reached"; }
    }
}