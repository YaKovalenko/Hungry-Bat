using System;
using Core.FSM;
using Core.Providers;
using Core.Services.SceneManagement;

namespace Core.States
{
    public class GameSuperState : SuperState
    {
        private readonly string _gameplayStateId = FiniteStateMachine.GetStateID<GameplayState>();
        private readonly string _winStateId = FiniteStateMachine.GetStateID<WinState>();
        private readonly string _loseStateId = FiniteStateMachine.GetStateID<LoseState>();

        private readonly ISceneManagementService _sceneManagementService;
        private readonly IStaticDataProvider _staticDataProvider;

        private readonly Action _setGameStateCallback;
        
        private string _currentState;

        private float _winTime;

        public GameSuperState(ISceneManagementService sceneManagementService, IStaticDataProvider staticDataProvider,
            Action setGameStateCallback)
        {
            _sceneManagementService = sceneManagementService;
            _setGameStateCallback = setGameStateCallback;
            
            FiniteStateMachine = CreateStateMachine(sceneManagementService, staticDataProvider, setGameStateCallback);
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
            IStaticDataProvider staticDataProvider,
            Action setGameStateCallback)
        {
            var gameplayState = new GameplayState(sceneManagementService, staticDataProvider,
                setGameStateCallback, OnPlayerWinHandler, OnPlayerLoseHandler);
            
            var winState = new WinState(sceneManagementService,
                () => _currentState = _winStateId, OnRestartHandler, OnMainMenuHandler);
            
            var loseState = new LoseState(sceneManagementService,  
                () => _currentState = _loseStateId, OnRestartHandler, OnMainMenuHandler);

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

        private void OnRestartHandler()
        {
            _currentState = _gameplayStateId;
        }

        private void OnMainMenuHandler()
        {
            _setGameStateCallback?.Invoke();
        }
    }
}