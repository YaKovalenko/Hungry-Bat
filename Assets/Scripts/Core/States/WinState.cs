using System;
using Core.FSM;
using Core.SceneManagement;

namespace Core.States
{
    public class WinState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;

        public WinState(ISceneManagementService sceneManagementService, Action callback)
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