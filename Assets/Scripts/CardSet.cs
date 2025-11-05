using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    [CreateAssetMenu(fileName = "CardSet", menuName = "MemoryGame/CardSet")]
    public class CardSet : ScriptableObject
    {
        [Tooltip("Just drop all card face sprites here. IDs will auto-generate from names.")]
        public List<Sprite> faces = new List<Sprite>();

        /// <summary>
        /// Returns the sprite based on id (auto-generated as sprite.name).
        /// </summary>
        public Sprite GetFaceById(string id)
        {
            for (int i = 0; i < faces.Count; i++)
            {
                if (faces[i] != null && faces[i].name == id)
                    return faces[i];
            }
            return null;
        }

        /// <summary>
        /// Returns a list of all IDs (auto-generated from sprite names).
        /// </summary>
        public List<string> GetAllIds()
        {
            var list = new List<string>();
            for (int i = 0; i < faces.Count; i++)
            {
                if (faces[i] != null)
                    list.Add(faces[i].name);
            }
            return list;
        }
    }
}
