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

    public class GameBootstrap : MonoBehaviour
    {
        [Header("Data")]
        public GameConfig config;
        public CardSet cardSet;
        public LevelConfig levelConfig;

        [Header("Scene Refs")]
        public Transform boardRoot;
        public CardController cardPrefab;
        public MatchService matchService;
        public TimerService timerService;
        public LevelRules levelRules;
        public ProgressService progress;   // optional
        public UIFlow uiFlow;              // for result/home/hud visibility

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

        private void Start()
        {
            if (!config || !cardSet || !boardRoot || !cardPrefab || !levelConfig)
            {
                Debug.LogError("GameBootstrap: Assign config, cardSet, levelConfig, boardRoot, cardPrefab.");
                return;
            }

            _pool = new ObjectPool<CardController>(cardPrefab, boardRoot, prewarm: 0);
            _board = new BoardController(boardRoot, config, cardSet, _pool);

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
        void CmdStartFromHome() { var idx = progress ? progress.GetCurrentLevelIndex() : 0; StartLevel(idx); UIEvents.ShowHUD(); }
        void CmdStartLevel(int index) { StartLevel(index); UIEvents.ShowHUD(); }
        void CmdRestart() => StartLevel(_levelIndex);
        void CmdGoHome() { uiFlow?.ShowHome(); timerService?.StopTimer(); }
        void CmdShowHUD() { uiFlow?.ShowGameHUD(); }
        void CmdPause() { timerService?.StopTimer(); uiFlow?.ShowPause(); }
        void CmdResume() { timerService?.StartTimer(); uiFlow?.HidePause(); }
        void CmdResetProgress() { progress?.ResetProgress(); }
    }
}
