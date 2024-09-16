using System;
using System.Collections.Generic;
namespace MatchGame.Core
{
    public class EventHandlerSystem :CoreSystem
    {
        private Dictionary<int, Action> eventDictionary = new Dictionary<int, Action>();

        public void AddListener(int eventName, Action eventAction)
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = eventAction;
            }
            else
            {
                eventDictionary[eventName] += eventAction;
            }
        }

        public void RemoveListener(int eventName, Action eventAction)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= eventAction;

                if (eventDictionary[eventName] == null)
                {
                    eventDictionary.Remove(eventName);
                }
            }
        }

        public void TriggerEvent(int eventName)
        {
            if (eventDictionary.TryGetValue(eventName, out Action eventAction))
            {
                eventAction.Invoke();
            }
        }
    }

}