using System;
using MemoryGame.UI.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MemoryGame.Views
{
    public class LevelSelectionView : MonoBehaviour
    {
        [Header("UI")]
        public Transform content;      
        public Button levelButtonPrefab;  
        public Button closeButton;

        [Header("Refs")]
        public LevelConfig levelConfig;

        void Awake()
        {
            Build();
            if (closeButton) closeButton.onClick.AddListener(() => UIEvents.HideLevelSelect());
        }
        void OnDestroy() { if (closeButton) closeButton.onClick.RemoveAllListeners(); }

        public void Rebuild()
        {
            for (int i = content.childCount - 1; i >= 0; i--) Destroy(content.GetChild(i).gameObject);
            Build();
        }

        void Build()
        {
            if (!content || !levelButtonPrefab || !levelConfig) return;
            int unlocked = UIQueries.GetUnlockedLevelCount(levelConfig.levels.Count);

            for (int i = 0; i < levelConfig.levels.Count; i++)
            {
                var btn = Instantiate(levelButtonPrefab, content);
                var txt = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (txt) txt.text = (i + 1).ToString();

                bool isUnlocked = i < unlocked;
                btn.interactable = isUnlocked;

                int levelIndex = i;
                btn.onClick.AddListener(() =>
                {
                    UIEvents.HideLevelSelect();
                    UIEvents.StartLevel(levelIndex);
                });
            }
        }
    }
}