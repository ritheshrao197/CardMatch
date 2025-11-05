using UnityEngine;
namespace MemoryGame
{
    /// <summary>
    /// Service to manage player progress and level unlocking
    /// </summary>
    public class ProgressService : MonoBehaviour
    {
        private const string KEY = "MG_HIGHEST_LEVEL";

        public int GetCurrentLevelIndex()
        {
            return Mathf.Clamp(PlayerPrefs.GetInt(KEY, 0), 0, int.MaxValue);
        }

        public void UnlockNextLevel(int justCompletedIndex)
        {
            int stored = PlayerPrefs.GetInt(KEY, 0);
            int next = Mathf.Max(stored, justCompletedIndex + 1);
            PlayerPrefs.SetInt(KEY, next);
            PlayerPrefs.Save();
        }

        public void ResetProgress()
        {
            PlayerPrefs.DeleteKey(KEY);
        }
    }

}