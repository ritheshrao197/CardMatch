
using System;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
namespace MatchGame.View
{
    public class GamePlayView : View
    {
        [SerializeField] private Button _pause;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _time;
        GameDataContainer _gameDataContainer;
        public CanvasGroup _screen;

        public override void Init()
        {
            base.Init();
            _screen = GetComponent<CanvasGroup>();  // Get the CanvasGroup component from the GameObject.

            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();

            _pause.onClick.AddListener(() => OnPause());
            eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, ShowGamePlay);
            eventHandlerSystem.AddListener(GameEventKeys.ScoreUpdated, ScoreUpdated);
            eventHandlerSystem.AddListener(GameEventKeys.TimerUpdated, TimerUpdated);
        }

        private void OnPause()
        {
            ServiceLocator.Instance.Get<AudioDataContainer>().OnButtonClick = SFX.ButtonClick;
            _gameDataContainer.GameState = State.Pause;
        }
        public override void Finalise()
        {
            base.Finalise();
            _pause.onClick.RemoveAllListeners();
            eventHandlerSystem.RemoveListener(GameEventKeys.GameStateUpdated, ShowGamePlay);
            eventHandlerSystem.RemoveListener(GameEventKeys.ScoreUpdated, ScoreUpdated);
            eventHandlerSystem.RemoveListener(GameEventKeys.TimerUpdated, TimerUpdated);

        }

        private void TimerUpdated()
        {
            _time.text = "Time :" + _gameDataContainer.CurrentTime.ToString("F0"); ;
        }

        private void ScoreUpdated()
        {
             _score.text = "Score :"+_gameDataContainer.Score +"/"+ _gameDataContainer.MaxScore;
        }

        private void ShowGamePlay()
        {
            if (_gameDataContainer.GameState == State.Play )
            {
                EnableScreen ();
            }
            else if (_gameDataContainer.GameState != State.Pause)
            {
                DisableScreen();
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
    }
}