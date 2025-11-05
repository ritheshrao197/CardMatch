using MemoryGame.UI.Events;
using UnityEngine;
namespace MemoryGame.Services
{
    /// <summary>
    /// Service to handle quitting the application
    /// </summary>
    public class QuitService : MonoBehaviour
    {
        void OnEnable()
        {
            UIEvents.OnQuit += HandleQuit;
        }

        void OnDisable()
        {
            UIEvents.OnQuit -= HandleQuit;
        }

        void HandleQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID || UNITY_IOS
        Application.Quit(); 
#else
        Application.Quit();
#endif
        }
    }

}