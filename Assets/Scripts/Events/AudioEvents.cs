
using System;
namespace MemoryGame.Audio.Events
{
    /// <summary>
    /// Events related to audio settings changes
    /// </summary>
    public static class AudioEvents
{
    public static event Action<bool> OnSfxEnabledChanged;
    public static event Action<bool> OnMusicEnabledChanged;

    public static void RaiseSfxEnabled(bool enabled)   => OnSfxEnabledChanged?.Invoke(enabled);
    public static void RaiseMusicEnabled(bool enabled) => OnMusicEnabledChanged?.Invoke(enabled);
}

}