using System.Collections;
using System.Collections.Generic;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace MatchGame.View
{

    public class LevelView : View
    {

       [SerializeField] private Button _play;
        [SerializeField]private TextMeshProUGUI levelNumberText;
        [SerializeField] private int _levelNumber;
        GameDataContainer _gameDataContainer;
       public void SetLevelButton(int levelNumber)
        {
            Init();
            _play = GetComponent<Button>();
            _play.onClick.RemoveAllListeners();
            _play.interactable = levelNumber <= _gameDataContainer.UnlockedLevel;
            _levelNumber = levelNumber;
            levelNumberText.text =""+levelNumber;
        }
        public override void Init()
        {
            base.Init();

            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();

            _play.onClick.AddListener(() => OnPlayButtonClick()); 
        }

        private void OnPlayButtonClick()
        {

            _gameDataContainer.CurrentLevel = _levelNumber;
            _gameDataContainer.GameState = State.Play;
            _gameDataContainer.Score = 0;


        }

    }
}