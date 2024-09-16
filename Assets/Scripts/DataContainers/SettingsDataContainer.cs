using MatchGame.Core;

namespace MatchGame.Data
{
    public class SettingsDataContainer
    {
        private bool _isSoundOn = true;
        public bool IsSoundOn
        {
            get
            {
                return _isSoundOn;
            }
            set
            {
                _isSoundOn = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(SettingsEventKeys.SetSound);


            }
        }
        private bool _isMusicOn = true;
        public bool IsMusicOn
        {
            get
            {
                return _isMusicOn;
            }
            set
            {
                _isMusicOn = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(SettingsEventKeys.SetMusic);


            }
        }
    }

}