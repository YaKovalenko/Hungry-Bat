using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class LosePopupView : BaseView
    {
        [SerializeField]
        private Button _restartButton;
        
        [SerializeField]
        private Button _mainMenuButton;

        public Action OnMainMenuClicked;
        public Action OnRestartClicked;
        
        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            OnMainMenuClicked?.Invoke();
        }

        private void OnRestartButtonClicked()
        {
            OnRestartClicked?.Invoke();
        }
    }
}