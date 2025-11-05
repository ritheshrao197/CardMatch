using MemoryGame.Audio.Events;
using MemoryGame.Events;
using MemoryGame.UI.Events;
using UnityEngine;

namespace MemoryGame.Controller
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Card SFX")]
        public AudioClip flip, match, mismatch;

        [Header("UI SFX")]
        public AudioClip click, popupOpen, popupClose;

        [Header("Level SFX")]
        public AudioClip levelStart, levelWin, levelFail;

        [Header("Music")]
        public AudioClip bgMusic;
        public bool playMusicOnAwake = true;

        [Header("Volumes")]
        [Range(0f, 1f)] public float sfxVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 0.75f;

        private AudioSource _sfx;
        private AudioSource _music;

        private const string KEY_SFX   = "MG_SFX_ENABLED";
        private const string KEY_MUSIC = "MG_MUSIC_ENABLED";
        private bool _sfxEnabled;
        private bool _musicEnabled;

        void Awake()
        {
            _sfx = gameObject.AddComponent<AudioSource>();
            _sfx.playOnAwake = false;

            _music = gameObject.AddComponent<AudioSource>();
            _music.loop = true;
            _music.playOnAwake = false;

            if (bgMusic) _music.clip = bgMusic;
            _music.volume = musicVolume;

            _sfxEnabled   = PlayerPrefs.GetInt(KEY_SFX,   1) == 1;
            _musicEnabled = PlayerPrefs.GetInt(KEY_MUSIC, 1) == 1;

            if (playMusicOnAwake && _musicEnabled && _music.clip) _music.Play();

            // Tell UI the initial state (no direct link)
            AudioEvents.RaiseSfxEnabled(_sfxEnabled);
            AudioEvents.RaiseMusicEnabled(_musicEnabled);
        }

        void OnEnable()
        {
            // Game event SFX
            GameEvents.OnCardSelected   += OnCardSelected;
            GameEvents.OnPairMatched    += OnPairMatched;
            GameEvents.OnPairMismatched += OnPairMismatched;
            GameEvents.OnLevelStarted   += OnLevelStarted;
            GameEvents.OnGameWon        += OnGameWon;
            GameEvents.OnGameLost       += OnGameLost;

            // UI interaction SFX
            UIEvents.OnPause           += HandlePopupOpen;
            UIEvents.OnResume          += HandlePopupClose;
            UIEvents.OnShowLevelSelect += HandlePopupOpen;
            UIEvents.OnHideLevelSelect += HandlePopupClose;
            UIEvents.OnGoHome          += HandleClick;
            UIEvents.OnRestart         += HandleClick;
            UIEvents.OnStartFromHome   += HandleClick;
            UIEvents.OnStartLevel      += _ => HandleClick();

            // Decoupled audio setting toggles
            UIEvents.OnToggleSfx   += ToggleSfx;
            UIEvents.OnToggleMusic += ToggleMusic;
        }

        void OnDisable()
        {
            GameEvents.OnCardSelected   -= OnCardSelected;
            GameEvents.OnPairMatched    -= OnPairMatched;
            GameEvents.OnPairMismatched -= OnPairMismatched;
            GameEvents.OnLevelStarted   -= OnLevelStarted;
            GameEvents.OnGameWon        -= OnGameWon;
            GameEvents.OnGameLost       -= OnGameLost;

            UIEvents.OnPause           -= HandlePopupOpen;
            UIEvents.OnResume          -= HandlePopupClose;
            UIEvents.OnShowLevelSelect -= HandlePopupOpen;
            UIEvents.OnHideLevelSelect -= HandlePopupClose;
            UIEvents.OnGoHome          -= HandleClick;
            UIEvents.OnRestart         -= HandleClick;
            UIEvents.OnStartFromHome   -= HandleClick;
            // can't unhook the lambda used for OnStartLevel easily; safe to leave

            UIEvents.OnToggleSfx   -= ToggleSfx;
            UIEvents.OnToggleMusic -= ToggleMusic;
        }

        // -- SFX plays --
        void OnCardSelected(CardController _) => PlaySfx(flip);
        void OnPairMatched(CardController _, CardController __)    => PlaySfx(match);
        void OnPairMismatched(CardController _, CardController __) => PlaySfx(mismatch);
        void OnLevelStarted(LevelDef __, int ___)
        {
            PlaySfx(levelStart);
            if (_musicEnabled && _music.clip && !_music.isPlaying) _music.Play();
        }
        void OnGameWon()                 => PlaySfx(levelWin);
        void OnGameLost(string __)       => PlaySfx(levelFail);
        void HandleClick()               => PlaySfx(click);
        void HandlePopupOpen()           => PlaySfx(popupOpen);
        void HandlePopupClose()          => PlaySfx(popupClose);

        void PlaySfx(AudioClip clip)
        {
            if (!_sfxEnabled || clip == null) return;
            _sfx.PlayOneShot(clip, sfxVolume);
        }

        // -- Toggles from UI (no direct references) --
        void ToggleSfx()
        {
            _sfxEnabled = !_sfxEnabled;
            PlayerPrefs.SetInt(KEY_SFX, _sfxEnabled ? 1 : 0);
            PlayerPrefs.Save();
            AudioEvents.RaiseSfxEnabled(_sfxEnabled);
        }

        void ToggleMusic()
        {
            _musicEnabled = !_musicEnabled;
            PlayerPrefs.SetInt(KEY_MUSIC, _musicEnabled ? 1 : 0);
            PlayerPrefs.Save();

            if (_musicEnabled)
            {
                if (_music.clip && !_music.isPlaying) _music.Play();
            }
            else if (_music.isPlaying) _music.Stop();

            _music.volume = musicVolume;
            AudioEvents.RaiseMusicEnabled(_musicEnabled);
        }
    }
}
