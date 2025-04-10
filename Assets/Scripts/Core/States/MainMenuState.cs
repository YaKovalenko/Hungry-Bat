using System;
using Core.FSM;
using Core.SceneManagement;

namespace Core.States
{
    public class MainMenuState : SuperState
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;

        public MainMenuState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}