using System;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class WinPopupView : BaseView
    {
        [SerializeField] 
        private TextMeshProUGUI _timeText;
        
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

        public void SetTime(float time)
        {
            _timeText.text = TimeExtensions.FormatTime(time);
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