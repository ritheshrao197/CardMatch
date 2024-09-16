using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;

namespace MatchGame
{
    public class CardMatchSystem : CoreSystem
    {
        //public List<Card> cards; // List of all cards in the game
        private Card firstCard; // Store the first flipped card
        EventHandlerSystem _eventHandlerSystem;
        GameDataContainer _gameDataContainer;

        public string resourcesPath = "SpriteContainer"; // Path to the Resources folder.

        public override void Init()
        {
            base.Init();
            _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
        }
        public void CheckForMatch(Card card)
        {
            if (firstCard == null)
            {
                firstCard = card;
            }
            else
            {
                if (firstCard.id == card.id)
                {
                    firstCard.Match();
                    card.Match();
                    if (!_gameDataContainer.MatchedCardIndexes.Contains(card.id))
                    {
                        _gameDataContainer.MatchedCardIndexes.Add(card.id);
                        _gameDataContainer.CardMatched = true;
                    }

                    firstCard = null;
                }
                else
                {
                    CoreContext.Instance.StartCoroutine(FlipCardsBack(card));
                }
            }
        }

        private IEnumerator FlipCardsBack(Card card)
        {
            yield return new WaitForSeconds(0.2f); // Wait for a moment before flipping back
            try
            {
                firstCard.Flip();
                card.Flip();
                List<Card> cards = new List<Card>();
                _gameDataContainer.FlipCards = firstCard;
                _gameDataContainer.FlipCards = card;
                firstCard = null;
            }
            catch (Exception e)
            {

            }
            yield return new WaitForSeconds(Time.deltaTime); // Wait for a moment before flipping back

        }
    }
}

    //public class CardMatchSystem : CoreSystem
    //{

    //    EventHandlerSystem _eventHandlerSystem;
    //    GameDataContainer _gameDataContainer;
    //    public override void Init()
    //    {
    //        base.Init();
    //        _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
    //        _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
    //    }
    //    public override void AddListener()
    //    {
    //        base.AddListener();
    //        _eventHandlerSystem.AddListener(GameEventKeys.CardDisplayed, CardDisplayed);

    //    }

    //    public override void RemoveListener()
    //    {
    //        base.RemoveListener();
    //        _eventHandlerSystem.RemoveListener(GameEventKeys.CardDisplayed, CardDisplayed);

    //    }
    //    public override void Finalise()
    //    {
    //        base.Finalise();

    //    }

    //    int ids = -1;
    //   int indxs = -1;
    //    private void CardDisplayed()
    //    {
    //        CheckForMatch(_gameDataContainer.DisplayedCard);
    //    }

    //    //     if(ids==-1)
    //    //    {
    //    //        ids=(_gameDataContainer.DisplayedCard.id);
    //    //        indxs=(_gameDataContainer.DisplayedCard.index);
    //    //    }
    //    //     else
    //    //    {
    //    //        if (ids==(_gameDataContainer.DisplayedCard.id)&& (indxs!=(_gameDataContainer.DisplayedCard.index)))
    //    //            {
    //    //                _gameDataContainer.CardMatched = true;
    //    //                ClearCards();
    //    //                ids=-1;
    //    //                indxs=-1;
    //    //            }

    //        //        else
    //        //        {
    //        //            ClearCards();
    //        //            ids=-1;
    //        //            indxs=-1;
    //        //            _gameDataContainer.FlipCards = true;

    //        //        }
    //        //    }
    //    private Card firstCard; // Store the first flipped card

    //    public void CheckForMatch(Card card)
    //    {
    //        if (firstCard == null)
    //        {
    //            firstCard = card;
    //        }
    //        else
    //        {
    //            if (firstCard.id == card.id)
    //            {
    //                firstCard.Match();
    //                card.Match();
    //              _gameDataContainer.CardMatched = true;

    //                // Handle matching logic (e.g., increase score, remove matched cards, etc.)
    //            }
    //            else
    //            {
    //                FlipCardsBack(card);
    //                _gameDataContainer.FlipCards = true;
    //            }
    //            firstCard = null;
    //        }
    //    }

    //    private void FlipCardsBack(Card card)
    //    {
    //        //yield return new WaitForSeconds(1f); // Wait for a moment before flipping back
    //        firstCard.Flip();
    //        card.Flip();
    //        firstCard = null;
    //    }
    //    //Debug.Log("bfr.CurrentCard. " + _gameDataContainer.CurrentCard.id + _gameDataContainer.CurrentCard.index);
    //    //Debug.Log("bfr.DisplayedCard. " + _gameDataContainer.DisplayedCard.id + _gameDataContainer.DisplayedCard.index);
    //    //Debug.Log("bfr.keys. " + _gameDataContainer.CurrentCard.Keys.Count);
    //    //if (_gameDataContainer.CurrentCard.Keys.Count == 0)
    //    //{
    //    //    _gameDataContainer.CurrentCard.Add(_gameDataContainer.DisplayedCard.id, _gameDataContainer.DisplayedCard.index);
    //    //    _gameDataContainer.DisplayedCard = null;

    //    //}
    //    //else
    //    //{
    //    //    if (_gameDataContainer.CurrentCard.Keys.FirstOrDefault() == (_gameDataContainer.DisplayedCard.id))
    //    //    {
    //    //        if (_gameDataContainer.CurrentCard[_gameDataContainer.DisplayedCard.id] != _gameDataContainer.DisplayedCard.index)
    //    //        {
    //    //            _gameDataContainer.DisplayedCard = null;
    //    //            Debug.Log("key" + _gameDataContainer.CurrentCard[_gameDataContainer.DisplayedCard.id]);
    //    //            Debug.Log("index" + _gameDataContainer.DisplayedCard.index);
    //    //            _gameDataContainer.CardMatched = true;
    //    //            ClearCards();

    //    //        }
    //    //        else if (_gameDataContainer.DisplayedCard.id != _gameDataContainer.CurrentCard.Keys.FirstOrDefault())
    //    //        {
    //    //            _gameDataContainer.DisplayedCard = null;
    //    //            //ClearCards();


    //    //        }

    //    //    }
    //    //    else
    //    //    {
    //    //        _gameDataContainer.DisplayedCard = null;
    //    //        ClearCards();

    //    //    }

    //    //}

    //    //Debug.Log("aftr.DisplayedCard. " + _gameDataContainer.DisplayedCard.id + _gameDataContainer.DisplayedCard.index);


    ////}

    //    private void ClearCards()
    //    {
    //        _gameDataContainer.CurrentCard.Clear();
    //        _gameDataContainer.FlipCards = true;

    //    }
    //}

//}