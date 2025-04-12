using System;
using Core.FSM;
using Core.Services.SceneManagement;
using UI.Controllers;
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
        }


        public override void Exit()
        {
        }
    }
}