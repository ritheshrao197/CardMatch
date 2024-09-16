using System;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace MatchGame.View
{
    public class LevelSelectionView : View
    {
        [SerializeField] private GameObject _levelPrefab;
        [SerializeField] private GameObject _levelParent;
        GameDataContainer _gameDataContainer;
        public CanvasGroup _screen;
        public Button _backButton;

        public override void Init()
        {
            base.Init();
            _screen = GetComponent<CanvasGroup>();  // Get the CanvasGroup component from the GameObject.

            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();

            _backButton.onClick.AddListener(() => BackButton()); 
            eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, ShowMenu);
        }

      
        public override void Finalise()
        {
            base.Finalise();
            eventHandlerSystem.RemoveListener(GameEventKeys.GameStateUpdated, ShowMenu);

        }
        private void BackButton()
        {
            ServiceLocator.Instance.Get<AudioDataContainer>().OnButtonClick = SFX.ButtonClick;
            _gameDataContainer.GameState = State.MainMenu;
        }


        private void ShowMenu()
        {
            if (_gameDataContainer.GameState == State.LevelSelection )
            {
                GenerateLevels();
                EnableScreen(); 
            }
            else 

            {
                DisableScreen();
            }
        }
        public void GenerateLevels()
        {

           
            DeleteChildren(_levelParent.transform);
            for (int i = 0; i < Constants.GridSizes.Count; i++)
            {
                // Create an instance of the prefab at a random position.
                GameObject cardObject = Instantiate(_levelPrefab, _levelParent.transform);
                LevelView level = cardObject.GetComponent<LevelView>();
                level.SetLevelButton(i+1);
            }
        }
        /// <summary>
        /// Enable the screen by making it interactable and visible.
        /// </summary>
        public void EnableScreen()
        {
            _screen.interactable = true;
            _screen.blocksRaycasts = true;
            _screen.alpha = 1;
        }

        /// <summary>
        /// Disable the screen by making it non-interactable and invisible.
        /// </summary>
        public void DisableScreen()
        {
            // Deactivate the current screen.
            _screen.interactable = false;
            _screen.blocksRaycasts = false;
            _screen.alpha = 0;
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