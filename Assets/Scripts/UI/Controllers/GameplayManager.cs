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
        private List<Vector3> _listPositions;
        private Treat _treat;
        private Treat _treatPrefab;
        private RectTransform _spawnArea;

        private float _gameDuration = 60f;
        private float _remainingTime;

        private Coroutine _timerCoroutine;

        public Action OnCloseButtonClicked;

    private void Awake()
    {
        _gameplayView.OnCloseButtonClick += OnCloseButtonHandler;

        _timer = gameObject.AddComponent<Timer>();
        _timer.Init(60f);
        
        _timer.OnTimerTick += UpdateTimerDisplay;
        _timer.OnTimerEnd += GameOver;

        _spawnArea = _gameplayView.SpawnArea;
        _treatPrefab = _gameplayView.TreatPrefab;
    }

    private void OnDestroy()
    {
        _gameplayView.OnCloseButtonClick -= OnCloseButtonHandler;
        _timer.OnTimerTick -= UpdateTimerDisplay;
        _timer.OnTimerEnd -= GameOver;
    }

    public void Init()
    {
        _gameplayView.SetViewVisible(true);

        GetSpawnPositions();
        SpawnTreat();
        ChangeTreatPosition();

        _treat.OnTreatClicked += OnTreatClicked;

        _timer.StartTimer();
    }

    private void OnCloseButtonHandler()
    {
        CheckScore();
        OnCloseButtonClicked?.Invoke();

        _timer.StopTimer();
    }

    private void CheckScore()
    {
        var bestScore = PlayerPrefs.GetInt(Constants.BEST_SCORE_PLAYER_PREFS);
        if (bestScore < _score)
        {
            PlayerPrefs.SetInt(Constants.BEST_SCORE_PLAYER_PREFS, _score);
        }
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
            float x = UnityEngine.Random.Range(_spawnArea.rect.xMin, _spawnArea.rect.xMax);
            float y = UnityEngine.Random.Range(_spawnArea.rect.yMin, _spawnArea.rect.yMax);

            _listPositions.Add(new Vector3(x, y, 0));
        }
    }

    private void SpawnTreat()
    {
        _treat = Instantiate(_treatPrefab, _spawnArea.transform);
    }

    private void ChangeTreatPosition()
    {
        _treat.transform.localPosition = _listPositions[UnityEngine.Random.Range(0, _listPositions.Count)];
    }

    private void UpdateTimerDisplay(float remainingTime)
    {
        _gameplayView.SetTime(remainingTime);
    }

    private void GameOver()
    {
        _gameplayView.SetViewVisible(false);
        CheckScore();
        OnCloseButtonClicked?.Invoke();
    }

    }
}