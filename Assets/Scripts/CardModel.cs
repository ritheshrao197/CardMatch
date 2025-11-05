using System;

namespace MemoryGame.Models
{
    public class CardModel
    {
        public string Id { get; private set; }
        public bool IsFaceUp { get; private set; }
        public bool IsMatched { get; private set; }

        public void FlipUp() => IsFaceUp = true;
        public void FlipDown() => IsFaceUp = false;
        public void Lock() => IsMatched = true;

        internal void Set(string id, bool isFaceUp=false, bool isMatched=false)
        {
            Id = id;
            IsFaceUp = isFaceUp;
            IsMatched = isMatched;
        }
       
    }
}