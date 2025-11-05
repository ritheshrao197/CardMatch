using UnityEngine;
namespace MemoryGame
{
    /// <summary>
    /// Component to fit a SpriteRenderer's sprite to specified world unit dimensions
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFitToUnits : MonoBehaviour
    {
        public enum FitMode { FitInside, Fill }
        public FitMode mode = FitMode.FitInside;

        [Header("Target size in world units")]
        public float targetWidth = 2f;
        public float targetHeight = 2f;

        [Header("Options")]
        public bool preserveAspect = true;

        SpriteRenderer _sr;

        void OnEnable() { _sr = GetComponent<SpriteRenderer>(); Apply(); }
        void OnValidate() { Apply(); }

        public void Apply()
        {
            if (_sr == null || _sr.sprite == null) return;

            // sprite.bounds.size is in world units at current scale
            // We want to compute the required localScale so the final size matches target
            var currentScale = transform.localScale;
            var size = _sr.sprite.bounds.size; // size at scale 1

            if (size.x <= 0f || size.y <= 0f) return;

            float sx = targetWidth / size.x;
            float sy = targetHeight / size.y;

            float kx, ky;

            if (preserveAspect)
            {
                if (mode == FitMode.FitInside)
                {
                    float k = Mathf.Min(sx, sy);
                    kx = ky = k;
                }
                else // Fill
                {
                    float k = Mathf.Max(sx, sy);
                    kx = ky = k;
                }
            }
            else
            {
                kx = sx;
                ky = sy;
            }

            transform.localScale = new Vector3(kx, ky, currentScale.z);
        }
    }

}