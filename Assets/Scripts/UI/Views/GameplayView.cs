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
        private GameObject _spawnArea;

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked()
        {
            
        }
    }
}