using System;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenuView : BaseView
    {
        [SerializeField]
        private Button _startGameButton;
        
        [SerializeField]
        private TextMeshProUGUI _bestTimeText;

        public Action OnPlayButtonClick;
        
        private void Awake()
        {
            _startGameButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            OnPlayButtonClick?.Invoke();
        }

        public void SetBestTime(float bestTime)
        {
            _bestTimeText.text = TimeExtensions.FormatTime(bestTime);
        }
    }
}