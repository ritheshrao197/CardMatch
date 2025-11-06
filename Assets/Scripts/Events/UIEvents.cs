using System;

namespace MemoryGame.UI.Events
{
    /// <summary>
    /// UI-related events for the Memory Game.
    /// Provides events for user interface interactions and navigation.
    /// </summary>
    public static class UIEvents
    {
        /// <summary>
        /// Event that fires when the user wants to start from the home screen
        /// </summary>
        public static event Action OnStartFromHome;
        
        /// <summary>
        /// Event that fires when the user wants to start a specific level.
        /// Parameter is the zero-based index of the level to start.
        /// </summary>
        public static event Action<int> OnStartLevel;
        
        /// <summary>
        /// Event that fires when the user wants to restart the current level
        /// </summary>
        public static event Action OnRestart;
        
        /// <summary>
        /// Event that fires when the user wants to go back to the home screen
        /// </summary>
        public static event Action OnGoHome;
        
        /// <summary>
        /// Event that fires when the user wants to show the HUD
        /// </summary>
        public static event Action OnShowHUD;

        /// <summary>
        /// Event that fires when the user wants to show the level selection screen
        /// </summary>
        public static event Action OnShowLevelSelect;
        
        /// <summary>
        /// Event that fires when the user wants to hide the level selection screen
        /// </summary>
        public static event Action OnHideLevelSelect;

        /// <summary>
        /// Event that fires when the user wants to pause the game
        /// </summary>
        public static event Action OnPause;
        
        /// <summary>
        /// Event that fires when the user wants to resume the game
        /// </summary>
        public static event Action OnResume;

        /// <summary>
        /// Event that fires when the user wants to reset their progress
        /// </summary>
        public static event Action OnResetProgress;
        
        /// <summary>
        /// Event that fires when the user wants to toggle SFX on/off
        /// </summary>
        public static event Action OnToggleSfx;
        
        /// <summary>
        /// Event that fires when the user wants to toggle music on/off
        /// </summary>
        public static event Action OnToggleMusic;
        
        /// <summary>
        /// Event that fires when the user wants to quit the application
        /// </summary>
        public static event Action OnQuit;

        /// <summary>
        /// Triggers the StartFromHome event
        /// </summary>
        public static void StartFromHome() => OnStartFromHome?.Invoke();
        
        /// <summary>
        /// Triggers the StartLevel event with the specified level index
        /// </summary>
        /// <param name="i">The zero-based index of the level to start</param>
        public static void StartLevel(int i) => OnStartLevel?.Invoke(i);
        
        /// <summary>
        /// Triggers the Restart event
        /// </summary>
        public static void Restart() => OnRestart?.Invoke();
        
        /// <summary>
        /// Triggers the GoHome event
        /// </summary>
        public static void GoHome() => OnGoHome?.Invoke();
        
        /// <summary>
        /// Triggers the ShowHUD event
        /// </summary>
        public static void ShowHUD() => OnShowHUD?.Invoke();

        /// <summary>
        /// Triggers the Quit event
        /// </summary>
        public static void Quit() => OnQuit?.Invoke();

        /// <summary>
        /// Triggers the ShowLevelSelect event
        /// </summary>
        public static void ShowLevelSelect() => OnShowLevelSelect?.Invoke();
        
        /// <summary>
        /// Triggers the HideLevelSelect event
        /// </summary>
        public static void HideLevelSelect() => OnHideLevelSelect?.Invoke();

        /// <summary>
        /// Triggers the Pause event
        /// </summary>
        public static void Pause() => OnPause?.Invoke();
        
        /// <summary>
        /// Triggers the Resume event
        /// </summary>
        public static void Resume() => OnResume?.Invoke();

        /// <summary>
        /// Triggers the ResetProgress event
        /// </summary>
        public static void ResetProgress() => OnResetProgress?.Invoke();

        /// <summary>
        /// Triggers the ToggleSfx event
        /// </summary>
        public static void ToggleSfx() => OnToggleSfx?.Invoke();
        
        /// <summary>
        /// Triggers the ToggleMusic event
        /// </summary>
        public static void ToggleMusic() => OnToggleMusic?.Invoke();
    }
}