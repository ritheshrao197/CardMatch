using System.Collections.Generic;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.UI;
namespace MatchGame.View
{
    public class CardGeneratorView : View
    {
        //public GridLayoutGroup _gridLayout;
        public GameObject _card; // Reference to the prefab you want to instantiate.


        GameDataContainer _gameDataContainer;
        public override void Init()
        {
            base.Init();
            //_gridLayout = _gridLayout.GetComponent<GridLayoutGroup>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            eventHandlerSystem.AddListener(GameEventKeys.IndexNumbersUpdated, GenerateCards);
        }
        public override void Finalise()
        {
            base.Finalise();
            eventHandlerSystem.RemoveListener(GameEventKeys.IndexNumbersUpdated, GenerateCards);
        }
        public void GenerateCards()
        {

            List<int> indexs = _gameDataContainer.IndexNumbers;
            indexs.AddRange(_gameDataContainer.IndexNumbers);
            ShuffleList(indexs);
            DeleteChildren(gameObject.transform);
            for (int i=0;i< indexs.Count;i++)
            {
                // Create an instance of the prefab at a random position.
                GameObject cardObject = Instantiate(_card,gameObject.transform);
              CardView card= cardObject.GetComponent<CardView>();
                card.SetIndex(i, indexs[i]);
            }
        }
        void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < n; i++)
            {
                int randomIndex = Random.Range(i, n);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
        public void DeleteChildren(Transform parentTransform)
        {
            if (parentTransform != null)
            {
                // Iterate through each child and destroy it.
                for (int i = parentTransform.childCount - 1; i >= 0; i--)
                {
                    Transform child = parentTransform.GetChild(i);
                    Destroy(child.gameObject);
                }
            }
            
        }

    }
}