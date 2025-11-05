using UnityEngine;
namespace MemoryGame.Views
{
    public static class UIQueries
{
    private const string KEY = "MG_HIGHEST_LEVEL";

    public static int GetUnlockedLevelCount(int totalLevels)
    {
        int highest = PlayerPrefs.GetInt(KEY, 0); // index
        return Mathf.Clamp(highest + 1, 1, totalLevels);
    }
}

}