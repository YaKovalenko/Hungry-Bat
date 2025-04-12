using System;
using Core.FSM;
using Core.Services.SceneManagement;

namespace Core.States
{
    public class GameSuperState : SuperState
    {
        private readonly string _gameplayStateId = FiniteStateMachine.GetStateID<GameplayState>();
        private readonly string _winStateId = FiniteStateMachine.GetStateID<WinState>();
        private readonly string _loseStateId = FiniteStateMachine.GetStateID<LoseState>();

        private readonly ISceneManagementService _sceneManagementService;
        
        private string _currentState;

        public GameSuperState(ISceneManagementService sceneManagementService, Action setGameStateCallback)
        {
            _sceneManagementService = sceneManagementService;
            
            FiniteStateMachine = CreateStateMachine(sceneManagementService, setGameStateCallback);
        }

        public override async void Enter()
        {
            await _sceneManagementService.LoadSceneAsync(Constants.GAME_SCENE_NAME);
            base.Enter();
            
            _currentState = _gameplayStateId;

        }
        
        public override async void Exit()
        {
            base.Exit();
        }

        private FiniteStateMachine CreateStateMachine(ISceneManagementService sceneManagementService,
            Action setGameStateCallback)
        {
            var gameplayState = new GameplayState(sceneManagementService, setGameStateCallback, OnPlayerWinHandler, OnPlayerLoseHandler);
            var winState = new WinState(sceneManagementService, () => _currentState = _winStateId);
            var loseState = new LoseState(sceneManagementService, () => _currentState = _loseStateId);

            var transitions = new Transition[]
            {
                new (() => string.Equals(_currentState, _winStateId), gameplayState, winState),
                new (() => string.Equals(_currentState, _gameplayStateId), winState, gameplayState),
                new (() => string.Equals(_currentState, _loseStateId), gameplayState, loseState),
                new (() => string.Equals(_currentState, _gameplayStateId), loseState, gameplayState)
            };

            return new FiniteStateMachine(transitions, gameplayState);
        }

        private void OnPlayerWinHandler()
        {
            _currentState = _winStateId;
        }
        
        private void OnPlayerLoseHandler()
        {
            _currentState = _loseStateId;
        }
    }
}