using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace MatchGame.View
{
    public class LevelComplteScreenView : View
    {
        [SerializeField] private Button _next;
        [SerializeField] private Button _quit;
        GameDataContainer _gameDataContainer;
        public CanvasGroup _screen;

        public override void Init()
        {
            base.Init();
            _screen = GetComponent<CanvasGroup>();  // Get the CanvasGroup component from the GameObject.

            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();

            eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, ShowMenu);
            _next.onClick.AddListener(() => Next());
            _quit.onClick.AddListener(() => Quit());
        }



        private void Next()
        {
            ServiceLocator.Instance.Get<AudioDataContainer>().OnButtonClick = SFX.ButtonClick;

            _gameDataContainer.CurrentLevel += 1;
            _gameDataContainer.Score = 0;
            _gameDataContainer.GameState = State.Play;
        }



        private void ShowMenu()
        {
            ServiceLocator.Instance.Get<AudioDataContainer>().OnButtonClick = SFX.ButtonClick;

            if (_gameDataContainer.GameState == State.GameOver)
            {
                EnableScreen();
            }
            else

            {
                DisableScreen();

            }
        }
        private void Quit()
        {
            _gameDataContainer.GameState = State.LevelSelection;
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
    }
}
