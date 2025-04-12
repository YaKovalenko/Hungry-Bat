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

        private float _timeDuration = 60f;
        private float _remainingTime;

        private Coroutine _timerCoroutine;

        public Action OnCloseButtonClicked;
        public Action OnPlayerWin;
        public Action OnPlayerLose;

        private void Awake()
        {
            SetInitialValues();
            
            _gameplayView.OnCloseButtonClick += OnCloseButtonHandler;

            _timer = gameObject.AddComponent<Timer>();
            _timer.Init(60f);

            _timer.OnTimerTick += UpdateTimerDisplay;
            _timer.OnTimerEnd += TimerExpired;

            _spawnArea = _gameplayView.SpawnArea;
            _treatPrefab = _gameplayView.TreatPrefab;
            
        }

        private void SetInitialValues()
        {
            _maxScore = 30;
            UpdateTimerDisplay(_timeDuration);
            _gameplayView.SetScore(0, _maxScore);
        }

        private void OnDestroy()
        {
            _gameplayView.OnCloseButtonClick -= OnCloseButtonHandler;
            _timer.OnTimerTick -= UpdateTimerDisplay;
            _timer.OnTimerEnd -= TimerExpired;
        }

        public void Init()
        {
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

        // private void CheckScore()
        // {
        //     var bestScore = PlayerPrefs.GetInt(Constants.BEST_SCORE_PLAYER_PREFS);
        //     if (bestScore < _score)
        //     {
        //         PlayerPrefs.SetInt(Constants.BEST_SCORE_PLAYER_PREFS, _score);
        //     }
        // }

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
                OnPlayerWin?.Invoke();
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