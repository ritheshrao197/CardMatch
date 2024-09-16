using MatchGame.Core;

namespace MatchGame.Data
{
    public enum SFX
    {
        ButtonClick,
        WrongMatch,
        CorrectMatch,
        LevelComplete
    }
    public class AudioDataContainer
    {
        private SFX _onButtonClick;
        public SFX OnButtonClick
        {
            get
            {
                return _onButtonClick;
            }
            set
            {
                _onButtonClick = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(AudioEventKeys.OnButtonClick);


            }
        }


    }

}