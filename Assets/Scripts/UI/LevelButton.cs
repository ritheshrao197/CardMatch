using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MemoryGame.Views
{
    public class LevelButton : MonoBehaviour
    {
        public TextMeshProUGUI label;
        public Button button;

        public void Set(int levelNumber, Action onClick)
        {
            if (label) label.text = levelNumber.ToString();
            if (button)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => onClick?.Invoke());
            }
        }
    }

}