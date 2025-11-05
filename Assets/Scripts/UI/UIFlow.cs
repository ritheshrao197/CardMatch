using System;
using MemoryGame.Events;
using MemoryGame.UI.Events;
using UnityEngine;
namespace MemoryGame.Views
{

    public class UIFlow : MonoBehaviour
    {
        [Header("Panels (MainCanvas)")]
        public GameObject homePanel;
        public GameObject hudPanel;

        [Header("PopupCanvas (overlay)")]
        public GameObject popupCanvasRoot;
        public GameObject resultPopup;
        public GameObject pausePopup;
        public GameObject levelSelectPanel;
        public GameObject dimBackground;

        void OnEnable()
        {
            UIEvents.OnShowLevelSelect += ShowLevelSelect;
            UIEvents.OnHideLevelSelect += HideLevelSelect;
            UIEvents.OnGoHome += ShowHome;
            UIEvents.OnShowHUD += ShowGameHUD;
            UIEvents.OnPause += ShowPause;
            UIEvents.OnResume += HidePause;
            UIEvents.OnRestart += HidePause;
        }
        void OnDisable()
        {
            UIEvents.OnShowLevelSelect -= ShowLevelSelect;
            UIEvents.OnHideLevelSelect -= HideLevelSelect;
            UIEvents.OnGoHome -= ShowHome;
            UIEvents.OnShowHUD -= ShowGameHUD;
            UIEvents.OnPause -= ShowPause;
            UIEvents.OnResume -= HidePause;
            UIEvents.OnRestart -= HidePause;

        }

        void Awake()
        {
            ShowOnly(home: true, hud: false);
            HideAllPopups();
        }

        public void ShowHome() { HideAllPopups(); ShowOnly(true, false); }
        public void ShowGameHUD() { HideAllPopups(); ShowOnly(false, true); }

        public void ShowLevelSelect()
        {
            ShowOnly(false, false);
            SetDim(true);
            if (levelSelectPanel) levelSelectPanel.SetActive(true);
            EnsurePopupCanvasActive();
        }
        public void HideLevelSelect()
        {
            // if (levelSelectPanel) levelSelectPanel.SetActive(false);
            // SetDim(false);
            // EnsurePopupCanvasActive();
            ShowHome();
        }

        public void ShowResult(bool win, int levelIndex, string reason, Action onNext, Action onHome)
        {
            Debug.Log($"[UIFlow] ShowResult called with win={win}, levelIndex={levelIndex}, reason={reason}");
            ShowOnly(false, false);
            SetDim(true);
            if (resultPopup != null)
            {
                var rp = resultPopup.GetComponent<ResultPopup>();
                if (rp != null)
                {
                    // Wrap callbacks so we hide the popup and show HUD before starting the next/home action
                    void NextWrapped()
                    {
                        HideAllPopups();
                        onNext?.Invoke();
                        ShowGameHUD();
                    }
                    void HomeWrapped()
                    {
                        HideAllPopups();
                        ShowHome();
                        onHome?.Invoke();
                    }
                    rp.Bind(win, levelIndex, reason, NextWrapped, HomeWrapped);
                    resultPopup.SetActive(true);

                }
            }
            EnsurePopupCanvasActive();
        }

        public void ShowPause()
        {
            InputLock.Lock();
            SetDim(true);
            if (pausePopup != null) pausePopup.SetActive(true);
            EnsurePopupCanvasActive();
        }

        public void HidePause()
        {
            if (pausePopup != null) pausePopup.SetActive(false);
            SetDim(false);
            EnsurePopupCanvasActive();
            InputLock.Unlock();
        }


        void HideAllPopups()
        {
            if (resultPopup) resultPopup.SetActive(false);
            if (pausePopup) pausePopup.SetActive(false);
            if (levelSelectPanel) levelSelectPanel.SetActive(false);
            SetDim(false);
            EnsurePopupCanvasActive();
        }

        void ShowOnly(bool home, bool hud)
        {
            if (homePanel) homePanel.SetActive(home);
            if (hudPanel) hudPanel.SetActive(hud);
        }

        void SetDim(bool v) { if (dimBackground) dimBackground.SetActive(v); }

        void EnsurePopupCanvasActive()
        {
            if (!popupCanvasRoot) return;
            bool any = (resultPopup?.activeSelf ?? false) || (pausePopup?.activeSelf ?? false) ||
                       (levelSelectPanel?.activeSelf ?? false) || (dimBackground?.activeSelf ?? false);
            popupCanvasRoot.SetActive(any);
        }
    }
}