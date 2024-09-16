using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;

namespace MatchGame
{

    public class SpriteProviderSystem : CoreSystem
    {

        EventHandlerSystem _eventHandlerSystem;
        GameDataContainer _gameDataContainer;

        public string resourcesPath = "SpriteContainer"; // Path to the Resources folder.

        public override void Init()
        {
            base.Init();
            _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            LoadSprites();
        }
        public override void AddListener()
        {
            base.AddListener();
            //_eventHandlerSystem.AddListener(GameEventKeys.CurrentLevelUpdated, LoadSprites);

        }

        public override void RemoveListener()
        {
            base.RemoveListener();
            //_eventHandlerSystem.RemoveListener(GameEventKeys.CurrentLevelUpdated, LoadSprites);

        }
        public override void Finalise()
        {
            base.Finalise();

        }



        public void LoadSprites()
        {
            if (!string.IsNullOrEmpty(resourcesPath))
            {
                _gameDataContainer.Sprites = Resources.Load<SpriteContainer>(resourcesPath).spriteList;
                //Debug.Log("_gameDataContainer"+ _gameDataContainer.Sprites.Length);

            }
            else
            {
                Debug.LogError("Resource path is not set in SpriteManager.");
                //return null;
            }
        }

    }
}