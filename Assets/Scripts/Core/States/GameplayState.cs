using System;
using Core.Providers;
using Core.Services.SceneManagement;
using StaticData;
using UI.Controllers;
using UnityEngine;
using State = Core.FSM.State;

namespace Core.States
{
    public class GameplayState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly IStaticDataProvider _staticDataProvider;

        private readonly Action _callback;
        private readonly Action _playerWin;
        private readonly Action _playerLose;

        private GameplayManager _gameplayManager;
        
        private Action _closeGame;

        public GameplayState(ISceneManagementService sceneManagementService, IStaticDataProvider staticDataProvider,
            Action callback, Action playerWin, Action playerLose)
        {
            _sceneManagementService = sceneManagementService;
            _staticDataProvider = staticDataProvider;
            
            _callback = callback;
            _playerWin = playerWin;
            _playerLose = playerLose;
        }

        public override async void Enter()
        {
            var gameDataContainer = _staticDataProvider.Get<GameDataContainer>();
            
            _gameplayManager = GameObject.FindObjectOfType<GameplayManager>();

            _gameplayManager.SetInitialValues(gameDataContainer.AmountOfTreatsRequired, gameDataContainer.RoundTime);
            _gameplayManager.Init();
            
            _gameplayManager.OnCloseButtonClicked += OnButtonClickHandler;
            _gameplayManager.OnPlayerWin += OnPlayerWinHandler;
            _gameplayManager.OnPlayerLose += OnPlayerLoseHandler;
        }

        private void OnPlayerLoseHandler()
        {
            _playerLose?.Invoke();
        }

        private void OnPlayerWinHandler()
        {
            _playerWin?.Invoke();
        }

        private void OnButtonClickHandler()
        {
            _callback?.Invoke();
        }

        public override void Exit()
        {
            _gameplayManager.HideView();
            _gameplayManager.OnCloseButtonClicked -= OnButtonClickHandler;
            _gameplayManager.OnPlayerWin -= OnPlayerWinHandler;
            _gameplayManager.OnPlayerLose -= OnPlayerLoseHandler;
        }
    }
}