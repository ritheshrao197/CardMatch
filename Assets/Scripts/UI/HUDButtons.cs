using MemoryGame.UI.Events;
using UnityEngine;
using UnityEngine.UI;
namespace MemoryGame.Views
{
    /// <summary>
    /// View component for HUD buttons in the Memory Game
    /// </summary>
    public class HUDButtons : MonoBehaviour
    {
        public Button pauseButton;

        void Awake() { if (pauseButton) pauseButton.onClick.AddListener(() => UIEvents.Pause()); }
        void OnDestroy() { if (pauseButton) pauseButton.onClick.RemoveAllListeners(); }
    }

}