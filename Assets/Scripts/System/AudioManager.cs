using System;
using System.Collections.Generic;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _backgroundMusic;
    public AudioSource _soundEffect;

    public AudioClip _buttonClick;
    public AudioClip _sfxButtonClick;
    public AudioClip _sfxCorrectMatch;
    public AudioClip _sfxLevelComplete;
    public AudioClip _sfxWrongMatch;
    public AudioClip _sfx5;

    public AudioClip _bgmusic;

    EventHandlerSystem _eventHandlerSystem;
    GameDataContainer _gameDataContainer;
    AudioDataContainer _audioDataContainer;
    SettingsDataContainer _settingsDataContainer;
    //public static AudioManager instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    public void Init()
    {
        _settingsDataContainer = ServiceLocator.Instance.Get<SettingsDataContainer>();
        _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
        _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
        _audioDataContainer = ServiceLocator.Instance.Get<AudioDataContainer>();
    }
    public  void AddListener()
    {
        _eventHandlerSystem.AddListener(AudioEventKeys.OnButtonClick, PlaySoundEffect);
        _eventHandlerSystem.AddListener(AudioEventKeys.PlayBg, PlayBackgroundMusic);
        _eventHandlerSystem.AddListener(SettingsEventKeys.SetMusic, SetMusic);

    }
    public  void RemoveListener()
    {
        _eventHandlerSystem.RemoveListener(AudioEventKeys.OnButtonClick, PlaySoundEffect);
        _eventHandlerSystem.RemoveListener(AudioEventKeys.PlayBg, PlayBackgroundMusic);
        _eventHandlerSystem.RemoveListener(SettingsEventKeys.SetMusic, SetMusic);


    }

    private void SetMusic()
    {
        if (_settingsDataContainer.IsMusicOn)
        {
            PlayBackgroundMusic();
        }
        else
            StopBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        _backgroundMusic.clip = _bgmusic;
        _backgroundMusic.Play();
    }
    public void StopBackgroundMusic()
    {
        _backgroundMusic.Stop();
    }

    public void PlaySoundEffect()
    {
        if(_settingsDataContainer.IsSoundOn)
        switch(_audioDataContainer.OnButtonClick)
        {
            case SFX.ButtonClick:
                _soundEffect.PlayOneShot(_sfxButtonClick);

                break;
            case SFX.CorrectMatch:
                _soundEffect.PlayOneShot(_sfxCorrectMatch);

                break;
            case SFX.LevelComplete:
                _soundEffect.PlayOneShot(_sfxLevelComplete);

                break;
            case SFX.WrongMatch:
                _soundEffect.PlayOneShot(_sfxWrongMatch);

                break;
         
        }
    }

}
