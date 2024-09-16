using System;
using System.Collections;
using MatchGame.Data;
using MatchGame.View;
using UnityEngine;
namespace MatchGame.Core
{
    public class CoreContext : MonoBehaviour
    {

        private static CoreContext instance;

        public static CoreContext Instance
        {
            get
            {

                return instance;
            }
        }
        // Reference to various managers, such as input manager, UI manager, etc.
        ServiceLocator _serviceLocator;
        EventHandlerSystem _eventHandlerSystem;
        //core systems
        SpriteProviderSystem _spriteProviderSystem;

        CardGenerator _cardGenerator;
        CardMatchSystem _cardMatchSystem;
        TimerSystem _timerSystem;
        ScoringSystem _scoringSystem;

        GameDataContainer _gameDataContainer = new();
        AudioDataContainer _audioDataContainer;
        SettingsDataContainer _settingsDataContainer;


        //views
        [SerializeField] GamePlayView _gamePlayView;
        [SerializeField] MainMenuView _mainMenuView;
        [SerializeField] LevelSelectionView _levelSelectionView;
        [SerializeField] PauseScreenView _pauseScreenView;
        [SerializeField] LevelComplteScreenView _levelComplteScreenView;
        [SerializeField] AudioManager _audioManager;


        [SerializeField] GridLayoutHandler _gridLayoutHandler;
    

        private void Awake()
        {
            if (instance == null)
                instance = this;
            // Initialize your managers and other components here.
            SystemInitialisation();
            DataContainerRegistration();
           
            CoreSystemInitialisation();
            ViewInitialisation();

            InitialiseAudioSystem();
        }

        private void InitialiseAudioSystem()
        {
            _audioManager.Init();
            _audioManager.AddListener();

        }

        private void ViewInitialisation()
        {
            _gamePlayView.Init();
            _mainMenuView.Init();
            _levelSelectionView.Init();
            _pauseScreenView.Init();
            _levelComplteScreenView.Init();
        }

        private void CoreSystemInitialisation()
        {

            _spriteProviderSystem = new();
            _spriteProviderSystem.Init();
            _spriteProviderSystem.AddListener();

            _cardGenerator = new();
            _cardGenerator.Init();
            _cardGenerator.AddListener();


            _cardMatchSystem = new();
            _cardMatchSystem.Init();
            _serviceLocator.Register(_cardMatchSystem);

            _timerSystem = new();
            _timerSystem.Init();
            _serviceLocator.Register(_timerSystem);




            _scoringSystem = new();
            _scoringSystem.Init();
            _serviceLocator.Register(_scoringSystem);

        }

        private void SystemInitialisation()
        {
            _serviceLocator = new();
            _eventHandlerSystem = new();
            _audioDataContainer = new();
            _settingsDataContainer = new();
          



        }

        private void DataContainerRegistration()
        {
            _serviceLocator.Register(_eventHandlerSystem);
            _serviceLocator.Register(_audioDataContainer);
            _serviceLocator.Register(_gameDataContainer);
            _serviceLocator.Register(_audioDataContainer);
            _serviceLocator.Register(_settingsDataContainer);
        }
      
       
    }
}