using MemoryGame.Controller;
using MemoryGame.Events;
using UnityEngine;
namespace MemoryGame
{
    public class LevelRules : MonoBehaviour
    {
        private int _moves;
        private int _moveLimit;
        private float _timeLimit;
        private bool _ended;
        private TimerService _timer;

        private void OnEnable()
        {
            GameEvents.OnPairMatched += OnPairEvent;
            GameEvents.OnPairMismatched += OnPairEvent;
            GameEvents.OnGameWon += OnGameWon;
        }

        private void OnDisable()
        {
            GameEvents.OnPairMatched -= OnPairEvent;
            GameEvents.OnPairMismatched -= OnPairEvent;
            GameEvents.OnGameWon -= OnGameWon;
        }

        public void BeginLevel(LevelDef def, int index, TimerService timer)
        {
            _moves = 0;
            _moveLimit = def.moveLimit;
            _timeLimit = def.timeLimitSec;
            _ended = false;
            _timer = timer;
        }

        private void Update()
        {
            if (_ended) return;
            if (_timeLimit > 0f && _timer != null && _timer.elapsed >= _timeLimit)
            {
                _ended = true;
                _timer.StopTimer();
                GameEvents.RaiseGameLost("time");
            }
        }

        private void OnPairEvent(CardController a, CardController b)
        {
            if (_ended) return;
            _moves++;
            if (_moveLimit > 0 && _moves >= _moveLimit)
            {
                // If limit reached before GameWon fired, it's a loss
                if (!a.Model.IsMatched || !b.Model.IsMatched)
                {
                    _ended = true;
                    _timer?.StopTimer();
                    GameEvents.RaiseGameLost("moves");
                }
            }
        }

        private void OnGameWon()
        {
            if (_ended) return;
            _ended = true;
        }
    }
}