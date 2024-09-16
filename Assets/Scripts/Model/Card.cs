namespace MatchGame.Data
{
    public class Card
    {
        public int id;
        public int index;
        public bool isFlipped;
        public bool isMatched;

        public Card(int index, int id)
        {
            this.id = id;
            this.index = index;
            this.isFlipped = false;
            this.isMatched = false;
        }

        public void Flip()
        {
            isFlipped = !isFlipped;
        }

        public void Match()
        {
            isMatched = true;

        }
    }

}