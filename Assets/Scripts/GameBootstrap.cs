using MemoryGame;
using MemoryGame.Config;
using MemoryGame.Controller;
using MemoryGame.Events;
using MemoryGame.Services;
using MemoryGame.UI.Events;
using MemoryGame.Views;
using UnityEngine;

namespace MemoryGame
{
    /// <summary>
    /// Main game bootstrap class that initializes and manages the game flow.
    /// Handles level progression, game state management, and coordination between services.
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        /// <summary>
        /// Game configuration data
        /// </summary>
        [Header("Data")]
        public GameConfig config;
        
        /// <summary>
        /// Collection of card sprites used in the game
        /// </summary>
        public CardSet cardSet;
        
        /// <summary>
        /// Configuration data for all game levels
        /// </summary>
        public LevelConfig levelConfig;

        /// <summary>
        /// References to scene objects and components
        /// </summary>
        [Header("Scene Refs")]
        public Transform boardRoot;
        public CardController cardPrefab;
        public MatchService matchService;
        public TimerService timerService;
        public LevelRules levelRules;
        public ProgressService progress;   
        public UIFlow uiFlow;             
        public BoardFrame frame;
        private BoardController _board;
        private ObjectPool<CardController> _pool;
        private int _levelIndex;

        private void OnEnable()
        {
            UIEvents.OnStartFromHome += CmdStartFromHome;
            UIEvents.OnStartLevel += CmdStartLevel;
            UIEvents.OnRestart += CmdRestart;
            UIEvents.OnGoHome += CmdGoHome;
            UIEvents.OnShowHUD += CmdShowHUD;
            UIEvents.OnPause += CmdPause;
            UIEvents.OnResume += CmdResume;
            UIEvents.OnResetProgress += CmdResetProgress;
        }
        
        private void OnDisable()
        {
            UIEvents.OnStartFromHome -= CmdStartFromHome;
            UIEvents.OnStartLevel -= CmdStartLevel;
            UIEvents.OnRestart -= CmdRestart;
            UIEvents.OnGoHome -= CmdGoHome;
            UIEvents.OnShowHUD -= CmdShowHUD;
            UIEvents.OnPause -= CmdPause;
            UIEvents.OnResume -= CmdResume;
            UIEvents.OnResetProgress -= CmdResetProgress;
        }

        /// <summary>
        /// Initializes the game on start
        /// </summary>
        private void Start()
        {
            if (!config || !cardSet || !boardRoot || !cardPrefab || !levelConfig)
            {
                Debug.LogError("GameBootstrap: Assign config, cardSet, levelConfig, boardRoot, cardPrefab.");
                return;
            }

            _pool = new ObjectPool<CardController>(cardPrefab, boardRoot, prewarm: 0);
            _board = new BoardController(boardRoot, config, cardSet, _pool,frame);

            _levelIndex = progress ? progress.GetCurrentLevelIndex() : 0;

            if (uiFlow) uiFlow.ShowHome(); else StartLevel(_levelIndex);

            GameEvents.OnGameWon += OnGameWon;
            GameEvents.OnGameLost += OnGameLost;
        }
        
        private void OnDestroy()
        {
            GameEvents.OnGameWon -= OnGameWon;
            GameEvents.OnGameLost -= OnGameLost;
        }

        /// <summary>
        /// Starts a specific level by index
        /// </summary>
        /// <param name="index">Zero-based index of the level to start</param>
        private void StartLevel(int index)
        {
            Debug.Log($"[GameBootstrap] Starting Level {index + 1}");
            if (index < 0 || index >= levelConfig.levels.Count)
            {
                Debug.Log("All levels complete. Restarting from Level 1.");
                index = 0;
            }
            _levelIndex = index;

            var def = levelConfig.levels[_levelIndex];

            // Per-level timing overrides
            if (def.flipDuration > 0) config.flipDuration = def.flipDuration;
            if (def.mismatchHideDelay > 0) config.mismatchHideDelay = def.mismatchHideDelay;

            _board.Build(def.rows, def.cols);

            matchService?.RegisterCards(_board.Cards);
            if (timerService != null) { timerService.ResetTimer(); timerService.StartTimer(); }
            levelRules?.BeginLevel(def, _levelIndex, timerService);

            GameEvents.RaiseLevelStarted(def, _levelIndex);
        }

        /// <summary>
        /// Handles game won event
        /// </summary>
        private void OnGameWon()
        {
            GameEvents.RaiseLevelCompleted(_levelIndex);
            progress?.UnlockNextLevel(_levelIndex);

            if (uiFlow)
            {
                uiFlow.ShowResult(true, _levelIndex, null,
                    onNext: () => {
                        StartLevel(_levelIndex + 1);
                        UIEvents.ShowHUD();
                    },
                    onHome: () => UIEvents.GoHome());
            }
            else StartLevel(_levelIndex + 1);
        }

        /// <summary>
        /// Handles game lost event
        /// </summary>
        /// <param name="reason">Reason for losing the game</param>
        private void OnGameLost(string reason)
        {
            if (uiFlow)
            {
                uiFlow.ShowResult(false, _levelIndex, reason,
                    onNext: () => StartLevel(_levelIndex),  // retry
                    onHome: () => UIEvents.GoHome());
            }
            else StartLevel(_levelIndex);
        }

        // === UIEvents handlers ===
        /// <summary>
        /// Command handler for starting from home screen
        /// </summary>
        void CmdStartFromHome() { var idx = progress ? progress.GetCurrentLevelIndex() : 0; StartLevel(idx); UIEvents.ShowHUD(); }
        
        /// <summary>
        /// Command handler for starting a specific level
        /// </summary>
        /// <param name="index">Index of the level to start</param>
        void CmdStartLevel(int index) { StartLevel(index); UIEvents.ShowHUD(); }
        
        /// <summary>
        /// Command handler for restarting the current level
        /// </summary>
        void CmdRestart() => StartLevel(_levelIndex);
        
        /// <summary>
        /// Command handler for returning to home screen
        /// </summary>
        void CmdGoHome() { uiFlow?.ShowHome(); timerService?.StopTimer(); }
        
        /// <summary>
        /// Command handler for showing the HUD
        /// </summary>
        void CmdShowHUD() { uiFlow?.ShowGameHUD(); }
        
        /// <summary>
        /// Command handler for pausing the game
        /// </summary>
        void CmdPause() { timerService?.StopTimer(); uiFlow?.ShowPause(); }
        
        /// <summary>
        /// Command handler for resuming the game
        /// </summary>
        void CmdResume() { timerService?.StartTimer(); uiFlow?.HidePause(); }
        
        /// <summary>
        /// Command handler for resetting player progress
        /// </summary>
        void CmdResetProgress() { progress?.ResetProgress(); }
    }
}