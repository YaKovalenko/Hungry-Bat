using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
        
        private int _score;
        private List<Vector3> _listPositions;
        private Treat _treat;

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);

        }

        private void OnTreatClicked()
        {
            Debug.Log($"Score {_score}");
            _score++;
            ChangeTreatPosition();
        }

        public void GetSpawnPositions()
        {
            _listPositions = new List<Vector3>();
            
            for (int i = 0; i < 10; i++)
            {
                float x = Random.Range(_spawnArea.rect.xMin, _spawnArea.rect.xMax);
                float y = Random.Range(_spawnArea.rect.yMin, _spawnArea.rect.yMax);
                
                _listPositions.Add(new Vector3(x, y, 0));
            }
        }

        public void InstantiateTreat()
        {
            _treat = Instantiate(_treatPrefab, _spawnArea.transform);
            _treat.transform.localPosition = _listPositions[Random.Range(0, _listPositions.Count)];
            _treat.OnTreatClicked += OnTreatClicked;
        }
        
        public void ChangeTreatPosition()
        {
            _treat.transform.localPosition = _listPositions[Random.Range(0, _listPositions.Count)];
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _treatPrefab.OnTreatClicked -= OnTreatClicked;
        }
        
        

        private void OnCloseButtonClicked()
        {
            
        }
        
        
    }
}