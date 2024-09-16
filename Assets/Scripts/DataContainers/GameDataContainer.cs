using System.Collections.Generic;
using MatchGame.Core;
using UnityEngine;

namespace MatchGame.Data
{
    public enum State
    {
        MainMenu,
        LevelSelection,
        Play,
        Pause,
        GameOver,
        none
    }

    public class GameDataContainer
    {
        private State _gameState =State.MainMenu;
        public State GameState
        {
            get
            {
                return _gameState;

            }
            set
            {
                _gameState = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.GameStateUpdated);


            }
        }
        private int _currentLevel = 1;
        public int CurrentLevel
        {
            get
            {
                return _currentLevel;

            }
            set
            {
                _currentLevel = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.CurrentLevelUpdated);
                _matchedCardIndexes.Clear();


            }
        }
        public List<int> _indexNumbers;
        public List<int> IndexNumbers
        {
            get
            {
                return _indexNumbers;

            }
            set
            {
                _indexNumbers = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.IndexNumbersUpdated);

            }
        }
        public List<int> _matchedCardIndexes = new List<int>();
        public List<int> MatchedCardIndexes
        {
            get
            {
                return _matchedCardIndexes;

            }
            set
            {
                _matchedCardIndexes = value;

            }
        }
        public Grid _gridSize;
        public Grid GridSize
        {
            get
            {
                return _gridSize;

            }
            set
            {
                _gridSize = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.GridSizeUpdated);

            }
        }

        public Card _displayedCard= new Card(-1,-1);
        public Card DisplayedCard
        {
            get
            {
                return _displayedCard;

            }
            set
            {
                if (value != null)
                {
                    _displayedCard = value;
                    //if(value.id==-1)
                    //{
                    //    ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.FlipCards);

                    //}
                    //else
                    ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.CardDisplayed);
                }

            }
        }
        public Dictionary<int, int> _currentCard = new Dictionary<int, int>();
        public Dictionary<int, int> CurrentCard
        {
            get
            {
                return _currentCard;

            }
            set
            {
                _currentCard = value;
                //if (value != -1)
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.FlipCards);

            }
        }
        public bool _cardMatched = false;
        public bool CardMatched
        {
            get
            {
                return _cardMatched;

            }
            set
            {
                _cardMatched = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.CardMatched);

            }
        }
        public Card _flipCards;
        public Card FlipCards
        {
            get
            {
                return _flipCards;

            }
            set
            {
                _flipCards = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.FlipCards);

            }
        }
        public int _score=0;
        public int Score
        {
            get
            {
                return _score;

            }
            set
            {
                
                _score = value;
                if(value==0)
                {
                    _matchedCardIndexes.Clear();
                }
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.ScoreUpdated);

            }
        }

        public Sprite[] Sprites { get; internal set; }
        public bool UpdateTimer
        {

            set
            {
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.UpdateTimer);

            }
        }
        public float _currentTime;
        public float CurrentTime
        {
            get
            {
                return _currentTime;

            }
            set
            {
                _currentTime = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.TimerUpdated);

            }
        }
        public int _maxScore;
        public int MaxScore
        {
            get
            {
                return _maxScore;

            }
            set
            {
                _maxScore = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(GameEventKeys.ScoreUpdated);


            }
        }
        public int _unlockedLevel;

        public int UnlockedLevel
        {
            get
            {
                _unlockedLevel = PlayerPrefs.GetInt("_unlockedLevel", 1);
                return _unlockedLevel;

            }
            set
            {
                if(value>_unlockedLevel)
                PlayerPrefs.SetInt("_unlockedLevel" , value);


            }
        }

    }

}