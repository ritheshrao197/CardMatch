using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MemoryGame.Constants; 

namespace MemoryGame.Views
{
    public class ResultPopupView : MonoBehaviour
    {
        [Header("Wiring")]
        public CanvasGroup group;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI subtitleText;
        public Button nextButton;
        public Button homeButton;

        public Action onNext;
        public Action onHome;

        void Reset()
        {
            group = GetComponent<CanvasGroup>();
        }

        public void Show(bool win, int levelIndex, string loseReason)
        {
            if (group != null)
            {
                group.alpha = 1f;
                group.blocksRaycasts = true;
                group.interactable = true;
            }

            gameObject.SetActive(true);

            if (titleText)
                titleText.text = win ? UIStrings.WinTitle : UIStrings.LoseTitle;

            if (subtitleText)
                subtitleText.text = win
                    ? $"Level {levelIndex + 1}"
                    : (string.IsNullOrEmpty(loseReason)
                        ? $"Level {levelIndex + 1}"
                        : loseReason);

            if (nextButton)
            {
                var label = nextButton.GetComponentInChildren<Text>();
                if (label)
                    label.text = win ? UIStrings.NextOnWin : UIStrings.NextOnLose;

                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() => onNext?.Invoke());
            }

            if (homeButton)
            {
                homeButton.onClick.RemoveAllListeners();
                homeButton.onClick.AddListener(() => onHome?.Invoke());
            }
        }

        public void Hide()
        {
            if (group != null)
            {
                group.alpha = 0f;
                group.blocksRaycasts = false;
                group.interactable = false;
            }
            gameObject.SetActive(false);
        }

        public void HideImmediate() => Hide();
    }
}
