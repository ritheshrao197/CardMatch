using MatchGame.Core;
using UnityEngine;

namespace MatchGame.View
{
    public class View : MonoBehaviour
    {
        public EventHandlerSystem eventHandlerSystem;

        ///// <summary>
        ///// This method is called when the GameObject is enabled, which typically occurs when the scene starts or the object is created.
        ///// Automatically registers this view with the ViewManager and initializes the screen reference.
        ///// </summary>
        //public void Init()
        //{
        //    Init();
        //}
        private void Start()
        {
            Init();
        }
        /// <summary>
        /// Initializes the view by registering it with the ViewSystem and setting up the screen reference.
        /// </summary>
        public virtual void Init()
        {
            ViewSytem.RegisterView(this);  // Register this view with the ViewSystem.
            eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();

        }
    /// <summary>
    /// This method is called when the GameObject is Disabled, which typically occurs when the object is deleted.
    /// </summary>
    private void OnDisable()
        {
            Finalise();
        }
        /// <summary>
        /// Finalize and clean up resources, if needed.
        /// </summary>
        public virtual void Finalise()
        {
            // Implement resource cleanup or finalization logic here if necessary.
        }
       
    }
}
