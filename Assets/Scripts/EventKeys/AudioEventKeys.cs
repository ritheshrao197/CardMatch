namespace MatchGame.Data
{
    public class AudioEventKeys
    {
        public static int OnButtonClick = KeyGenerator.GetKey();
        public static int PlayBg = KeyGenerator.GetKey();
        public static int MusicOff = KeyGenerator.GetKey();
        public static int MusicOn = KeyGenerator.GetKey();
        public static int Soundff = KeyGenerator.GetKey();
        public static int SoundOn = KeyGenerator.GetKey();
    }
    public class GameEventKeys
    {
        public static int CurrentLevelUpdated = KeyGenerator.GetKey();
        public static int GenerateGrid = KeyGenerator.GetKey();
        public static int GetIndexNumbers = KeyGenerator.GetKey();
        public static int IndexNumbersUpdated = KeyGenerator.GetKey();
        public static int GridSizeUpdated = KeyGenerator.GetKey();
        public static int FlipCards = KeyGenerator.GetKey();
        public static int CardMatched = KeyGenerator.GetKey();
        public static int TimerUpdated = KeyGenerator.GetKey();

        public static int CardDisplayed = KeyGenerator.GetKey();
        public static int GameStateUpdated = KeyGenerator.GetKey();
        public static int ScoreUpdated = KeyGenerator.GetKey();
        public static int UpdateTimer = KeyGenerator.GetKey();
        public static int StartTimer = KeyGenerator.GetKey();
        public static int GameOver = KeyGenerator.GetKey();


    }

    public class SettingsEventKeys
    {
        public static int SetSound = KeyGenerator.GetKey();
        public static int SetMusic= KeyGenerator.GetKey();
    }

}