using Core.FSM;
using Core.SceneManagement;

namespace Core.States
{
    public class GameSuperState : SuperState
    {
        private readonly string _gameplayStateId = FiniteStateMachine.GetStateID<GameplayState>();
        private readonly string _winStateId = FiniteStateMachine.GetStateID<WinState>();
        private readonly string _loseStateId = FiniteStateMachine.GetStateID<LoseState>();

        private string _currentState;

        public GameSuperState(ISceneManagementService sceneManagementService)
        {
            FiniteStateMachine = CreateStateMachine(sceneManagementService);
        }

        private FiniteStateMachine CreateStateMachine(ISceneManagementService sceneManagementService)
        {
            var gameplayState = new GameplayState(sceneManagementService, () => _currentState = _gameplayStateId);
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