using System;
using System.Collections;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;

namespace MatchGame
{
    public class ScoringSystem : CoreSystem
    {

        EventHandlerSystem _eventHandlerSystem;
        GameDataContainer _gameDataContainer;
        TimerSystem _timerSystem;


        public override void Init()
        {
            base.Init();
            _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            _timerSystem = ServiceLocator.Instance.Get<TimerSystem>();
        }
        public override void AddListener()
        {
            base.AddListener();
            _eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, UpdateGameState);
            _eventHandlerSystem.AddListener(GameEventKeys.CardMatched, UpdateScore);
            _eventHandlerSystem.AddListener(GameEventKeys.GridSizeUpdated, SetMaxScoreForLevel);


        }

        public override void RemoveListener()
        {
            base.RemoveListener();
            _eventHandlerSystem.RemoveListener(GameEventKeys.CardMatched, UpdateScore);
            _eventHandlerSystem.RemoveListener(GameEventKeys.GameStateUpdated, UpdateGameState);
            _eventHandlerSystem.RemoveListener(GameEventKeys.GridSizeUpdated, SetMaxScoreForLevel);



        }

        private void SetMaxScoreForLevel()
        {
            _gameDataContainer.MaxScore = (_gameDataContainer.GridSize.columns * _gameDataContainer.GridSize.rows) / 2;
        }

        private void UpdateGameState()
        {
            if (_gameDataContainer.GameState == State.Play)
            {
                _timerSystem.Start();
            }
            else if (_gameDataContainer.GameState == State.GameOver)
            {
                _timerSystem.Stop();
                _gameDataContainer.Score = 0;
            }
        }

        public override void Finalise()
        {
            base.Finalise();

        }


        public void UpdateScore()
        {
            _gameDataContainer.Score = _gameDataContainer.MatchedCardIndexes.Count;
            if (_gameDataContainer.MaxScore == _gameDataContainer.Score)
            {
                CoreContext.Instance.StartCoroutine(LevelComplete());

            }
        }

        private IEnumerator LevelComplete()
        {
            yield return new WaitForSeconds(0.5f);

            _gameDataContainer.GameState = State.GameOver;
            _gameDataContainer.UnlockedLevel = _gameDataContainer.CurrentLevel + 1;
        }

        public void SubtractPoints(int points)
        {
            _gameDataContainer.Score -= 1;
        }


    }
}