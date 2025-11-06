using MemoryGame.UI.Events;
using UnityEngine;

namespace MemoryGame.Services
{
    /// <summary>
    /// Service to handle quitting the application.
    /// Listens for quit events and properly terminates the application based on the platform.
    /// </summary>
    public class QuitService : MonoBehaviour
    {
        /// <summary>
        /// Registers the quit event handler when the component is enabled
        /// </summary>
        void OnEnable()
        {
            UIEvents.OnQuit += HandleQuit;
        }

        /// <summary>
        /// Unregisters the quit event handler when the component is disabled
        /// </summary>
        void OnDisable()
        {
            UIEvents.OnQuit -= HandleQuit;
        }

        /// <summary>
        /// Handles the quit event by terminating the application.
        /// Uses the appropriate method based on the platform (Editor, mobile, or standalone).
        /// </summary>
        void HandleQuit()
        {
#if UNITY_EDITOR
            // In the Unity Editor, stop play mode
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID || UNITY_IOS
            // On mobile platforms, quit the application
            Application.Quit(); 
#else
            // On other platforms, quit the application
            Application.Quit();
#endif
        }
    }
}