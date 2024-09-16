using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MatchGame.Data;
using MatchGame.Core;
using System;

namespace MatchGame.View
{
    using UnityEngine;
    using UnityEngine.UI;


    public class CardView : View
    {
        [SerializeField] private Sprite _firstMaterial;
        [SerializeField] private Sprite _secondMaterial;
        [SerializeField] private Image _cardImage;
        [SerializeField] private Image _cardBack;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI _indexTxt;
        private Quaternion _currentRotation;

        [HideInInspector] public bool _revealed = false;
        public int cardIndex;
        public int cardID;
        private Card cardData;
        private CardMatchSystem _cardManager;

        private GameDataContainer _gameDataContainer;
        private AudioDataContainer _audioDataContainer;
        private bool coroutineAllowed =true, facedUp;


        public void SetIndex(int index, int id)
        {
            base.Init();
            cardData = new Card(index, id);
            cardID = id;
            cardIndex = index;
            _indexTxt.text = "" + id;
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            _audioDataContainer = ServiceLocator.Instance.Get<AudioDataContainer>();
            _cardManager = ServiceLocator.Instance.Get<CardMatchSystem>();
            {
                _cardImage.sprite = _gameDataContainer.Sprites[id];
            }
            //catch(Exception e)
            //{

            //}

            button = GetComponent<Button>();
            // Attach the OnClick event handler to the button.
            button.onClick.AddListener(OnCardClick);
            eventHandlerSystem.AddListener(GameEventKeys.FlipCards, ApplyFirstMaterial);
            eventHandlerSystem.AddListener(GameEventKeys.CardMatched, CardMatched);



        }
        private void OnDisable()
        {
            eventHandlerSystem.RemoveListener(GameEventKeys.FlipCards, ApplyFirstMaterial);
            eventHandlerSystem.RemoveListener(GameEventKeys.CardMatched, CardMatched);
            //button.onClick.RemoveListener(OnCardClick);


        }

        private void CardMatched()
        {
            if (cardData.isMatched)
            {
                StartCoroutine(DeactivateCoroutine());
                _audioDataContainer.OnButtonClick = SFX.CorrectMatch;

            }
        }

        private void OnCardClick()
        {
            if (!cardData.isFlipped && !cardData.isMatched)
            {
                cardData.Flip();
                ApplySecondMaterial();
                // Flip the card's visual representation (e.g., show the card face)
                // You'll need to implement the UI and card flipping animations in Unity.

                _cardManager.CheckForMatch(cardData);
                _audioDataContainer.OnButtonClick = SFX.ButtonClick;
            }
        }
        public Card GetIndex() { return cardData; }
        public void ApplyFirstMaterial()
        {
            //if (coroutineAllowed)
            {
                if (_gameDataContainer.FlipCards.id == cardData.id &&
                _gameDataContainer.FlipCards.index == cardData.index)
                {
                    cardData = _gameDataContainer.FlipCards;

                    _cardBack.sprite = _firstMaterial;

                    _cardImage.gameObject.SetActive(false);
                    CoreContext.Instance.StartCoroutine(FaceDown());

                }
            }


        }

        public void ApplySecondMaterial()
        {
            //if (coroutineAllowed)
            {
                CoreContext.Instance.StartCoroutine(FaceUp());

                _cardBack.sprite = _secondMaterial;
                _cardImage.gameObject.SetActive(true);
            }

        }

        private Color initialColor;
        private Color targetColor = new Color(0f, 0f, 0f, 0f); // Target color with alpha set to 0.

        private IEnumerator DeactivateCoroutine()
        {
            //Revealed = false;
            float elapsedTime = 0f;
            initialColor = _cardBack.color;
            while (elapsedTime < 1)
            {
                _cardBack.color = Color.Lerp(initialColor, targetColor, elapsedTime / 1);
                _cardImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / 1);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _cardImage.gameObject.SetActive(false);

            _cardBack.color = targetColor;
            _cardImage.color = targetColor;
            //yield return new WaitForSeconds(1f);
            //gameObject.SetActive(false);
        }
        private IEnumerator FaceUp()
        {
            coroutineAllowed = false;

            if (!facedUp)
            {
                facedUp = !facedUp;

                for (float i = 0f; i <= 180f; i += 10f)
                {
                    transform.rotation = Quaternion.Euler(0f, i, 0f);
                    if (i == 90f)
                    {
                        _cardBack.sprite = _firstMaterial;
                    }
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
            coroutineAllowed = true;

        }
        private IEnumerator FaceDown()
        {
            coroutineAllowed = false;

            if (facedUp)
            {
                facedUp = !facedUp;

                for (float i = 180f; i >= 0f; i -= 10f)
                {
                    transform.rotation = Quaternion.Euler(0f, i, 0f);
                    if (i == 90f)
                    {
                        _cardBack.sprite = _secondMaterial;
                    }
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }

            coroutineAllowed = true;

        }
    }
}