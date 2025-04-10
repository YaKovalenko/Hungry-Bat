using System;
using Core.FSM;
using Core.SceneManagement;
using Core.States;
using UnityEngine;
using VContainer.Unity;

namespace Core
{
    public class GameEntryPoint  : IStartable, ITickable, IFixedTickable, IDisposable
    {
        private readonly string _loadingStateId = FiniteStateMachine.GetStateID<LoadingState>();
        private readonly string _mainMenuStateId = FiniteStateMachine.GetStateID<MainMenuState>();

        private readonly FiniteStateMachine _stateMachine;

        private string _currentState;

        public GameEntryPoint(ISceneManagementService sceneManagementService)
        {
            _stateMachine = CreateStateMachine(sceneManagementService);
        }

        public void Start()
        {
            _stateMachine.Start();
        }

        public void Tick()
        {
            _stateMachine.Tick(Time.deltaTime);
        }

        public void FixedTick()
        {
            _stateMachine.FixedTick(Time.fixedDeltaTime);
        }

        public void Dispose()
        {
            _stateMachine.Stop();
            _stateMachine.Dispose();
        }

        private FiniteStateMachine CreateStateMachine(ISceneManagementService sceneManagementService)
        {
            var loadingState = new LoadingState(sceneManagementService, () => _currentState = _mainMenuStateId);
            var mainMenuState = new MainMenuState(sceneManagementService, () => _currentState = _mainMenuStateId);

            var transitions = new Transition[]
            {
                new Transition(() => string.Equals(_currentState, _mainMenuStateId), loadingState, mainMenuState)
            };

            return new FiniteStateMachine(transitions, loadingState);
        }
    }
}