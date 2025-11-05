using UnityEngine;
namespace MemoryGame.Controller
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BoardFrame : MonoBehaviour
    {
        [Tooltip("Optional explicit reference; defaults to SpriteRenderer on this GameObject")] public SpriteRenderer frame;
        [Range(0f, 0.5f)] public float innerPadding = 0.05f;
        [Range(0f, 0.5f)] public float cardPadding = 0.1f;

        void Reset() { frame = GetComponent<SpriteRenderer>(); }

        public bool HasBounds(out Rect inner)
        {
            var sr = frame == null ? GetComponent<SpriteRenderer>() : frame;
            if (sr == null || sr.sprite == null) { inner = default; return false; }
            var b = sr.bounds;
            float padX = innerPadding * b.size.x;
            float padY = innerPadding * b.size.y;
            inner = new Rect(b.min.x + padX, b.min.y + padY, b.size.x - 2f * padX, b.size.y - 2f * padY);
            return true;
        }
    }

}
