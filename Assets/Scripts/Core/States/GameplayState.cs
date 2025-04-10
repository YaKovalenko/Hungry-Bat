using System;
using Core.FSM;
using Core.SceneManagement;

namespace Core.States
{
    public class GameplayState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;

        public GameplayState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override async void Enter()
        {
            await _sceneManagementService.LoadSceneAsync(Constants.GAME_SCENE_NAME);
            
            

        }

        public override void Exit()
        {
        }
    }
}