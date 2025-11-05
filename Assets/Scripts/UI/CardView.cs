using System.Collections;
using UnityEngine;

namespace MemoryGame.Views
{
    /// <summary>
    /// View component for a single card in the Memory Game
    /// </summary>
    public class CardView : MonoBehaviour
    {
        [Header("Renderers")]
        public SpriteRenderer face;
        public SpriteRenderer back;

        public void BindFace(Sprite faceSprite)
        {
            if (face != null)
            {
                face.sprite = faceSprite;
                var fitter = face.GetComponent<SpriteFitToUnits>();
                if (fitter) fitter.Apply();
            }
        }

        public IEnumerator AnimateFlip(bool faceUp, float duration, System.Action onComplete = null)
        {
            float half = Mathf.Max(0.0001f, duration * 0.5f);
            float t = 0f;
            // First half: scale X down to 0
            while (t < half)
            {
                t += Time.deltaTime;
                float k = 1f - Mathf.Clamp01(t / half);
                transform.localScale = new Vector3(Mathf.Max(0.0001f, k), 1f, 1f);
                yield return null;
            }

            // Swap visibility at the "thin" point
            if (face != null && back != null)
            {
                face.enabled = faceUp;
                back.enabled = !faceUp;
            }

            t = 0f;
            // Second half: scale X up to 1
            while (t < half)
            {
                t += Time.deltaTime;
                float k = Mathf.Clamp01(t / half);
                transform.localScale = new Vector3(Mathf.Max(0.0001f, k), 1f, 1f);
                yield return null;
            }
            transform.localScale = Vector3.one;
            onComplete?.Invoke();
        }

        public void SetInstant(bool faceUp)
        {
            if (face != null && back != null)
            {
                face.enabled = faceUp;
                back.enabled = !faceUp;
            }
            transform.localScale = Vector3.one;
        }
        public void NormalizeChildScale()
        {
            if (face) face.transform.localScale = Vector3.one;
            if (back) back.transform.localScale = Vector3.one;
        }
    }

}