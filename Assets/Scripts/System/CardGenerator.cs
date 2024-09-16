using System;
using System.Collections.Generic;
using MatchGame.Core;
using MatchGame.Data;
using MatchGame.Utils;
using UnityEngine;

namespace MatchGame
{

    public class CardGenerator : CoreSystem
    {

        EventHandlerSystem _eventHandlerSystem;
        GameDataContainer _gameDataContainer;
        public override void Init()
        {
            base.Init();
            _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
        }
        public override void AddListener()
        {
            base.AddListener();
            _eventHandlerSystem.AddListener(GameEventKeys.CurrentLevelUpdated, GetGridSize);
            _eventHandlerSystem.AddListener(GameEventKeys.GridSizeUpdated, GetIndexNumbers);

        }
        public override void RemoveListener()
        {
            base.RemoveListener();
            _eventHandlerSystem.RemoveListener(GameEventKeys.CurrentLevelUpdated, GetGridSize);
            _eventHandlerSystem.RemoveListener(GameEventKeys.GridSizeUpdated, GetIndexNumbers);

        }
        public override void Finalise()
        {
            base.Finalise();

        }
        public void GetGridSize()
        {
            _gameDataContainer.GridSize = Constants.GridSizes[_gameDataContainer.CurrentLevel];
            //Debug.Log("GridSize "+ _gameDataContainer.GridSize.rows+"-"+_gameDataContainer.GridSize.columns);


        }
        public void GetIndexNumbers()
        {
            //Debug.Log("Index Numbers");

            _gameDataContainer.IndexNumbers = GenerateRandomNumbers((_gameDataContainer.GridSize.rows * _gameDataContainer.GridSize.columns) / 2);
            //foreach(int i in _gameDataContainer.IndexNumbers)
            //{
            //    Debug.Log(i);
            //}

        }


        public List<int> GenerateRandomNumbers(int n, int min =0, int max=99)
        {
            if (n > max - min + 1)
            {
                throw new ArgumentException("Cannot generate more unique numbers than the range allows.");
            }

            List<int> result = new List<int>();
            List<int> availableNumbers = new List<int>();

            // Fill the list of available numbers with the range [min, max]
            for (int i = min; i <= max; i++)
            {
                availableNumbers.Add(i);
            }

            System.Random rand = new System.Random();

            for (int i = 0; i < n; i++)
            {
                int index = rand.Next(0, availableNumbers.Count);
                int randomNumber = availableNumbers[index];
                availableNumbers.RemoveAt(index);
                result.Add(randomNumber);
            }

            return result;
        }

    }
}