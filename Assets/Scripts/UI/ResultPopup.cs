using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MemoryGame.Views
{
    public class ResultPopup : MonoBehaviour
    {
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI subtitleText;
        public Button nextButton;   // Next (win) / Retry (lose)
        public Button homeButton;

        Action _onNext;
        Action _onHome;

        public void Bind(bool win, int levelIndex, string reason, Action onNext, Action onHome)
        {
            Debug.Log($"[ResultPopup] Bind called with win={win}, levelIndex={levelIndex}, reason={reason}");
            _onNext = onNext; _onHome = onHome;
            if (titleText) titleText.text = win ? "Level Complete" : "Level Failed";
            if (subtitleText)
                subtitleText.text = win ? $"You finished Level {levelIndex + 1}"
                                        : (reason == "time" ? "Time's up" : "Move limit reached");
            if (nextButton) nextButton.GetComponentInChildren<TextMeshProUGUI>().text = win ? "Next" : "Retry";

            if (nextButton) { nextButton.onClick.RemoveAllListeners(); nextButton.onClick.AddListener(() => _onNext?.Invoke()); }
            if (homeButton) { homeButton.onClick.RemoveAllListeners(); homeButton.onClick.AddListener(() => _onHome?.Invoke()); }
        }
    }
}