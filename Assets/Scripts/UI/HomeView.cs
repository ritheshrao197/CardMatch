using MemoryGame.Audio.Events;
using MemoryGame.UI.Events;
using UnityEngine;
using UnityEngine.UI;
namespace MemoryGame.Views
{
    /// <summary>
    /// View component for the Home screen of the Memory Game
    /// </summary>
    public class HomeView : MonoBehaviour
    {
        [Header("Buttons")]

        public Button startButton;
        public Button resetProgressButton;
        public Button levelSelectButton;
        public Button sfxButton;
        public Button exitButton;
        public Button musicButton;
        [Header("Icons")]
        public Image sfxIcon;
        public Image musicIcon;
        [Header("Sprites")]

        public Sprite onSpriteSfx;
        public Sprite offSpriteSfx;
        public Sprite onSpriteMusic;
        public Sprite offSpriteMusic;

        void Awake()
        {
            if (startButton) startButton.onClick.AddListener(() => UIEvents.StartFromHome());
            if (resetProgressButton) resetProgressButton.onClick.AddListener(() => UIEvents.ResetProgress());
            if (levelSelectButton) levelSelectButton.onClick.AddListener(() => UIEvents.ShowLevelSelect());
            if (sfxButton) sfxButton.onClick.AddListener(() => UIEvents.ToggleSfx());
            if (musicButton) musicButton.onClick.AddListener(() => UIEvents.ToggleMusic());
            if (exitButton) exitButton.onClick.AddListener(() => UIEvents.Quit());

        }
        void OnEnable()
        {
            AudioEvents.OnSfxEnabledChanged += OnSfxChanged;
            AudioEvents.OnMusicEnabledChanged += OnMusicChanged;
        }
        void OnDisable()
        {
            AudioEvents.OnSfxEnabledChanged -= OnSfxChanged;
            AudioEvents.OnMusicEnabledChanged -= OnMusicChanged;
        }
        void OnDestroy()
        {
            if (startButton) startButton.onClick.RemoveAllListeners();
            if (resetProgressButton) resetProgressButton.onClick.RemoveAllListeners();
            if (levelSelectButton) levelSelectButton.onClick.RemoveAllListeners();
             if (sfxButton) sfxButton.onClick.RemoveAllListeners();
            if (musicButton) musicButton.onClick.RemoveAllListeners();
            if (exitButton) exitButton.onClick.RemoveAllListeners();
        }
        void OnSfxChanged(bool enabled)
        {
            if (sfxIcon) sfxIcon.sprite = enabled ? onSpriteSfx : offSpriteSfx;
        }

        void OnMusicChanged(bool enabled)
        {
            if (musicIcon) musicIcon.sprite = enabled ? onSpriteMusic : offSpriteMusic;
        }
    }

}