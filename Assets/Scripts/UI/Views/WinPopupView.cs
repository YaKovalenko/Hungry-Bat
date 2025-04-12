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

        public void SetTimeText(string timeText)
        {
            _timeText.text = timeText;
        }

        private void OnMainMenuButtonClicked()
        {
            
        }

        private void OnRestartButtonClicked()
        {
            
        }
    }
}