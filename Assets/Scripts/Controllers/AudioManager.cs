using MemoryGame.Audio.Events;
using MemoryGame.Events;
using MemoryGame.UI.Events;
using UnityEngine;

namespace MemoryGame.Controller
{

    /// <summary>
    /// Manages all game audio: sound effects (SFX) and background music.
    /// Handles event-driven playback, volume, and user toggles for SFX/music.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        // --- Card SFX ---
        [Header("Card SFX")]
        public AudioClip flip, match, mismatch; // Card flip, match, and mismatch sounds

        // --- UI SFX ---
        [Header("UI SFX")]
        public AudioClip click, popupOpen, popupClose; // UI click and popup sounds

        // --- Level SFX ---
        [Header("Level SFX")]
        public AudioClip levelStart, levelWin, levelFail; // Level start, win, and fail sounds

        // --- Music ---
        [Header("Music")]
        public AudioClip bgMusic; // Background music clip
        public bool playMusicOnAwake = true; // Should music play on game start?

        // --- Volume Controls ---
        [Header("Volumes")]
        [Range(0f, 1f)] public float sfxVolume = 1f;      // SFX volume
        [Range(0f, 1f)] public float musicVolume = 0.75f; // Music volume

        // --- Audio Sources ---
        private AudioSource _sfx;   // For SFX
        private AudioSource _music; // For music

        // --- PlayerPrefs Keys ---
        private const string KEY_SFX   = "MG_SFX_ENABLED";
        private const string KEY_MUSIC = "MG_MUSIC_ENABLED";
        private bool _sfxEnabled;   // Is SFX enabled?
        private bool _musicEnabled; // Is music enabled?


        /// <summary>
        /// Initializes audio sources, loads user preferences, and starts music if enabled.
        /// </summary>
        void Awake()
        {
            // Create SFX audio source
            _sfx = gameObject.AddComponent<AudioSource>();
            _sfx.playOnAwake = false;

            // Create music audio source
            _music = gameObject.AddComponent<AudioSource>();
            _music.loop = true;
            _music.playOnAwake = false;

            if (bgMusic) _music.clip = bgMusic;
            _music.volume = musicVolume;

            // Load SFX/music enabled state from PlayerPrefs
            _sfxEnabled   = PlayerPrefs.GetInt(KEY_SFX,   1) == 1;
            _musicEnabled = PlayerPrefs.GetInt(KEY_MUSIC, 1) == 1;

            // Start music if enabled
            if (playMusicOnAwake && _musicEnabled && _music.clip) _music.Play();

            // Notify UI of initial state
            AudioEvents.RaiseSfxEnabled(_sfxEnabled);
            AudioEvents.RaiseMusicEnabled(_musicEnabled);
        }


        /// <summary>
        /// Subscribes to game and UI events to trigger SFX/music.
        /// </summary>
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

            // Audio setting toggles
            UIEvents.OnToggleSfx   += ToggleSfx;
            UIEvents.OnToggleMusic += ToggleMusic;
        }


        /// <summary>
        /// Unsubscribes from all events to prevent memory leaks.
        /// </summary>
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
            // Can't unhook the lambda used for OnStartLevel easily; safe to leave

            UIEvents.OnToggleSfx   -= ToggleSfx;
            UIEvents.OnToggleMusic -= ToggleMusic;
        }


        // --- Event Handlers: Play SFX for game/UI events ---
        /// <summary>Plays card flip SFX when a card is selected.</summary>
        void OnCardSelected(CardController _) => PlaySfx(flip);
        /// <summary>Plays match SFX when a pair is matched.</summary>
        void OnPairMatched(CardController _, CardController __)    => PlaySfx(match);
        /// <summary>Plays mismatch SFX when a pair is mismatched.</summary>
        void OnPairMismatched(CardController _, CardController __) => PlaySfx(mismatch);
        /// <summary>Plays level start SFX and music when a level starts.</summary>
        void OnLevelStarted(LevelDef __, int ___)
        {
            PlaySfx(levelStart);
            if (_musicEnabled && _music.clip && !_music.isPlaying) _music.Play();
        }
        /// <summary>Plays win SFX when the game is won.</summary>
        void OnGameWon()                 => PlaySfx(levelWin);
        /// <summary>Plays fail SFX when the game is lost.</summary>
        void OnGameLost(string __)       => PlaySfx(levelFail);
        /// <summary>Plays click SFX for UI buttons.</summary>
        void HandleClick()               => PlaySfx(click);
        /// <summary>Plays popup open SFX.</summary>
        void HandlePopupOpen()           => PlaySfx(popupOpen);
        /// <summary>Plays popup close SFX.</summary>
        void HandlePopupClose()          => PlaySfx(popupClose);


        /// <summary>
        /// Plays a sound effect if SFX is enabled and the clip is valid.
        /// </summary>
        /// <param name="clip">The AudioClip to play.</param>
        void PlaySfx(AudioClip clip)
        {
            if (!_sfxEnabled || clip == null) return;
            _sfx.PlayOneShot(clip, sfxVolume);
        }

        // --- UI Toggles ---
        /// <summary>
        /// Toggles SFX on/off, saves preference, and notifies listeners.
        /// </summary>
        void ToggleSfx()
        {
            _sfxEnabled = !_sfxEnabled;
            PlayerPrefs.SetInt(KEY_SFX, _sfxEnabled ? 1 : 0);
            PlayerPrefs.Save();
            AudioEvents.RaiseSfxEnabled(_sfxEnabled);
        }

        /// <summary>
        /// Toggles music on/off, saves preference, and notifies listeners.
        /// </summary>
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
