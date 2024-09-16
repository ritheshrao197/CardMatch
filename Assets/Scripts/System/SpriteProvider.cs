using UnityEngine;
namespace MatchGame
{
    public class SpriteProvider 
    {
        public SpriteContainer _spriteList;

        public Sprite GetSprite(int index)
        {
            return _spriteList.spriteList[index];
        }
    }
    public class KeyGenerator
    {
        static int currentKey = 1;

        public static int GetKey()
        {
            return currentKey++;
        }
    }
    public  class EventKeys
    {
        public  static int OnStart = KeyGenerator.GetKey();
    }
  

}