using System;
using Core.FSM;
using Core.SceneManagement;

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

        }
        
        public override async void Exit()
        {
            
        }

        private FiniteStateMachine CreateStateMachine(ISceneManagementService sceneManagementService,
            Action gameStateCallback)
        {
            var gameplayState = new GameplayState(sceneManagementService, gameStateCallback);
            var winState = new WinState(sceneManagementService, () => _currentState = _winStateId);
            var loseState = new LoseState(sceneManagementService, () => _currentState = _loseStateId);

            var transitions = new Transition[]
            {
                new Transition(() => string.Equals(_currentState, _winStateId), gameplayState, winState),
                new Transition(() => string.Equals(_currentState, _gameplayStateId), winState, gameplayState),
                new Transition(() => string.Equals(_currentState, _loseStateId), gameplayState, loseState),
                new Transition(() => string.Equals(_currentState, _gameplayStateId), loseState, gameplayState)
            };

            return new FiniteStateMachine(transitions, gameplayState);
        }
    }
}