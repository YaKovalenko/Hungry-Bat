using System;
using System.Collections.Generic;
using UI.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Controllers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] 
        private GameplayView _gameplayView;
        
        private int _score;
        private List<Vector3> _listPositions;
        private Treat _treat;
        private Treat _treatPrefab;
        private RectTransform _spawnArea;

        public Action OnCloseButtonClicked;
        
        private void Awake()
        {
            _gameplayView.OnCloseButtonClick += OnCloseButtonHandler;

            _spawnArea = _gameplayView.SpawnArea;
            _treatPrefab = _gameplayView.TreatPrefab; 
        }

        private void OnDestroy()
        {
            _gameplayView.OnCloseButtonClick -= OnCloseButtonHandler;
        }

        public void Init()
        {
            _gameplayView.SetViewVisible(true);

            GetSpawnPositions();
            SpawnTreat();
            ChangeTreatPosition();
            
            _treat.OnTreatClicked += OnTreatClicked;
        }

        private void OnCloseButtonHandler()
        {
            OnCloseButtonClicked?.Invoke();
        }

        private void OnTreatClicked()
        {
            _score++;
            _gameplayView.SetScore(_score);
            ChangeTreatPosition();
        }

        private void GetSpawnPositions()
        {
            _listPositions = new List<Vector3>();
            
            for (int i = 0; i < 10; i++)
            {
                float x = Random.Range(_spawnArea.rect.xMin, _spawnArea.rect.xMax);
                float y = Random.Range(_spawnArea.rect.yMin, _spawnArea.rect.yMax);
                
                _listPositions.Add(new Vector3(x, y, 0));
            }
        }

        private void SpawnTreat()
        {
            _treat = Instantiate(_treatPrefab, _spawnArea.transform);
        }

        private void ChangeTreatPosition()
        {
            _treat.transform.localPosition = _listPositions[Random.Range(0, _listPositions.Count)];
        }
    }
}