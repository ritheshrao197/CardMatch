
using System;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
namespace MatchGame.View
{
    public class MainMenuView : View
    {
        [SerializeField] private Button _playbtn;
        [SerializeField] private Button _quitbtn;
        [SerializeField] private Button _soundbtn;
        [SerializeField] private Button _musicbtn;
        GameDataContainer _gameDataContainer;
        SettingsDataContainer _settingsDataContainer;
        public CanvasGroup _screen;

        public override void Init()
        {
            base.Init();
            //_play.GetComponent<Button>();
            _screen = GetComponent<CanvasGroup>();  // Get the CanvasGroup component from the GameObject.

            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            _settingsDataContainer = ServiceLocator.Instance.Get<SettingsDataContainer>();
            ResetButtons();
            _playbtn.onClick.AddListener(() => OnPlayButtonClick());
            _quitbtn.onClick.AddListener(() => OnQuitButtonClick());
            _soundbtn.onClick.AddListener(() => OnSoundButtonClick());
            _musicbtn.onClick.AddListener(() => OnMusicButtonClick());
            eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, ShowMenu);
        }


        private void OnSoundButtonClick()
        {

            _settingsDataContainer.IsSoundOn = !_settingsDataContainer.IsSoundOn;
            _soundbtn.GetComponentInChildren<TextMeshProUGUI>().text = _settingsDataContainer.IsSoundOn ? "On" : "Off";
            Debug.Log(_settingsDataContainer.IsSoundOn);
        }

        private void OnMusicButtonClick()
        {
            _settingsDataContainer.IsMusicOn = !_settingsDataContainer.IsMusicOn;
            _musicbtn.GetComponentInChildren<TextMeshProUGUI>().text = _settingsDataContainer.IsMusicOn ? "On" : "Off";


        }

        private void OnQuitButtonClick()
        {
            Application.Quit();

         

        }

        private void OnPlayButtonClick()
        {
            ServiceLocator.Instance.Get<AudioDataContainer>().OnButtonClick = SFX.ButtonClick;

            _gameDataContainer.GameState = State.LevelSelection;
        }
        public override void Finalise()
        {
            base.Finalise();
            _playbtn.onClick.RemoveAllListeners();
            eventHandlerSystem.RemoveListener(GameEventKeys.GameStateUpdated, ShowMenu);

        }

        private void ShowMenu()
        {
           if( _gameDataContainer.GameState == State.MainMenu)
            {
                EnableScreen();
            }
            else 

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

        private void ResetButtons()
        {
            _playbtn.onClick.RemoveAllListeners();
            _quitbtn.onClick.RemoveAllListeners();
            _soundbtn.onClick.RemoveAllListeners();
            _musicbtn.onClick.RemoveAllListeners();
        }
    }
}
