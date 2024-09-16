using System.Collections.Generic;
using MatchGame.Core;
namespace MatchGame.View
{
    public class ViewSytem : CoreSystem
    {
       private int _activeScreenIndex = 0;
        private static List<View> _registeredViews = new List<View>();

        public static void RegisterView(View view)
        {
            if (!_registeredViews.Contains(view))
            {
                _registeredViews.Add(view);
            }
        }
        private void Start()
        {
            // Initialize by setting the first screen as active.
            SetActiveScreen(_activeScreenIndex);
        }
        //public override void Init()
        //{
        //    base.Init();
        //}
        public void ShowScreen(int screenIndex)
        {
            if (screenIndex != _activeScreenIndex)
            {
                SetActiveScreen(screenIndex);
            }
        }

        private void SetActiveScreen(int screenIndex)
        {
            //_registeredViews[_activeScreenIndex].DisableScreen();
            //_registeredViews[screenIndex].EnableScreen();


            //// Update the active screen index.
            _activeScreenIndex = screenIndex;
        }
    }
}