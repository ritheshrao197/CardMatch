using System.Collections;
using System.Collections.Generic;
using MemoryGame.Config;
using MemoryGame.Controller;
using MemoryGame.Events;
using UnityEngine;
namespace MemoryGame.Services
{
    /// <summary>
    /// Service to handle matching logic between selected cards
    /// </summary>
    public class MatchService : MonoBehaviour
    {
        [Header("Refs")]
        public GameConfig config;

        private CardController _first, _second;
        private bool _busy;
        private int _remainingPairs;
        private readonly List<CardController> _allCards = new List<CardController>();

        private void OnEnable()
        {
            GameEvents.OnCardSelected += OnCardSelected;
            GameEvents.OnRemainingPairsChanged += OnRemainingPairsChanged;
        }

        private void OnDisable()
        {
            GameEvents.OnCardSelected -= OnCardSelected;
            GameEvents.OnRemainingPairsChanged -= OnRemainingPairsChanged;
        }

        public void RegisterCards(IEnumerable<CardController> cards)
        {
            _allCards.Clear();
            _allCards.AddRange(cards);
        }

        private void OnRemainingPairsChanged(int v)
        {
            _remainingPairs = v;
        }

        private void SetAllInput(bool enabled)
        {
            foreach (var c in _allCards) c.SetInput(enabled);
        }

        private void OnCardSelected(CardController c)
        {
            if (_busy) return;
            if (_first == null)
            {
                _first = c; return;
            }
            if (c == _first) return;

            _second = c;
            StartCoroutine(Resolve());
        }

        private IEnumerator Resolve()
        {
            _busy = true;
            SetAllInput(false);

            yield return new WaitForSeconds(0.05f); // tiny buffer

            if (_first.Model.Id == _second.Model.Id)
            {
                _first.Lock();
                _second.Lock();
                GameEvents.RaisePairMatched(_first, _second);
                _remainingPairs--;
                GameEvents.RaiseRemainingPairsChanged(_remainingPairs);

                if (_remainingPairs <= 0)
                {
                    GameEvents.RaiseGameWon();
                }
            }
            else
            {
                GameEvents.RaisePairMismatched(_first, _second);
                yield return new WaitForSeconds(Mathf.Max(0f, config != null ? config.mismatchHideDelay : 0.7f));
                // Flip back
                yield return _first.StartCoroutine(_first.FlipRoutine(false));
                yield return _second.StartCoroutine(_second.FlipRoutine(false));
            }

            _first = _second = null;
            SetAllInput(true);
            _busy = false;
        }
    }
}