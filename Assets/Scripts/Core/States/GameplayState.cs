using System;
using Core.FSM;
using Core.Services.SceneManagement;
using UI.Views;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace Core.States
{
    public class GameplayState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;

        private GameplayView _gameplayView;

        public GameplayState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override async void Enter()
        {
            _gameplayView = GameObject.FindObjectOfType<GameplayView>();

            if (_gameplayView != null)
            {
                _gameplayView.SetViewVisible(true);
            }

            _gameplayView.GetSpawnPositions();
            _gameplayView.InstantiateTreat();
            
            Debug.Log("Enter GameplayState");


        }


        public override void Exit()
        {
            _gameplayView.SetViewVisible(false);
        }
    }
}