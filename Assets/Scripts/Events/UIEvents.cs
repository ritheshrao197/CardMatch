
using System;
namespace MemoryGame.UI.Events
{
    /// <summary>
    /// UI-related events for the Memory Game
    /// </summary>
    public static class UIEvents
    {
        public static event Action OnStartFromHome;
        public static event Action<int> OnStartLevel;
        public static event Action OnRestart;
        public static event Action OnGoHome;
        public static event Action OnShowHUD;

        public static event Action OnShowLevelSelect;
        public static event Action OnHideLevelSelect;

        public static event Action OnPause;
        public static event Action OnResume;

        public static event Action OnResetProgress;
        public static event Action OnToggleSfx;
        public static event Action OnToggleMusic;
        public static event Action OnQuit;

        public static void StartFromHome() => OnStartFromHome?.Invoke();
        public static void StartLevel(int i) => OnStartLevel?.Invoke(i);
        public static void Restart() => OnRestart?.Invoke();
        public static void GoHome() => OnGoHome?.Invoke();
        public static void ShowHUD() => OnShowHUD?.Invoke();

        public static void Quit() => OnQuit?.Invoke();

        public static void ShowLevelSelect() => OnShowLevelSelect?.Invoke();
        public static void HideLevelSelect() => OnHideLevelSelect?.Invoke();

        public static void Pause() => OnPause?.Invoke();
        public static void Resume() => OnResume?.Invoke();

        public static void ResetProgress() => OnResetProgress?.Invoke();

        public static void ToggleSfx() => OnToggleSfx?.Invoke();
        public static void ToggleMusic() => OnToggleMusic?.Invoke();

    }

}