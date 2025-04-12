using System;
using Core.FSM;
using Core.Services.SceneManagement;

namespace Core.States
{
    public class LoadingState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;

        public LoadingState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override async void Enter()
        {
            _callback?.Invoke();
        }
    }
}