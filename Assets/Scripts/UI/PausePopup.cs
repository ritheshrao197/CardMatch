using MemoryGame.UI.Events;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame.Views
{
public class PausePopup : MonoBehaviour
{
    public Button resumeButton;
    public Button restartButton;
    public Button homeButton;

    void Awake()
    {
        if (resumeButton)  resumeButton.onClick.AddListener(() => UIEvents.Resume());
        if (restartButton) restartButton.onClick.AddListener(() => UIEvents.Restart());
        if (homeButton)    homeButton.onClick.AddListener(() => UIEvents.GoHome());
    }

    void OnDestroy()
    {
        if (resumeButton)  resumeButton.onClick.RemoveAllListeners();
        if (restartButton) restartButton.onClick.RemoveAllListeners();
        if (homeButton)    homeButton.onClick.RemoveAllListeners();
    }
}

}