
using System.Collections.Generic;
using MemoryGame.Config;
using MemoryGame.Events;
using UnityEngine;
namespace MemoryGame.Controller
{
    public class BoardController
    {
        private readonly Transform _root;
        private readonly GameConfig _cfg;
        private readonly CardSet _set;
        private readonly ObjectPool<CardController> _pool;
        private readonly BoardFrame _frame; // NEW

        public List<CardController> Cards { get; } = new List<CardController>();

        public BoardController(Transform root, GameConfig cfg, CardSet set, ObjectPool<CardController> pool, BoardFrame frame = null)
        {
            _root = root; _cfg = cfg; _set = set; _pool = pool; _frame = frame;
        }

        /// <summary>
        /// Build or rebuild the board using only the required number of cards.
        /// Reuses already-instantiated cards when possible, and returns extras to the pool.
        /// </summary>
        public void Build(int rows, int cols)
        {
            int totalSlots = rows * cols;
            int usable = (totalSlots / 2) * 2; // enforce even count
            int pairs = usable / 2;

            if (usable != totalSlots)
            {
                Debug.LogWarning($"[BoardController] Odd grid detected ({rows}x{cols}={totalSlots}). Using {usable} cards (pairs={pairs}) and leaving {totalSlots - usable} empty slot(s).");
            }

            // Prepare ids
            var ids = PickIds(pairs);
            Shuffle(ids);

            // Pool sizing
            while (Cards.Count < usable)
            {
                var card = _pool.Get();
                card.transform.SetParent(_root, false);
                Cards.Add(card);
            }
            if (Cards.Count > usable)
            {
                for (int i = Cards.Count - 1; i >= usable; i--)
                {
                    _pool.Release(Cards[i]);
                    Cards.RemoveAt(i);
                }
            }

            // === Layout inside BoardFrame (if provided), else fallback to old spacing ===
            if (_frame != null && _frame.HasBounds(out var inner))
            {
                LayoutInsideRect(inner, rows, cols, ids);
            }
            else
            {
                float scale = CalculateCardScale(rows, cols);
                for (int i = 0; i < usable; i++)
                {
                    var card = Cards[i];
                    var pos = IndexToLocal(i, rows, cols, _cfg.cellX, _cfg.cellY);
                    card.transform.localPosition = pos;
                    card.transform.localScale = Vector3.one * scale;

                    var id = ids[i];
                    var face = _set.GetFaceById(id);
                    card.Init(id, face, startFaceUp: false);
                    card.flipDuration = _cfg.flipDuration;
                }
            }

            GameEvents.RaiseRemainingPairsChanged(pairs);
        }

        private void LayoutInsideRect(Rect innerWorld, int rows, int cols, List<string> ids)
        {
            int totalSlots = rows * cols;
            int usable = (totalSlots & 1) == 1 ? totalSlots - 1 : totalSlots; // ensure even

            // Cell size from inner rect
            float cellW = innerWorld.width / cols;
            float cellH = innerWorld.height / rows;

            // Dynamic extra padding for tighter grids (prevents edge bleed in 5×5+)
            // Effective padding = boardFrame.cardPadding + a small grid-based term
            float gridPad = Mathf.Clamp01(0.012f * Mathf.Max(rows, cols));   // ~0.06 at 5x5
            float pad = Mathf.Clamp01((_frame != null ? _frame.cardPadding : 0.1f) + gridPad);

            float boxW = cellW * (1f - pad);
            float boxH = cellH * (1f - pad);

            for (int i = 0; i < usable; i++)
            {
                int r = i / cols;
                int c = i % cols;

                // Center of each cell in world space
                float cx = innerWorld.xMin + (c + 0.5f) * cellW;
                float cy = innerWorld.yMax - (r + 0.5f) * cellH;
                Vector3 worldPos = new Vector3(cx, cy, 0f);
                Vector3 localPos = _root.InverseTransformPoint(worldPos);

                var card = Cards[i];
                card.transform.localPosition = localPos;

                // Make sure child renderers are at scale 1 to avoid double scaling
                card.view?.NormalizeChildScale();

                // Measure natural size at scale 1 (see helper below)
                Vector2 nat = GetCardNaturalSize(card);
                float scale = Mathf.Min(boxW / nat.x, boxH / nat.y);
                card.transform.localScale = Vector3.one * scale;

                // Init content
                var id = ids[i];
                var face = _set.GetFaceById(id);
                card.Init(id, face, false);
                card.flipDuration = _cfg.flipDuration;
            }
        }

        private static Vector2 GetCardNaturalSize(CardController card)
        {
            var view = card.view;
            if (view != null)
            {
                if (view.back && view.back.sprite)
                {
                    // sprite.bounds.size is in local sprite units; multiply by child localScale if not 1
                    var s = view.back.sprite.bounds.size;
                    var ls = view.back.transform.localScale;
                    return new Vector2(s.x * ls.x, s.y * ls.y);
                }
                if (view.face && view.face.sprite)
                {
                    var s = view.face.sprite.bounds.size;
                    var ls = view.face.transform.localScale;
                    return new Vector2(s.x * ls.x, s.y * ls.y);
                }
            }
            // conservative default aspect (taller than wide)
            return new Vector2(1f, 1.4f);
        }

        public void ClearAll()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                _pool.Release(Cards[i]);
            }
            Cards.Clear();
        }

        private List<string> PickIds(int pairCount)
        {
            var unique = new List<string>();
            var avail = new List<string>(_set.GetAllIds());
            Shuffle(avail);
            for (int i = 0; i < pairCount && i < avail.Count; i++)
                unique.Add(avail[i]);

            var list = new List<string>(pairCount * 2);
            foreach (var id in unique)
            {
                list.Add(id); list.Add(id);
            }
            return list;
        }

        private float CalculateCardScale(int rows, int cols)
        {
            // Base reference: 4x4 grid = scale 1.0, shrink when more cells
            int maxDim = Mathf.Max(rows, cols);
            return Mathf.Clamp(4f / maxDim, 0.4f, 1f);
        }

        private static Vector3 IndexToLocal(int index, int rows, int cols, float dx, float dy)
        {
            int r = index / cols;
            int c = index % cols;
            float width = (cols - 1) * dx;
            float height = (rows - 1) * dy;
            float x = -width * 0.5f + c * dx;
            float y = height * 0.5f - r * dy;
            return new Vector3(x, y, 0);
        }

        private static void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
