using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenuView : BaseView
    {
        [SerializeField]
        private Button _startGameButton;
        
        [SerializeField]
        private TextMeshProUGUI _bestScoreText;

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

        public void SetBestScore(string bestScore)
        {
            _bestScoreText.text = bestScore;
        }
    }
}