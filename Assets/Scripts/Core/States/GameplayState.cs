using System;
using Core.Services.SceneManagement;
using UI.Controllers;
using UI.Views;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;
using State = Core.FSM.State;

namespace Core.States
{
    public class GameplayState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;
        
        private Action _closeGame;

        private GameplayManager _gameplayManager;

        public GameplayState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override async void Enter()
        {
            _gameplayManager = GameObject.FindObjectOfType<GameplayManager>();
            _gameplayManager.Init();
            _gameplayManager.OnCloseButtonClicked += OnButtonClickHandler;
        }


        private void OnButtonClickHandler()
        {
            _callback?.Invoke();
        }


        public override void Exit()
        {
            _gameplayManager.OnCloseButtonClicked -= OnButtonClickHandler;
        }
    }
}