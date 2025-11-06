using System.Collections;
using System.Collections.Generic;
using MemoryGame.Config;
using MemoryGame.Controller;
using MemoryGame.Events;
using UnityEngine;
using MemoryGame.Constants;

namespace MemoryGame.Services
{
    /// <summary>
    /// Service to handle matching logic between selected cards in the memory game.
    /// Manages card selection, determines matches, and handles game progression.
    /// </summary>
    public class MatchService : MonoBehaviour
    {
        /// <summary>
        /// Reference to the game configuration settings
        /// </summary>
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

        /// <summary>
        /// Registers all cards in the current game for matching logic
        /// </summary>
        /// <param name="cards">Collection of card controllers to register</param>
        public void RegisterCards(IEnumerable<CardController> cards)
        {
            _allCards.Clear();
            _allCards.AddRange(cards);
        }

        private void OnRemainingPairsChanged(int v)
        {
            _remainingPairs = v;
        }

        /// <summary>
        /// Sets input enabled/disabled state for all registered cards
        /// </summary>
        /// <param name="enabled">True to enable input, false to disable</param>
        private void SetAllInput(bool enabled)
        {
            foreach (var c in _allCards) c.SetInput(enabled);
        }

        /// <summary>
        /// Handles card selection events from the game events system
        /// </summary>
        /// <param name="c">The card controller that was selected</param>
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

        /// <summary>
        /// Coroutine that resolves the matching logic between two selected cards
        /// </summary>
        /// <returns>IEnumerator for coroutine execution</returns>
        private IEnumerator Resolve()
        {
            _busy = true;
            SetAllInput(false);

            yield return new WaitForSeconds(MatchConstants.ResolveBufferDelay); // tiny buffer

            // Check if the two selected cards match
            if (_first.Model.Id == _second.Model.Id)
            {
                // Cards match - lock them and notify listeners
                _first.Lock();
                _second.Lock();
                GameEvents.RaisePairMatched(_first, _second);
                _remainingPairs--;
                GameEvents.RaiseRemainingPairsChanged(_remainingPairs);

                // Check if all pairs have been matched (game won)
                if (_remainingPairs <= 0)
                {
                    GameEvents.RaiseGameWon();
                }
            }
            else
            {
                // Cards don't match - notify listeners and flip them back
                GameEvents.RaisePairMismatched(_first, _second);
                yield return new WaitForSeconds(Mathf.Max(0f, config != null ? config.mismatchHideDelay : MatchConstants.DefaultMismatchHideDelay));
                // Flip back
                yield return _first.StartCoroutine(_first.FlipRoutine(false));
                yield return _second.StartCoroutine(_second.FlipRoutine(false));
            }

            // Reset selection and re-enable input
            _first = _second = null;
            SetAllInput(true);
            _busy = false;
        }
    }
}