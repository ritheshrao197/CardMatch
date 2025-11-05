using System.Collections;
using MemoryGame.Events;
using MemoryGame.Models;
using MemoryGame.Views;
using UnityEngine;
namespace MemoryGame.Controller
{

    [RequireComponent(typeof(Collider2D))]
    public class CardController : MonoBehaviour
    {
        public CardView view;
        public float flipDuration = 0.25f;

        private readonly CardModel _model = new CardModel();
        private bool _inputEnabled = true;

        public CardModel Model => _model;

        public void Init(string id, Sprite faceSprite, bool startFaceUp = false)
        {
            _model.Set(id);
            if (view != null) view.BindFace(faceSprite);
            if (startFaceUp)
            {
                _model.FlipUp();
                view?.SetInstant(true);
            }
            else
            {
                _model.FlipDown();
                view?.SetInstant(false);
            }
            gameObject.SetActive(true);
            _inputEnabled = true;
            InputLock.Unlock();
                    }

        private void OnMouseUpAsButton()
        {
            // Debug.Log($"[CardController] OnMouseUpAsButton called for card {_model.Id}");
            // Debug.Log($"[CardController] _inputEnabled={_inputEnabled}, IsMatched={_model.IsMatched}, IsFaceUp={_model.IsFaceUp}");
            if (InputLock.IsLocked || !_inputEnabled || _model.IsMatched || _model.IsFaceUp) return;
            StartCoroutine(FlipRoutine(true));
            GameEvents.RaiseCardSelected(this);
        }

        public void SetInput(bool enabled) => _inputEnabled = enabled;

        public IEnumerator FlipRoutine(bool faceUp)
        {
            yield return view.StartCoroutine(view.AnimateFlip(faceUp, flipDuration));
            if (faceUp) _model.FlipUp(); else _model.FlipDown();
        }

        public void Lock() => _model.Lock();
        public void FaceInstant(bool faceUp)
        {
            view?.SetInstant(faceUp);
            if (faceUp) _model.FlipUp(); else _model.FlipDown();
        }
    }
}