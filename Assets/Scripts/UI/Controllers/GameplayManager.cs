using System;
using System.Collections;
using System.Collections.Generic;
using UI.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Controllers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private GameplayView _gameplayView;

        private Timer _timer;

        private int _score;
        private int _maxScore;
        private List<Vector3> _listPositions;
        private Treat _treat;
        private Treat _treatPrefab;
        private RectTransform _spawnArea;

        private float _timeDuration;
        private float _remainingTime;
        private float _currentTime;

        private Coroutine _timerCoroutine;

        public Action OnCloseButtonClicked;
        public Action OnPlayerWin;
        public Action OnPlayerLose;

        private void Awake()
        {
            _gameplayView.OnCloseButtonClick += OnCloseButtonHandler;

            _timer = gameObject.AddComponent<Timer>();

            _timer.OnTimerTick += UpdateTimerDisplay;
            _timer.OnTimerEnd += TimerExpired;

            _spawnArea = _gameplayView.SpawnArea;
            _treatPrefab = _gameplayView.TreatPrefab;
        }

        private void OnDestroy()
        {
            _gameplayView.OnCloseButtonClick -= OnCloseButtonHandler;
            _timer.OnTimerTick -= UpdateTimerDisplay;
            _timer.OnTimerEnd -= TimerExpired;
        }

        public void SetInitialValues(int maxScore, int timeDuration)
        {
            _maxScore = maxScore;
            _timeDuration = timeDuration;
            
            UpdateTimerDisplay(_timeDuration);
            _gameplayView.SetScore(0, _maxScore);
        }

        public void Init()
        {
            _timer.Init(_timeDuration);
            GetSpawnPositions();
            SpawnTreat();
            ChangeTreatPosition();

            _treat.OnTreatClicked += OnTreatClicked;
            _gameplayView.SetViewVisible(true);

            _timer.StartTimer();
        }

        private void OnCloseButtonHandler()
        {
            OnCloseButtonClicked?.Invoke();

            _timer.StopTimer();
        }

        private void CheckBestTime()
        {
            var bestTime = PlayerPrefs.GetFloat(Constants.BEST_TIME_PLAYER_PREFS);
            if (bestTime < _currentTime)
            {
                PlayerPrefs.SetFloat(Constants.BEST_TIME_PLAYER_PREFS, _currentTime);
            }
        }

        private void OnTreatClicked()
        {
            _score++;
            _gameplayView.SetScore(_score, _maxScore);
            CheckScore();
            ChangeTreatPosition();
        }

        private void CheckScore()
        {
            if (_score == _maxScore)
            {
                _currentTime = _timer.GetTimeRemaining();
                CheckBestTime();
                
                OnPlayerWin?.Invoke();
            }
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

        private void UpdateTimerDisplay(float remainingTime)
        {
            _gameplayView.SetTime(remainingTime);
        }

        private void TimerExpired()
        {
            OnPlayerLose?.Invoke();
        }

        public void HideView()
        {
            _timer.StopTimer();
            _gameplayView.SetViewVisible(false);
        }
    }
}