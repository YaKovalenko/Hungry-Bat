using System;
using System.Collections.Generic;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class GameplayView : BaseView
    {
        [SerializeField] 
        private Button _closeButton;
        
        [SerializeField]
        private TextMeshProUGUI _scoreText;
        
        [SerializeField]
        private TextMeshProUGUI _timeText;

        [SerializeField] 
        private RectTransform _spawnArea;

        [SerializeField] 
        private Treat _treatPrefab;
        
        public RectTransform SpawnArea => _spawnArea;
        public Treat TreatPrefab => _treatPrefab;

        public Action OnCloseButtonClick;

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
        
        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void SetScore(int score, int maxScore)
        {
            _scoreText.text = $"{score} / {maxScore}";
        }

        public void SetTime(float time)
        {
            _timeText.text = TimeExtensions.FormatTime(time);
        }

        private void OnCloseButtonClicked()
        {
            OnCloseButtonClick?.Invoke();
        }
    }
}